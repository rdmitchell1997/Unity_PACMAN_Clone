//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed = 0.4f;
    Vector2 destination = Vector2.zero;
    Vector2 direction = Vector2.zero;
    Vector2 nextDirection = Vector2.zero;

    //public class to get the direction from other scripts.
    public Vector2 getDir()
    {
        return direction;
    }
    //The Serializable field allows for this data to be saved and loaded across scenes.
    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }

    public PointSprites points;

    public static int killstreak = 0;

    // script handles
    private GameGUINavigation GUINav;
    private GameManager GM;

    private bool _deadPlaying = false;

    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GUINav = GameObject.Find("UI Manager").GetComponent<GameGUINavigation>();
        destination = transform.position;
    }

    void FixedUpdate()
    {
        //Checking the game state through a switch case, this could be simplified to a bool figure but at this point a switch case allows for growth in future.
        switch (GameManager.gameState)
        {
            case GameManager.GameState.Game:
                ReadInputAndMove();
                Animate();
                break;

            case GameManager.GameState.Dead:
                if (!_deadPlaying)
                    StartCoroutine("PlayDeadAnimation");
                break;
        }


    }

    IEnumerator PlayDeadAnimation()
    {
        _deadPlaying = true;
        GetComponent<Animator>().SetBool("Die", true);
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("Die", false);
        _deadPlaying = false;

        if (GameManager.lives <= 0)
        {
            GUINav.H_ShowGameOverScreen();
        }

        else
            GM.ResetScene();
    }

    void Animate()
    {
        Vector2 dir = destination - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination()
    {
        destination = new Vector2(15f, 11f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void ReadInputAndMove()
    {
        // move closer to destination
        Vector2 p = Vector2.MoveTowards(transform.position, destination, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // get the next direction from input
        if (Input.GetAxis("Horizontal") > 0) nextDirection = Vector2.right;
        if (Input.GetAxis("Horizontal") < 0) nextDirection = Vector2.left;
        if (Input.GetAxis("Vertical") > 0) nextDirection = Vector2.up;
        if (Input.GetAxis("Vertical") < 0) nextDirection = Vector2.down;

        // if pacman is in the center of a tile
        if (Vector2.Distance(destination, transform.position) < 0.00001f)
        {
            if (Valid(nextDirection))
            {
                destination = (Vector2)transform.position + nextDirection;
                direction = nextDirection;
            }
            else   // if next direction is not valid
            {
                if (Valid(direction))  // and the prev. direction is valid
                    destination = (Vector2)transform.position + direction;   // continue on that direction

                // otherwise, do nothing
            }
        }
    }

    public void UpdateScore()
    {
        killstreak++;

        // limit killstreak at 4
        if (killstreak > 4) killstreak = 4;

        Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        GameManager.score += (int)Mathf.Pow(2, killstreak) * 100;

    }
}
