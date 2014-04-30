using UnityEngine;
using System.Collections;

public class SquadManagerScript : MonoBehaviour {

	private GameObject[] listOfCops = new GameObject[5];
	private GameObject currentCop;
	
	// Use this for initialization
	void Start () {
		
		int i; 
		
		for(i = 0; i < 5; ++i)
		{
			listOfCops[i] = this.transform.GetChild(i).gameObject;
		}
		
		currentCop = null;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void switchCurrentCop(PolicemanScript cop)
	{
		int i;
		
		currentCop = cop.gameObject;
		
		for(i = 0; i < 5; ++i)
		{
			if(listOfCops[i] != currentCop)
				listOfCops[i].GetComponent<PolicemanScript>().deactivate();
		}
		
		currentCop.GetComponent<PolicemanScript>().activate();
	}
	
	public void setDestinationForCop(Vector3 destination)
	{
		if(currentCop != null)
		{
			GameObject direction = (GameObject)Instantiate(Resources.Load("DirectionPoint"));
			
			direction.transform.position = destination;
			
			Material currentCopMaterial = currentCop.GetComponent<Renderer>().material;
			
			//Resources.Load("Materials/Blue", typeof(Material)) as Material;
			
			direction.transform.GetChild(4).GetComponent<Renderer>().material = currentCopMaterial;
			
			currentCop.GetComponent<NavMeshScript>().setTarget(direction);
		}
	}
	
	public void unselectCop()
	{
		int i;
		
		currentCop = null;
		
		for(i = 0; i < 5; ++i)
		{
			listOfCops[i].GetComponent<PolicemanScript>().deactivate();
		}
	}
}
