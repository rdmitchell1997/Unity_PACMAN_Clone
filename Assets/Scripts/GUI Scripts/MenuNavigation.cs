//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {
    //Functions to be called when changing scenes
	public void MainMenu()
	{
		SceneManager.LoadScene("menu");
	}

	public void Quit()
	{
		Application.Quit();
	}
	
	public void Play()
	{
        SceneManager.LoadScene("game");
	}
}
