//Author: James Murphy
//Purpose: To move the dragon towards the player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class DragonMovement : MonoBehaviour
{
	private Rigidbody thisRB;
	[Header ("Movement Variables")]
	[SerializeField]
	private float accelerationSpeed = 2;
	[SerializeField]
	private float maxSpeed = 30f, turningSpeed = 6;
	private Transform player;

	private void Awake () //Configure and get any components
	{
		if (GetComponent<Rigidbody> () != null)
		{
			thisRB = GetComponent<Rigidbody> ();
			thisRB.useGravity = false;
		}
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	private void FixedUpdate () //All movement code will be done in fixed update in order to make sure physics is independent of frame rate
	{
		if (thisRB != null)
		{
			//Look at the player at all times
			LookAtPlayer ();
			//Move towards the player at all times
			MoveTowardsPlayer ();
		}
	}

	private void LookAtPlayer () //This code will force the dragon to look at the player at all times at a certain speed
	{
		//Look at the player
		Quaternion dragonRotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, dragonRotation, (Time.fixedDeltaTime * turningSpeed));
	}

	private void MoveTowardsPlayer () //Move towards the player at all times
	{
		if (thisRB.velocity.magnitude < maxSpeed)
		{
			thisRB.AddForce (transform.forward * accelerationSpeed);
		}
	}
}