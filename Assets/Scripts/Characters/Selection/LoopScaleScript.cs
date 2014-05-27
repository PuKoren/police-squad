/*
SCRIPT TO CHANGE THE SCALE OF THE "SELECTION CIRCLE" ON THE CURRENT COP
*/

using UnityEngine;
using System.Collections;

public class LoopScaleScript : MonoBehaviour {

	public float min;
	public float max;
	public float increment;
	
	private bool isGrowing;
	private bool activated;
	
	// Use this for initialization
	void Start () {
		isGrowing = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(activated)
		{
			float step = increment * Time.deltaTime;
			
			if(isGrowing)
			{
				if(this.transform.localScale.x < max)
					this.transform.localScale = new Vector3(this.transform.localScale.x + step, this.transform.localScale.y, this.transform.localScale.z + step);
				else
					isGrowing = false;
			}
			else
			{
				if(this.transform.localScale.x > min)
					this.transform.localScale = new Vector3(this.transform.localScale.x - step, this.transform.localScale.y, this.transform.localScale.z - step);
				else
					isGrowing = true;
			}
		}
	}
	
	public void activate()
	{
		this.GetComponent<MeshRenderer>().enabled = true;
		activated = true;
	}
	
	public void deactivate()
	{
		this.GetComponent<MeshRenderer>().enabled = false;
		activated = false;
	}
}
