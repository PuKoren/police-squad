using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshScript : MonoBehaviour {

	private Transform currentTarget;
	private NavMeshAgent agent;
	
	private List<Transform> listTarget = new List<Transform>();
	
	// Use this for initialization
	void Start () {
		
		agent = this.GetComponent<NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// if there are targets in the list but none is selected yet
		if(listTarget.Count > 0 && currentTarget == null)
			setTarget();
		
		// if a target has been defined
		if(currentTarget != null)
		{
			// move the object
			agent.SetDestination(currentTarget.position);
			
			// get the position of the object and the target
			Vector3 objectPos = this.transform.position;
			Vector3 targetPos = currentTarget.position;
			
			objectPos.y = 0.0f;
			targetPos.y = 0.0f;
			
			// if the object has reached the target, the directionPoint is destroyed, the target is removed  from the list
			if(Vector3.Distance(objectPos, targetPos) <=1)
			{
				DestroyImmediate(listTarget[0].gameObject);
				listTarget.RemoveAt(0);
				currentTarget = null;
			}
			
			this.transform.GetChild(1).GetComponent<PathTracerScript>().deleteLine(objectPos);
		}
	}
	
	public void addTarget(GameObject pos)
	{
		listTarget.Add(pos.transform);
		
		this.transform.GetChild(1).GetComponent<PathTracerScript>().addCheckpoint(pos);
	}
	
	private void setTarget()
	{
		currentTarget = listTarget[0];
	}
}
