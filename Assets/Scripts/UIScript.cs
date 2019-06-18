﻿//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public int score;

	public List<Image> lives = new List<Image>(3);

	Text txt_score, txt_level;
	
	// Use this for initialization
	void Start () 
	{
        txt_score = GetComponentsInChildren<Text>()[0];
        txt_level = GetComponentsInChildren<Text>()[1];

	    for (int i = 0; i < 3 - GameManager.lives; i++)
	    {
	        Destroy(lives[lives.Count-1]);
            lives.RemoveAt(lives.Count-1);
	    }
	}
	
	void Update () 
	{

        // update score text
        score = GameManager.score;
		txt_score.text = "Score\n" + score;
	    txt_level.text = "Level\n" + (GameManager.Level + 1);

	}


}
