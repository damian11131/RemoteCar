using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System;

public class MainGuiController : MonoBehaviour
{
	// Set in UnityEditor
	public RectTransform upButton;
	public RectTransform downButton;
	public RectTransform leftButton;
	public RectTransform rightButton;
	public TextMeshProUGUI statusText;

	public void Start() 
	{
		InitializeEventTriggers ();
		StartCoroutine (TryConnectingToEsp ());
	}
		
	IEnumerator TryConnectingToEsp()
	{
		if (!ArduinoAgent.instance.IsConnectedWithEsp ()) {
			try {
				ArduinoAgent.instance.ConnectToEsp ();
				statusText.text = "STATUS:CONNECTED";
			} catch (SocketException e) {
				Debug.Log ("Error");
			}
		} else {
			statusText.text = "STATUS:DISCONNECTED";
		}
		yield return new WaitForSeconds (2);
		StartCoroutine (TryConnectingToEsp ());

	}
		
	void InitializeEventTriggers()
	{
		EventTrigger upTrigger = upButton.GetComponent<EventTrigger> ();
		EventTrigger.Entry upEnterEntry = new EventTrigger.Entry ();
		upEnterEntry.eventID = EventTriggerType.PointerEnter;
		upEnterEntry.callback.AddListener ((data) => {
			OnUpButtonPress((PointerEventData)data);
		});
		upTrigger.triggers.Add (upEnterEntry);
		EventTrigger.Entry upExitEntry= new EventTrigger.Entry();
		upExitEntry.eventID = EventTriggerType.PointerExit;
		upExitEntry.callback.AddListener ((data) => {
			OnUpButtonRelease((PointerEventData)data);
		});
		upTrigger.triggers.Add (upExitEntry);

		EventTrigger downTrigger = downButton.GetComponent<EventTrigger> ();
		EventTrigger.Entry downEnterEntry = new EventTrigger.Entry ();
		downEnterEntry.eventID = EventTriggerType.PointerEnter;
		downEnterEntry.callback.AddListener ((data) => {
			OnDownButtonPress((PointerEventData)data);
		});
		downTrigger.triggers.Add (downEnterEntry);
		EventTrigger.Entry downExitEntry= new EventTrigger.Entry();
		downExitEntry.eventID = EventTriggerType.PointerExit;
		downExitEntry.callback.AddListener ((data) => {
			OnDownButtonRelease((PointerEventData)data);
		});
		downTrigger.triggers.Add (downExitEntry);

		EventTrigger leftTrigger = leftButton.GetComponent<EventTrigger> ();
		EventTrigger.Entry leftEnterEntry = new EventTrigger.Entry ();
		leftEnterEntry.eventID = EventTriggerType.PointerEnter;
		leftEnterEntry.callback.AddListener ((data) => {
			OnLeftButtonPress((PointerEventData)data);
		});
		leftTrigger.triggers.Add (leftEnterEntry);
		EventTrigger.Entry leftExitEntry = new EventTrigger.Entry ();
		leftExitEntry.eventID = EventTriggerType.PointerExit;
		leftExitEntry.callback.AddListener ((data) => {
			OnLeftButtonRelease((PointerEventData)data);
		});
		leftTrigger.triggers.Add (leftExitEntry);

		EventTrigger rightTrigger = rightButton.GetComponent<EventTrigger> ();
		EventTrigger.Entry rightEnterEntry = new EventTrigger.Entry ();
		rightEnterEntry.eventID = EventTriggerType.PointerEnter;
		rightEnterEntry.callback.AddListener ((data) => {
			OnRightButtonPress((PointerEventData)data);
		});
		rightTrigger.triggers.Add (rightEnterEntry);
		EventTrigger.Entry rightExitEntry = new EventTrigger.Entry ();
		rightExitEntry.eventID = EventTriggerType.PointerExit;
		rightExitEntry.callback.AddListener ((data) => {
			OnRightButtonRelease((PointerEventData)data);
		});
		rightTrigger.triggers.Add (rightExitEntry);
			
	}

	public void Update()
	{
		
	}
		

	public void OnUpButtonPress(PointerEventData data)
	{
		ArduinoAgent.instance.MoveForward ();
	}

	public void OnUpButtonRelease(PointerEventData data)
	{
		ArduinoAgent.instance.Stop ();
	}

	public void OnDownButtonPress(PointerEventData data)
	{
		ArduinoAgent.instance.MoveBackwards ();
	}

	public void OnDownButtonRelease(PointerEventData data)
	{
		ArduinoAgent.instance.Stop ();
	}

	public void OnLeftButtonPress(PointerEventData data)
	{
		ArduinoAgent.instance.RotateLeft();
	}

	public void OnLeftButtonRelease(PointerEventData data)
	{
		ArduinoAgent.instance.StopRotating ();
	}

	public void OnRightButtonPress(PointerEventData data)
	{
		ArduinoAgent.instance.RotateRight ();
	}

	public void OnRightButtonRelease(PointerEventData data)
	{
		ArduinoAgent.instance.StopRotating ();
	}

}
