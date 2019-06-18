﻿//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour
{

    public float speed;

	void Start () 
    {
	    Destroy(gameObject, 1.5f);
	}
	
	
	void Update () 
    {
	    transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
	    speed -= 0.01f;
    }
}
