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
		
		this.transform.GetChild(0).GetComponent<TextMesh>().text = "";
		this.transform.GetChild(1).GetComponent<TextMesh>().text = "";
		
		squad = GameObject.FindGameObjectWithTag("Squad");
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Return) && turn == 1)
		{
			frameCounter = 0;
			++turn;
			
			squad.GetComponent<SquadManagerScript>().makeUnitsMove();
			squad.GetComponent<SquadManagerScript>().unselectCop();
			squad.GetComponent<SquadManagerScript>().allowToSelectUnit(false);
			
			displayInfo = true;
		}
		
		if(squad.GetComponent<SquadManagerScript>().hasTheTeamFinishedToMove() && turn == 2)
		{
			turn = 1;
			++round;
			displayInfo = true;
			frameCounter = 0;
			squad.GetComponent<SquadManagerScript>().makeUnitsStopMoving();
			squad.GetComponent<SquadManagerScript>().allowToSelectUnit(true);
			squad.GetComponent<SquadManagerScript>().resetListOfActionsCounter();
		}
		
		if(displayInfo)
		{
		
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
