using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Threading;
using System.Threading.Tasks;

// Class communicating with Arduino using ESP8266espClient 
public class ArduinoAgent
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
        public static readonly string MeasureVoltage = "MV";
	}


	readonly string DefaultEspIp = "192.168.4.1";
	readonly int DefaultEspPort = 80;
    readonly float ArduinoBatteryInitialVoltage = 7.5f;

    public ArduinoAgent()
    {
        espClient = new ESP8266Client();
    }

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

    public bool CheckEspConnection()
    {
        return espClient.IsConnected();
    }

	public bool IsConnectedWithEsp()
	{
		return espClient.IsConnected();
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
        espClient.Writeln(Commands.MoveBackwards);
    }

    public byte ReadVoltagePercentage()
    {
        espClient.Writeln(Commands.MeasureVoltage);
        byte res = espClient.ReadByte();
        return res;
    }


}