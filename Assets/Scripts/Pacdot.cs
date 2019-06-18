//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using UnityEngine;
using System.Collections;

public class Pacdot : MonoBehaviour {

    //A simple collison trigger check if pacman collides with a dot, destroy it and add score.

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "pacman")
		{
			GameManager.score += 10;
		    GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");
            Destroy(gameObject);

            //if only 1 dot remains restart the level.
		    if (pacdots.Length == 1)
		    {
		        GameObject.FindObjectOfType<GameGUINavigation>().LoadLevel();
		    }
		}
	}
}
