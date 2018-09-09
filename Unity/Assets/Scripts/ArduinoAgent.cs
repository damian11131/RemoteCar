using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

// Class communicating with Arduino using ESP8266espClient 
public class ArduinoAgent : MonoBehaviour
{
	ESP8266Client espClient;

	public static class Commands
	{
		public static readonly string MoveForward = "FD";
		public static readonly string Stop = "ST";
		public static readonly string MoveBackwards = "BD";
		public static readonly string RotateRight = "RR";
		public static readonly string RotateLeft = "RL";
		public static readonly string StopRotating = "SR";
	}

	public static ArduinoAgent instance;

	readonly string DefaultEspIp = "192.168.4.1";
	readonly int DefaultEspPort = 80;

	#region Singleton
	void Awake()
	{
		instance = this;
		DontDestroyOnLoad (gameObject);
		espClient = new ESP8266Client ();
	}
	#endregion

	public void ConnectToEsp(string ip, int port)
	{
		try {
			espClient.Connect (ip, port);
		}catch(SocketException e) {
			throw e;
		}
	}

	public void ConnectToEsp()
	{
		try {
			ConnectToEsp(DefaultEspIp, DefaultEspPort);
		}catch(SocketException e) {
			throw e;
		}

	}

	public void DisconnectFromEsp()
	{
		espClient.Disconnect ();
	}

	public void MoveForward()
	{
		espClient.Writeln (Commands.MoveForward);
	}

	public void Stop()
	{
		espClient.Writeln (Commands.Stop);
	}

	public void RotateLeft()
	{
		espClient.Writeln (Commands.RotateLeft);
	}

	public void RotateRight()
	{
		espClient.Writeln (Commands.RotateRight);
	}

	public void StopRotating()
	{
		espClient.Writeln (Commands.StopRotating);
	}

	public void MoveBackwards()
	{
		espClient.Writeln (Commands.MoveBackwards);
	}
}