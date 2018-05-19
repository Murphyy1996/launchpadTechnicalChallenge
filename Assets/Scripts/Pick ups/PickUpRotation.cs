//Author: James Murphy
//Purpose: Rotate the object at the desired speed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotation : MonoBehaviour
{
	//Rotation variables
	[SerializeField]
	[Range (0, 100)]
	private float rotationSpeed = 25;
	[SerializeField]
	private axis localRotationAxis = axis.x;
	private enum axis { x, y, z };

 private void Start ()
 {

	}

	private void FixedUpdate () //Rotate the obj at the request speed and axis
	{
		//Decide what axis to use
		Vector3 rotationAxis = Vector3.zero;
		switch (localRotationAxis)
		{
			case axis.x:
				rotationAxis = transform.right;
				break;
			case axis.y:
				rotationAxis = transform.up;
				break;
			case axis.z:
				rotationAxis = transform.forward;
				break;
		}
		//Rotate on the desired axis
		transform.Rotate (rotationAxis * (rotationSpeed * Time.fixedDeltaTime), Space.Self);
	}
}