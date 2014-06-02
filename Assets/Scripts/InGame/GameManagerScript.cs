using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	
	private int turn;
	private int round;
	private bool displayInfo;
	private int frameCounter;
	public int timer;
	
	private GameObject squad;
	
	// Use this for initialization
	void Start () {
		turn = 1;
		round = 1;
		
		displayInfo = true;
		frameCounter = 0;
		
		// Hide the messages
		this.transform.GetChild(0).GetComponent<TextMesh>().text = "";
		this.transform.GetChild(1).GetComponent<TextMesh>().text = "";
		
		squad = GameObject.FindGameObjectWithTag("Squad");
		
		squad.GetComponent<SquadManagerScript>().deactivateCopsFieldOfView();
	}
	
	// Update is called once per frame
	void Update () {
		
		// If the user press "ENTER" and the current turn is 1, then the turn 2 starts
		if(Input.GetKeyDown(KeyCode.Return) && turn == 1)
		{
			frameCounter = 0;
			++turn;
			
			/* During the turn 2:
				- The units are allowed to move
				- None of the units are selected
				- It's impossible to select an unit
				- The cops field of view are activated so they can see enemies
			*/
			squad.GetComponent<SquadManagerScript>().makeUnitsMove();
			squad.GetComponent<SquadManagerScript>().unselectCop();
			squad.GetComponent<SquadManagerScript>().allowToSelectUnit(false);
			squad.GetComponent<SquadManagerScript>().activateCopsFieldOfView();
			
			displayInfo = true;
		}
		
		// If the units have finished their paths
		if(squad.GetComponent<SquadManagerScript>().hasTheTeamFinishedToMove() && turn == 2)
		{
			turn = 1;
			++round;
			
			displayInfo = true;
			frameCounter = 0;
			
			/* During the turn 1:
				- The units can't move
				- The user can select units
				- The counters of actions per units are reset
				- Cops field of view are deactivated, so the player can click through them
			*/
			squad.GetComponent<SquadManagerScript>().makeUnitsStopMoving();
			squad.GetComponent<SquadManagerScript>().allowToSelectUnit(true);
			squad.GetComponent<SquadManagerScript>().resetListOfActionsCounter();
			squad.GetComponent<SquadManagerScript>().deactivateCopsFieldOfView();
		}
		
		// Display messages
		if(displayInfo)
		{
			// Until the the frameCounter is lower than timer, the messages will be displayed
			this.transform.GetChild(0).GetComponent<TextMesh>().text = "Round " + round;
			this.transform.GetChild(1).GetComponent<TextMesh>().text = "Turn " + turn;
			squad.GetComponent<SquadManagerScript>().allowToSelectUnit(false);
			
			++frameCounter;
			
			if(frameCounter >= timer)
			{
				displayInfo = false;
				this.transform.GetChild(0).GetComponent<TextMesh>().text = "";
				this.transform.GetChild(1).GetComponent<TextMesh>().text = "";
				
				if(turn ==1)
					squad.GetComponent<SquadManagerScript>().allowToSelectUnit(true);
			}
		}
			
	
	}
}
