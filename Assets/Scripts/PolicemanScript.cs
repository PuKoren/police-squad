using UnityEngine;
using System.Collections;

public class PolicemanScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		this.GetComponent<TrailRenderer>().material = this.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown()
	{
		//Debug.Log(this);
		this.transform.parent.GetComponent<SquadManagerScript>().switchCurrentCop(this);
	}
	
	public void activate()
	{
		this.transform.GetChild(0).GetComponent<LoopScaleScript>().activate();
	}
	
	public void deactivate()
	{
		this.transform.GetChild(0).GetComponent<LoopScaleScript>().deactivate();
	}
}
