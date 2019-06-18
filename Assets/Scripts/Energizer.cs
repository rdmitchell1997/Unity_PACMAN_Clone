//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using UnityEngine;
using System.Collections;

public class Energizer : MonoBehaviour {

    private GameManager gm;

	void Start ()
	{
	    gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if( gm == null )    Debug.Log("Energizer did not find Game Manager!");
	}
    //if pacman collides with energize than make ghosts scared and destoy the dot.
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "pacman")
        {
            gm.ScareGhosts();
            Destroy(gameObject);
        }
    }
}
