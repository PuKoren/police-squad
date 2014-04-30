using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour {

	private int leftButton = 0;
	private int rightButton = 1;
	private float rodentRange = 250.0f;
	private string tagger = "Ground";
	
	public SquadManagerScript squad;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseOver()
	{
		// Get the right click, and if a cop is selected the click of the moouse will determine the next destination for the cop
		if(Input.GetMouseButtonDown(rightButton))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, rodentRange)){
				if(hit.transform.CompareTag(tagger)){
					
					Vector3 click = new Vector3(hit.point.x, hit.point.y, hit.point.z);
					
					squad.GetComponent<SquadManagerScript>().setDestinationForCop(click);
					 
				}
			}
		}
		
		// Unselect a cop if one is selected
		if(Input.GetMouseButtonDown(leftButton))
		{
			squad.GetComponent<SquadManagerScript>().unselectCop();
		}
	}
}
