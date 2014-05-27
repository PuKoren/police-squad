﻿using UnityEngine;
using System.Collections;

public class SquadManagerScript : MonoBehaviour {

	private GameObject[] listOfCops = new GameObject[5];
	private int[] listOfActionsCounter = new int[5];
	private GameObject currentCop;
	public int nbActionsPerTurn = 2;
	private int currentCopIndex;
	private bool canSelectUnit;
	
	// Use this for initialization
	void Start () {
		
		int i; 
		
		// Add all the cops to the list, and set the counter of their allowed moves (checkpoints) to 0
		for(i = 0; i < 5; ++i)
		{
			listOfCops[i] = this.transform.GetChild(i).gameObject;
			listOfActionsCounter[i] = 0;
		}
		
		currentCop = null;
		
		canSelectUnit = true;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Change the selected cop
	public void switchCurrentCop(PolicemanScript cop)
	{
		if(canSelectUnit)
		{
			int i;
			
			currentCop = cop.gameObject;
			
			// Unselect the cops
			for(i = 0; i < 5; ++i)
			{
				if(listOfCops[i] != currentCop)
					listOfCops[i].GetComponent<PolicemanScript>().deactivate();
				else 
					currentCopIndex = i;
			}
			
			// selected the wanted one
			currentCop.GetComponent<PolicemanScript>().activate();
		}
	}
	
	// add a destination to the selected cop
	public void setDestinationForCop(Vector3 destination)
	{
		if(currentCop != null && listOfActionsCounter[currentCopIndex] < nbActionsPerTurn)
		{
			// Create the checkpoint object and set its position
			GameObject direction = (GameObject)Instantiate(Resources.Load("Prefabs/Checkpoint"));
			
			direction.transform.position = destination;
			
			// get the material of the cop
			Material currentCopMaterial = currentCop.GetComponent<Renderer>().material;
			
			// apply the material of the cop to the checkpoint
			direction.transform.GetChild(4).GetComponent<Renderer>().material = currentCopMaterial;
			
			// add the destination as a target to the cop
			currentCop.GetComponent<NavMeshScript>().addTarget(direction);
			
			// Increase the current number of actions of the cop
			++listOfActionsCounter[currentCopIndex];
		}
	}
	
	// unselect all cops
	public void unselectCop()
	{
		int i;
		
		currentCop = null;
		
		for(i = 0; i < 5; ++i)
		{
			listOfCops[i].GetComponent<PolicemanScript>().deactivate();
		}
	}
	
	// Re-set the counter of actions of all the cops to 0
	public void resetListOfActionsCounter()
	{
		for(int i = 0; i < listOfActionsCounter.Length ; ++i)
		{
			listOfActionsCounter[i] = 0;
		}
	}
	
	// Allow the units to move if they have to
	public void makeUnitsMove()
	{
		for(int i = 0; i < listOfCops.Length; ++i)
		{
			listOfCops[i].GetComponent<NavMeshScript>().setExecuteActions(true);
		}
	}
	
	// Stop the units moving
	public void makeUnitsStopMoving()
	{
		for(int i = 0; i < listOfCops.Length; ++i)
		{
			listOfCops[i].GetComponent<NavMeshScript>().setExecuteActions(false);
		}
	}
	
	// Say if the team has finished to move, therefore all the units have done their actions (completed their paths)
	public bool hasTheTeamFinishedToMove()
	{	
		for(int i = 0; i < listOfCops.Length; ++i)
		{
			if(listOfCops[i].GetComponent<NavMeshScript>().hasFinishedItsPath() == false)
				return false;
		}
		
		return true;
		
	}
	
	// Allow or not to select units in game
	public void allowToSelectUnit(bool allow)
	{
		canSelectUnit = allow;
	}
}
