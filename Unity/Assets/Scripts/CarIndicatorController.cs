using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarIndicatorController : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject parentContainer;
	float topSpeed;

	#region Singleton 
	public static CarIndicatorController instance;
	#endregion

	public bool AccelerationEnabled { get; set; }
	public bool BrakeEnabled { get; set; }
	public Quaternion TargetRotation { get;set; }


	// Set in Unity Editor
	public float linearAcceleration;
	public float angularAcceleration;

	void Awake()
	{
		instance = this;
	}
		

	void Start () 
	{
		parentContainer = transform.parent.gameObject;
		rb = parentContainer.GetComponent<Rigidbody2D> ();

		AccelerationEnabled = false;
		BrakeEnabled = false;
	}

	void FixedUpdate()
	{
		Vector2 force = new Vector2();
		if (AccelerationEnabled) {
			force = Vector2.up * linearAcceleration * Time.deltaTime;
			rb.AddRelativeForce (force);
			topSpeed = ((force.magnitude / rb.drag) - Time.fixedDeltaTime * force.magnitude) / rb.mass;
		}
		if (BrakeEnabled) {
			force = -Vector2.up * linearAcceleration * Time.deltaTime;
			rb.AddRelativeForce (force);
			topSpeed = ((force.magnitude / rb.drag) - Time.fixedDeltaTime * force.magnitude) / rb.mass;
		}
	}

	void Update()
	{
		float step = angularAcceleration * Time.deltaTime;
		parentContainer.transform.rotation = Quaternion.RotateTowards (transform.rotation, TargetRotation, step);
	}

	public float GetTopSpeed()
	{
		return topSpeed;
	}

	public Vector2 GetCurrentVelocity()
	{
		return rb.velocity;
	}
}
