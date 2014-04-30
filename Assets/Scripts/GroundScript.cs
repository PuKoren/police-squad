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
		
		if(Input.GetMouseButtonDown(leftButton))
		{
			squad.GetComponent<SquadManagerScript>().unselectCop();
		}
	}
}
