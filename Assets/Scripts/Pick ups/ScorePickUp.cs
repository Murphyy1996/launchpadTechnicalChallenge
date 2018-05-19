//Author: James Murphy
//Purpose: To increase the score when collided with etc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickUp : MonoBehaviour
{
	[SerializeField]
	private int pickUpValue = 5;
	private void Start () //Configure this collider on start
	{
		//Get this collider if it exists
		if (GetComponent<Collider> () != null)
		{
			Collider thisCollider = GetComponent<Collider> ();
			//Make sure it is a trigger
			thisCollider.isTrigger = true;
		}
	}

	private void OnTriggerEnter (Collider otherObj) //If the player has collided with this...
	{
		if (otherObj.tag == "Player")
		{
			//Increase the score
			if (ScoreManager.singleton != null)
			{
				ScoreManager.singleton.IncreaseScore (pickUpValue);
                //Turn off this object as its no longer needed
                this.gameObject.SetActive(false);
			}
		}
	}
}