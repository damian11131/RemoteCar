using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Net.Sockets;
using System;

public class SettingsGuiConttoller : MonoBehaviour
{
	// Set in UnityEditor
	public InputField ipInput;
	public InputField portInput;


	void Start()
	{
		InitializeInputFields ();
	}

	void InitializeInputFields()
	{
		if (PlayerPrefs.HasKey (Keys.IpKey)) {
			string ip = PlayerPrefs.GetString (Keys.IpKey);
			ipInput.text = ip;
		}
		if (PlayerPrefs.HasKey (Keys.PortKey)) {
			int port = PlayerPrefs.GetInt (Keys.PortKey);
			portInput.text = port.ToString();
		}
	}

	public void OnBackButtonClick()
	{
		SceneManager.LoadScene ("MainScene");
	}
		

	public void OnSaveButtonClick()
	{
		string ip = ipInput.text;
		string port = portInput.text;
		string msg;

	    if (!DataValidator.ValidateIp (ip)) {
			msg = "Incorrect ip";
			ipInput.text = msg;
			return;
		}
		if (!DataValidator.ValidatePort (port)) {
			msg = "Incorrect port";
			portInput.text = msg;
			return;
		}
			
		PlayerPrefs.SetString (Keys.IpKey, ip);
		PlayerPrefs.SetInt (Keys.PortKey, int.Parse (port));

	}
}
