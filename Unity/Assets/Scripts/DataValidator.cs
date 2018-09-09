using UnityEngine;
using System;

public class DataValidator 
{
	// For now it supports only IPV4 
	public static bool ValidateIp(string ip)
	{
		char separator = '.';
		string[] substrings = ip.Split (separator);
		if (substrings.Length != 4) {
			return false;
		}
		foreach (string s in substrings) {
			
		}
		return true;
	}

	public static bool ValidatePort(string port)
	{
		int intPort;
		// Try convert port to integer
		try 
		{
			intPort = int.Parse(port);
		}catch(FormatException) 
		{
			return false;
		}
		// Port should be positive 16 bit integer
		if (intPort > 65535 || intPort <= 0) {
			return false;
		}
		return true;
	}
}
