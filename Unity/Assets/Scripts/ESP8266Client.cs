using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net.Sockets;

// Class responsible for communicating with ESP8266 over WiFi
public class ESP8266Client
{
	TcpClient client;
	NetworkStream networkStream;
	StreamWriter writer;
	StreamReader reader;
//	string espIp;
//	int espPort;
	bool isConnected;

	public ESP8266Client(string espIp, int espPort)
	{
		client = new TcpClient(espIp, espPort);
		networkStream = client.GetStream();
		writer = new StreamWriter(networkStream);
		reader = new StreamReader(networkStream);
		isConnected = true;
	}

	public ESP8266Client()
	{
		isConnected = false;
	}

	public void Connect(string espIp, int espPort)
	{
		if (isConnected) {
			Disconnect ();
			isConnected = false;
		}

		try {
			client = new TcpClient (espIp, espPort);
			networkStream = client.GetStream ();
			writer = new StreamWriter (networkStream);
			reader = new StreamReader (networkStream);
		}catch(SocketException e) {
			throw e;
		}

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
		if (isConnected) {
			isConnected = false;

			writer.Close ();
			reader.Close ();
			client.Close ();
		}
	}
}
