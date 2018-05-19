//Author: James Murphy
//Purpose: To trigger the victory state 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider))]
public class FinishLine : MonoBehaviour
{
	private void Start () //Set any collider as a trigger
	{
		GetComponent<Collider> ().isTrigger = true;
	}
	private void OnTriggerEnter (Collider otherObj) //If the player collides into this object, activate the win screen
	{
		//If the other object is player
		if (otherObj.tag == "Player")
		{
			if (UIManager.singleton != null)
			{
				//Open the finish game ui
				UIManager.singleton.OpenFinishUI ();
			}
		}
	}
}