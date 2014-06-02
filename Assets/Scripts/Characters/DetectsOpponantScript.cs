using UnityEngine;
using System.Collections;

public class DetectsOpponantScript : MonoBehaviour {
	
	public string opponant;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// A cop or an enemy has been seen
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == opponant)
		{
			/**************
			   CODE HERE
			**************/
		}
	}
}
