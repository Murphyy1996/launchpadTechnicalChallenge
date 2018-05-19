//Author: James Murphy
//Purpose: To accumulate the score total and hold all functions regarding the score

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	//Singleton variable to make this accesible from other scripts
	public static ScoreManager singleton;
	//Score variables
	private int currentScore = 0, highScore = 0;

	private void Awake () //Initialise this singleton and any values
	{
		//This line makes this script accesible from other scripts
		singleton = this;
		//This names the object as the score manager
		this.gameObject.name = "Score Manager";
		//This will load the current highscore
		LoadHighScore ();
        //Intialise the UI
        UIManager.singleton.InitialiseUI();
	}

	public void IncreaseScore (int amount) //Increase the score by the specified amount
	{
		currentScore = currentScore + amount;
	}

	private void LoadHighScore () //Load the highscore from the saved player preferences
	{
		//If there is an existing highscore...
		if (PlayerPrefs.HasKey ("highscore") == true)
		{
			//...Load the existing high score
			highScore = PlayerPrefs.GetInt ("highscore");
		}
	}

	public void SaveHighScore () //Set the new highscore and save to player preferences
	{
		//If the current score is bigger than the high score variable
		if (currentScore > highScore)
		{
			//Save the highscore to player preferences
			PlayerPrefs.SetInt ("highscore", currentScore);
		}
	}

	public int ReturnCurrentScore () //This will return the current score
	{
		return currentScore;
	}

    public int ReturnHighScore() //This will return the high score
    {
        return highScore;
    }
}