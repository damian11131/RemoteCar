using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net.Sockets;
using UnityEngine.Networking;
using System.Threading.Tasks;

// Class responsible for communicating with ESP8266 over WiFi
public class ESP8266Client
{
	TcpClient client;
	NetworkStream networkStream;
	StreamWriter writer;
    IAsyncResult result;

	public ESP8266Client(string espIp, int espPort)
	{
		client = new TcpClient ();
		Connect (espIp, espPort);
	}

	public ESP8266Client()
	{
		client = new TcpClient ();
	}

	public void Connect(string espIp, int espPort)
	{
        result = client.BeginConnect(espIp, espPort, null, null);
        result.AsyncWaitHandle.WaitOne(50);
		networkStream = client.GetStream ();
		writer = new StreamWriter (networkStream);
	}

	public bool IsConnected()
	{
		return client.Connected;
	}

	public void Write(int data)
	{
		writer.Write (data);
		writer.Flush ();
	}

	public void Write(string data)
	{
		writer.Write (data);
		writer.Flush ();
	}

	public void Writeln(string data)
	{
		writer.Write (data + '\n');
		writer.Flush ();
	}

	public void Write(byte data)
	{
		writer.Write (data);
		writer.Flush ();
	}

	public byte ReadByte()
	{
        return (byte) networkStream.ReadByte();
	}

	public void Disconnect()
    {
        client.EndConnect(result);
		client.Close ();
		writer.Close ();
	}
}
