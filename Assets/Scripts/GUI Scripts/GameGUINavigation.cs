//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using System;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUINavigation : MonoBehaviour {

	//------------------------------------------------------------------
	// Variable declarations
	
	private bool paused;
    private bool quit;

	public float initialDelay;

	// canvas
	public Canvas PauseCanvas;
	public Canvas QuitCanvas;
	public Canvas ReadyCanvas;
	public Canvas ScoreCanvas;
    public Canvas GameOverCanvas;
	
	// buttons
	public Button MenuButton;

	//------------------------------------------------------------------
	// Function Definitions

	void Start () 
	{
		StartCoroutine("ShowReadyScreen", initialDelay);
	}
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quit == true)
                ToggleQuit();
            else
                TogglePause();
        }
	}

	// public handle to show ready screen coroutine call
	public void H_ShowReadyScreen()
	{
		StartCoroutine("ShowReadyScreen", initialDelay);
	}

    public void H_ShowGameOverScreen()
    {
        StartCoroutine("ShowGameOverScreen");
    }

	IEnumerator ShowReadyScreen(float seconds)
	{
		GameManager.gameState = GameManager.GameState.Init;
		ReadyCanvas.enabled = true;
		yield return new WaitForSeconds(seconds);
		ReadyCanvas.enabled = false;
		GameManager.gameState = GameManager.GameState.Game;
	}

    IEnumerator ShowGameOverScreen()
    {
        Debug.Log("Showing GAME OVER Screen");
        GameOverCanvas.enabled = true;
        yield return new WaitForSeconds(2);
        Menu();
    }

	public void getScoresMenu()
	{
		Time.timeScale = 0f;		// stop the animations
		GameManager.gameState = GameManager.GameState.Scores;
		MenuButton.enabled = false;
		ScoreCanvas.enabled = true;
	}

	//------------------------------------------------------------------
	// Button functions

	public void TogglePause()
	{
		// if paused before key stroke, unpause the game
		if(paused)
		{
			Time.timeScale = 1;
			PauseCanvas.enabled = false;
			paused = false;
			MenuButton.enabled = true;
		}
		
		// if not paused before key stroke, pause the game
		else
		{
			PauseCanvas.enabled = true;
			Time.timeScale = 0.0f;
			paused = true;
			MenuButton.enabled = false;
		}


        Debug.Log("PauseCanvas enabled: " + PauseCanvas.enabled);
	}
	
	public void ToggleQuit()
	{
		if(quit)
        {
            PauseCanvas.enabled = true;
            QuitCanvas.enabled = false;
			quit = false;
		}
		
		else
        {
            QuitCanvas.enabled = true;
			PauseCanvas.enabled = false;
			quit = true;
		}
	}

	public void Menu()
	{
        SceneManager.LoadScene("menu");
		Time.timeScale = 1.0f;

        // destroy the gamemanager if we return to the menu.
	    GameManager.DestroySelf();
	}

    public void LoadLevel()
    {
        //load the game
        GameManager.Level++;
        SceneManager.LoadScene("game");
    }
}
