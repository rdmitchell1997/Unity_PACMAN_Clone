﻿//ROBERT MITCHELL
//PAC MAN CLONE
//UPDATED: 18/06/2019
//https://robert-mitchell.myportfolio.com/
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AI : MonoBehaviour {

	public Transform target;

    //making reference to tileManagers and its tiles.
	private List<TileManager.Tile> tiles = new List<TileManager.Tile>();
	private TileManager manager;
	public GhostMove ghost;

	public TileManager.Tile nextTile = null;
	public TileManager.Tile targetTile;
	TileManager.Tile currentTile;

	void Awake()
	{
		manager = GameObject.Find("Game Manager").GetComponent<TileManager>();
		tiles = manager.tiles;

        //write error to log if not present could also use Assertions to stop playback if not present.
        if (ghost == null)	Debug.Log ("game object ghost not found");
		if(manager == null)	Debug.Log ("game object Game Manager not found");
	}

	public void AILogic()
	{
		// get current tile
		Vector3 currentPos = new Vector3(transform.position.x + 0.499f, transform.position.y + 0.499f);
		currentTile = tiles[manager.Index ((int)currentPos.x, (int)currentPos.y)];
		
		targetTile = GetTargetTilePerGhost();
		
		// get the next tile according to direction
		if(ghost.direction.x > 0)	nextTile = tiles[manager.Index ((int)(currentPos.x+1), (int)currentPos.y)];
		if(ghost.direction.x < 0)	nextTile = tiles[manager.Index ((int)(currentPos.x-1), (int)currentPos.y)];
		if(ghost.direction.y > 0)	nextTile = tiles[manager.Index ((int)currentPos.x, (int)(currentPos.y+1))];
		if(ghost.direction.y < 0)	nextTile = tiles[manager.Index ((int)currentPos.x, (int)(currentPos.y-1))];
		
		if(nextTile.occupied || currentTile.isIntersection)
		{
			//---------------------
			// If wall is touched
			if(nextTile.occupied && !currentTile.isIntersection)
			{
				// try to move left or right if wall in way try to move up or down
				if(ghost.direction.x != 0)
				{
					if(currentTile.down == null)	ghost.direction = Vector3.up;
					else 							ghost.direction = Vector3.down;
					
				}

                // try to move up or down if wall in way try to move left or right
                else if (ghost.direction.y != 0)
				{
					if(currentTile.left == null)	ghost.direction = Vector3.right; 
					else 							ghost.direction = Vector3.left;
					
				}
				
			}
			
			//---------------------------------------------------------------------------------------
			// If ghost is at intersection
			// we calculate the distance to pacman from each available tile and choose the shortest one.
			if(currentTile.isIntersection)
			{
				
				float dist1, dist2, dist3, dist4;
				dist1 = dist2 = dist3 = dist4 = 999999f;
				if(currentTile.up != null && !currentTile.up.occupied && !(ghost.direction.y < 0)) 		dist1 = manager.distance(currentTile.up, targetTile);
				if(currentTile.down != null && !currentTile.down.occupied &&  !(ghost.direction.y > 0)) 	dist2 = manager.distance(currentTile.down, targetTile);
				if(currentTile.left != null && !currentTile.left.occupied && !(ghost.direction.x > 0)) 	dist3 = manager.distance(currentTile.left, targetTile);
				if(currentTile.right != null && !currentTile.right.occupied && !(ghost.direction.x < 0))	dist4 = manager.distance(currentTile.right, targetTile);
				
				float min = Mathf.Min(dist1, dist2, dist3, dist4);
				if(min == dist1) ghost.direction = Vector3.up;
				if(min == dist2) ghost.direction = Vector3.down;
				if(min == dist3) ghost.direction = Vector3.left;
				if(min == dist4) ghost.direction = Vector3.right;
				
			}
			
		}
		
		// if there is no decision to be made, designate next waypoint for the ghost
		else
		{
			ghost.direction = ghost.direction;
		}
	}

	public void RunLogic()
	{
		// get current tile
		Vector3 currentPos = new Vector3(transform.position.x + 0.499f, transform.position.y + 0.499f);
		currentTile = tiles[manager.Index ((int)currentPos.x, (int)currentPos.y)];

		// get the next tile according to direction
		if(ghost.direction.x > 0)	nextTile = tiles[manager.Index ((int)(currentPos.x+1), (int)currentPos.y)];
		if(ghost.direction.x < 0)	nextTile = tiles[manager.Index ((int)(currentPos.x-1), (int)currentPos.y)];
		if(ghost.direction.y > 0)	nextTile = tiles[manager.Index ((int)currentPos.x, (int)(currentPos.y+1))];
		if(ghost.direction.y < 0)	nextTile = tiles[manager.Index ((int)currentPos.x, (int)(currentPos.y-1))];

		if(nextTile.occupied || currentTile.isIntersection)
		{
			//---------------------
			// If ghost hits wall
			if(nextTile.occupied && !currentTile.isIntersection)
			{
				// if there is a wall to the left and right move up or down.
				if(ghost.direction.x != 0)
				{
					if(currentTile.down == null)	ghost.direction = Vector3.up;
					else 							ghost.direction = Vector3.down;
					
				}
				
				// visa versa from above.
				else if(ghost.direction.y != 0)
				{
					if(currentTile.left == null)	ghost.direction = Vector3.right; 
					else 							ghost.direction = Vector3.left;
					
				}
				
			}
			
			//---------------------------------------------------------------------------------------
			// If we are at intersection
			// choose one available option at random
			if(currentTile.isIntersection)
			{
				List<TileManager.Tile> availableTiles = new List<TileManager.Tile>();
				TileManager.Tile chosenTile;
				if(currentTile.up != null && !currentTile.up.occupied && !(ghost.direction.y < 0)) 			availableTiles.Add (currentTile.up);
				if(currentTile.down != null && !currentTile.down.occupied &&  !(ghost.direction.y > 0)) 	availableTiles.Add (currentTile.down);	
				if(currentTile.left != null && !currentTile.left.occupied && !(ghost.direction.x > 0)) 		availableTiles.Add (currentTile.left);
				if(currentTile.right != null && !currentTile.right.occupied && !(ghost.direction.x < 0))	availableTiles.Add (currentTile.right);

				int rand = Random.Range(0, availableTiles.Count);
				chosenTile = availableTiles[rand];
				ghost.direction = Vector3.Normalize(new Vector3(chosenTile.x - currentTile.x, chosenTile.y - currentTile.y, 0));
			}
			
		}
		
		// if there is no decision to be made, designate next waypoint for the ghost
		else
		{
			ghost.direction = ghost.direction;
		}
	}


	TileManager.Tile GetTargetTilePerGhost()
	{
		Vector3 targetPos;
		TileManager.Tile targetTile;
		Vector3 dir;

		// get the target tile position (round it down to int so we can reach with Index() function)
		switch(name)
		{
		case "blinky":	// target = pacman (move straight towards)
			targetPos = new Vector3 (target.position.x+0.499f, target.position.y+0.499f);
			targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
			break;
		case "pinky":	// target = pacman + 4*pacman's direction (4 steps ahead of pacman)
			dir = target.GetComponent<PlayerController>().getDir();
			targetPos = new Vector3 (target.position.x+0.499f, target.position.y+0.499f) + 4*dir;

			// if pacmans going up, not 4 ahead but 4 up and 4 left is the target
			// so subtract 4 from X coord from target position
			if(dir == Vector3.up)	targetPos -= new Vector3(4, 0, 0);

			targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
			break;
		case "inky":	// target = ambushVector(pacman+2 - blinky) added to pacman+2
			dir = target.GetComponent<PlayerController>().getDir();
			Vector3 blinkyPos = GameObject.Find ("blinky").transform.position;
			Vector3 ambushVector = target.position + 2*dir - blinkyPos ;
			targetPos = new Vector3 (target.position.x+0.499f, target.position.y+0.499f) + 2*dir + ambushVector;
			targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
			break;
		case "clyde":
			targetPos = new Vector3 (target.position.x+0.499f, target.position.y+0.499f);
			targetTile = tiles[manager.Index((int)targetPos.x, (int)targetPos.y)];
			if(manager.distance(targetTile, currentTile) < 9)
				targetTile = tiles[manager.Index (0, 2)];
			break;
		default:
			targetTile = null;
			Debug.Log ("TARGET TILE NOT ASSIGNED");
			break;
		
		}
		return targetTile;
	}
}