//Author: James Murphy
//Purpose: Control the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make sure this script creations a rigidbody
[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	//Variables for movement
	private Rigidbody thisRB;
	[Header ("Speed Variables")]
	[SerializeField]
	private float accelerationValue = 0.8f;
	[SerializeField]
	private float minSpeed = 5f, maxSpeed = 20f, drag = 2f;
	private float targetSpeed = 15f;
	[SerializeField]
	private float currentSpeed;
	[Header ("Collision Variables")]
	[SerializeField]
	private float bounceAmount = 5;
	[SerializeField]
	private float stunDuration = 1f, stunCooldown = 1f;
	private float stunCounter = 0;
	private bool isStunned = false, allowedToStun = false;
	[Header ("Roll Variables")]
	[SerializeField]
	private float rollSpeed = 4f;
	//Input variables
	private float horizontalAxisInputValue = 0;
	//Pitch and yaw variables
	[Header ("Pitch and Yaw Variables")]
	[SerializeField]
	private float pitchSpeed = 2f;
	[SerializeField]
	private float yawSpeed = 1f;

	private void Awake () //Configure the rigidbody etc
	{
		//Get this rigidbody
		thisRB = GetComponent<Rigidbody> ();
		//Configure the rigidbody
		thisRB.drag = drag;
		thisRB.angularDrag = drag;
		thisRB.useGravity = false;
		//Do not allow stun until the player has sufficiently gained base speed
		StartCoroutine (AllowStunAgain ());
	}

	private void Update () //Control code will be run in update to get key presses accurately
	{
		if (thisRB != null)
		{
			ControlInputCode ();
		}
	}

	private void FixedUpdate () //Run code such as movement in the fixed update
	{
		//This essentialyl contains the look code but is integral to movement
		PitchAndYawCode ();
		//Only run the following if the player isn't stunned
		if (isStunned == false)
		{
			SpeedMovement ();
			RollCode ();
		}
		//Only check if stunned after a certain amount of time (lets the player get speed back up)
		StunCode ();
	}

	private void ControlInputCode () //This contains the code for control inputs
	{
		//Keyboard input
		if (Input.GetJoystickNames ().Length == 0)
		{
			//This is for the speed value
			float keyboardAxis = Input.GetAxis ("Vertical");
			targetSpeed = targetSpeed + keyboardAxis;
			//This is for the roll value
			horizontalAxisInputValue = Input.GetAxis ("Horizontal");
		}
		else //Controller input
		{
            //This is for the speed value
            float controllerAxis = -Input.GetAxis("C1LeftJoyY");
            targetSpeed = targetSpeed + controllerAxis;
            //This is for the roll value
            horizontalAxisInputValue = 0;
            if (Input.GetKey(KeyCode.Joystick1Button5))
            {
                horizontalAxisInputValue = 1;
            }
            else if (Input.GetKey(KeyCode.Joystick1Button4))
            {
                horizontalAxisInputValue = -1;
            }
        }
	}

	private void StunCode () //if the player isn't moving, turn on gravity and bounce backwards for the specified time
	{
		//Only do this is stun is enabled
		if (allowedToStun == true)
		{
			if (thisRB != null) //Make sure the rigidbody reference is available
			{
				//If the player isn't stunned.. check if they fulfill the stun conditions
				if (isStunned == false)
				{
					if (thisRB.velocity.magnitude < 1) //if the player is going super slow, bounce off (I have done it this way as the only time the player goes this slow is when they hit something)
					{
						//Mark the player as stunned and enable gravity
						isStunned = true;
						thisRB.useGravity = true;
						thisRB.AddForce (-transform.forward * (bounceAmount));
					}
				}
				else //If the player is stunned increase the stun counter
				{
					stunCounter = stunCounter + Time.fixedDeltaTime;
					//Exit the stun if the player has been in it for the required time
					if (stunCounter > stunDuration)
					{
						//Allow gravity and allow the player to gain speed again
						thisRB.useGravity = false;
						isStunned = false;
						allowedToStun = false;
						stunCounter = 0;
						//Enable stun after a certain amount of time again
						StartCoroutine (AllowStunAgain ());
					}
				}
			}
		}
	}

	private void SpeedMovement () //This contains the movement code and move the object in the direction it is facing
	{
		if (thisRB != null)
		{
			//Make sure the target speed stays between the boundries
			targetSpeed = Mathf.Clamp (targetSpeed, minSpeed, maxSpeed);

			//Track the current speed
			currentSpeed = thisRB.velocity.magnitude;

			if (thisRB.velocity.magnitude < targetSpeed)
			{
				//Accelerate at the instructed speed
				thisRB.AddForce (transform.forward * accelerationValue);
			}
		}
	}

	private void RollCode () //Perform roll code based off the horizontal axis
	{
		//Rotate on the z axis
		transform.Rotate (0, 0, (-horizontalAxisInputValue * rollSpeed));
	}

	private void PitchAndYawCode () //Performs the pitch and yaw code
	{
		if (thisRB != null)
		{
            //By default use mouse and kb controls
            Vector3 pitchYawInput = new Vector3(Input.GetAxis("Mouse Y") *-pitchSpeed, Input.GetAxis("Mouse X") * yawSpeed, 0);
            if (Input.GetJoystickNames().Length != 0) //For controller support
            {
                //Make sure mouse input is not being used
                pitchYawInput = Vector3.zero;
                //Rotate the players horizontal axis
                pitchYawInput = new Vector3(Input.GetAxis("C1RightJoyY") * pitchSpeed, Input.GetAxis("C1RightJoyX") * yawSpeed, 0);
            }
            //Perform the pitch and yaw the input
            transform.Rotate(pitchYawInput);
        }
	}

	private IEnumerator AllowStunAgain () //After a certain amount of time allow stun again
	{
		yield return new WaitForSeconds (stunCooldown);
		allowedToStun = true;
	}
}