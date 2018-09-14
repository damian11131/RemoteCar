using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net.Sockets;
using UnityEngine.Networking;

// Class responsible for communicating with ESP8266 over WiFi
public class ESP8266Client
{
	TcpClient client;
	NetworkStream networkStream;
	StreamWriter writer;
	StreamReader reader;
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

		result = client.BeginConnect (espIp, espPort, null, null);
		int timeout = 1000;
		result.AsyncWaitHandle.WaitOne (timeout);
		if (client.Connected)
		{
			networkStream = client.GetStream ();
			writer = new StreamWriter (networkStream);
			reader = new StreamReader (networkStream);
		} 
		else
		{
			throw new SocketException ();
		}
			
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

	public string ReadLine()
	{
		return reader.ReadLine ();
	}

	public int Read()
	{
		return reader.Read ();
	}

	public void Disconnect()
	{
		client.EndConnect (result);
		client.Close ();
		reader.Close ();
		writer.Close ();
	}
}
