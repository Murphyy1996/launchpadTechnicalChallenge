//Author: James Murphy
//Purpose: To make it so the dragon kills the player upon impact

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonKillScript : MonoBehaviour
{
	private void OnTriggerEnter (Collider otherObj) //When the player touches this obj, go to the game over screen
	{
		//if the other object is the player
		if (otherObj.tag == "Player")
		{
			//And the ui manager exists..
			if (UIManager.singleton != null)
			{
				//Open the game over screen
				UIManager.singleton.OpenGameOverUI ();
			}
		}
	}
}