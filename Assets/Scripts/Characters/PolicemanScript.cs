using UnityEngine;
using System.Collections;

public class PolicemanScript : MonoBehaviour {

	public enum colors{Blue, Green, Red, Orange, Yellow};
	public colors visionColor;
	
	// Use this for initialization
	void Start () {
		this.transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load("Materials/ColorWMediumTransparency/" + visionColor + "_MT", typeof(Material)) as Material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown()
	{
		this.SelectUnit();
	}
	
	public void activate()
	{
		
		this.transform.GetChild(2).GetComponent<LoopScaleScript>().activate();
	}
	
	public void deactivate()
	{
		// deactivate the torus scale changing
		this.transform.GetChild(2).GetComponent<LoopScaleScript>().deactivate();
	}

	public void SelectUnit() {
		// Tell the squad object (parent) to select this object
		this.transform.parent.GetComponent<SquadManagerScript>().switchCurrentCop(this);
	}
}
