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
//	public RectTransform wheel;
	//public TextMeshProUGUI couldNotConnectText;
	public RectTransform leftButton;
	public RectTransform rightButton;

	//public Transform gaugeIndicatorContainer;
	//public GameObject arduinoAgent;
//	public TextMeshProUGUI batteryText;
	//public TextMeshProUGUI speedText;

	//RectTransform gaugeIndicator;


	public void Start() 
	{
		InitializeEventTriggers ();
		try {
			ArduinoAgent.instance.ConnectToEsp ();
		}catch(SocketException e) {
			Debug.Log ("Eror");
			Application.Quit ();
		}

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
		// For debug purposes
		/*if (Input.mousePresent) {
			Vector2 pos = Input.mousePosition;
			if (RectTransformUtility.RectangleContainsScreenPoint(wheel, pos, Camera.main))
			{
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
				float deltaX = worldPos.x - wheel.position.x;
				float deltaY = worldPos.y - wheel.position.y;
				float angle = Mathf.Atan2 (deltaY, deltaX) * Mathf.Rad2Deg;
				Debug.Log (angle);

				wheel.rotation = Quaternion.Euler (0, 0, angle);

				//CarIndicatorController.instance.TargetRotation = Quaternion.Euler (0, 0, angle);
			}
		}*/

		//RotateCarAndWheel ();
	//	UpdateSpeedGaugeIndicator ();
	}

	/*void RotateCarAndWheel()
	{
		foreach(Touch touch in Input.touches)
		{
			Vector2 touchPos = touch.position;
			// Is pointer/touch point inside wheel?
			if (RectTransformUtility.RectangleContainsScreenPoint(wheel, touchPos, Camera.main))
			{
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPos);
				float deltaX = worldPos.x - wheel.position.x;
				float deltaY = worldPos.y - wheel.position.y;
				float angle = Mathf.Atan2 (deltaY, deltaX) * Mathf.Rad2Deg - 90;

				wheel.rotation = Quaternion.Euler (0, 0, angle);

				CarIndicatorController.instance.TargetRotation = Quaternion.Euler (0, 0, angle);
			}

		}
	}
	void UpdateSpeedGaugeIndicator()
	{
		short gaugeDots = 10;
		float angleBetweenTwoDots = 230.0f / gaugeDots;
		float maxSpeed = CarIndicatorController.instance.GetTopSpeed();
		float currentSpeed = CarIndicatorController.instance.GetCurrentVelocity().magnitude;
		float currentSpeedPercentageOfMaxSpeed = currentSpeed / maxSpeed;
		float dotsUsed = currentSpeedPercentageOfMaxSpeed * gaugeDots;
		float pseudoAngleOfIndicator = dotsUsed * angleBetweenTwoDots;
		float angleOfIndicator = 0;
		if (currentSpeed != 0) {
			if (pseudoAngleOfIndicator <= 115) {
				angleOfIndicator = 115 - pseudoAngleOfIndicator;
			} else {
				angleOfIndicator = -pseudoAngleOfIndicator + 115;
			}
		} else {
			angleOfIndicator = 115;
		}


		gaugeIndicatorContainer.rotation = Quaternion.Euler(0, 0, angleOfIndicator);
		///float tospSpeed = ((force.magnitude / rb.drag) - Time.fixedDeltaTime * force.magnitude) / rb.mass;
	}*/

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
		

	/*public void OnSettingsButtonClick()
	{
		SceneManager.LoadScene("SettingsScene");
	}*/

}
