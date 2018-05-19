//Author: James Murphy
//Purpose: Control the UI such as highscores and other buttons

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	//Declare this singleton to allow easy access from other scripts
	public static UIManager singleton;
	//Serialized variable to be added easily added in the inspector, whilst staying private
	[SerializeField]
	private GameObject menuUI, gameOverUI, finishUI;
	[SerializeField]
	private Text menuHighscoreText, ingameScoreText;

	private void Awake () //Delay any awake code by 0.02 seconds to let other manager start up
	{
		//Initialise this singleton
		singleton = this;
	}

    private void Update() //This contains controller inputs for the menu
    {
        //Only run when paused as i dont want this code running when in game
        if (Time.timeScale == 0)
        {
            ControllerInputs();
        }
    }

    private void ControllerInputs() //Contains what pressing controller buttons does
    {
        if (Input.GetKey(KeyCode.Joystick1Button0)) //A button
        {
            //Change what the buttons do, dependent on what menu is open
            if (menuUI.activeSelf == true)
            {
                PlayButtonCode();
            }
            else if (gameOverUI.activeSelf == true || finishUI.activeSelf == true)
            {
                RestartButton();
            }
        }
        else if (Input.GetKey(KeyCode.Joystick1Button1)) //B button
        {
            //Change what the buttons do, dependent on what menu is open
            if (gameOverUI.activeSelf == true || finishUI.activeSelf == true)
            {
                ExitButton();
            }
        }
    }

	public void InitialiseUI () //Get the current highscore and display it by being called from the score manager script
	{
		if (menuHighscoreText != null && ScoreManager.singleton != null)
		{
			menuHighscoreText.text = "Highscore: " + ScoreManager.singleton.ReturnHighScore ();
		}
		//Set the timescale to zero to make sure the game doesn't run whilst at the menu
		Time.timeScale = 0;
	}

	private void FixedUpdate () //Keep the score text updated
	{
		if (ingameScoreText != null && ScoreManager.singleton != null)
		{
			ingameScoreText.text = "Score: " + ScoreManager.singleton.ReturnCurrentScore ();
		}
	}

	public void PlayButtonCode () //This will start timescale and hide the start ui when called
	{
		if (menuUI != null)
		{
			menuUI.SetActive (false);
			Time.timeScale = 1;
			Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
		}
	}

	public void RestartButton () //This will restart this scene
	{
        Time.timeScale = 1;
        ScoreManager.singleton.SaveHighScore();
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void OpenGameOverUI () //This will open the game over ui and pause the game
	{
		if (gameOverUI != null)
		{
			gameOverUI.SetActive (true);
			Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
	}

	public void OpenFinishUI () //This will open the finish ui and pause the game
	{
		finishUI.SetActive (true);
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

	public void ExitButton () //This will exit the game
	{
		Application.Quit ();
	}
}