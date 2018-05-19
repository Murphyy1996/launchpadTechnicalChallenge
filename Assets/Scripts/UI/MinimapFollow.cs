//Author: James Murphy
//Purpose: Follow the player on the the current all axis except y

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
	//This is the transform for the player
	private Transform player;

	private void Awake () //Get the player transform
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	private void FixedUpdate () //Follow the player on all axis except for y
	{
		//Make sure there is a player reference
		if (player != null)
		{
			//This will ensure the minimap holder will only follow the player on x and z and ignore y
			transform.position = new Vector3 (player.position.x, transform.position.y, player.position.z);
		}
	}
}