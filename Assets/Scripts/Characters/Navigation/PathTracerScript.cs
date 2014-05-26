using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathTracerScript : MonoBehaviour {

	private List<Vector3> listPositions = new List<Vector3>();
	private List<Transform> listCheckpoints = new List<Transform>();
	
	private bool isMoving;
	private NavMeshAgent agent;
	private Transform currentTarget;
	
	private LineRenderer line;
	
	public float distanceToDeleteLine;
	
	// Use this for initialization
	void Start () {
		
		agent = this.GetComponent<NavMeshAgent>();
		
		line = this.GetComponent<LineRenderer>();
		
		string name = this.transform.parent.GetComponent<Renderer>().material.ToString().Substring(0, this.transform.parent.GetComponent<Renderer>().material.ToString().Length - 34);
		
		line.material = Resources.Load("Materials/ColorWHighTransparency/" + name + "_HT", typeof(Material)) as Material;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(listCheckpoints.Count != 0 && currentTarget == null)
			setTarget();
			
		if(currentTarget != null)
		{
			agent.SetDestination(currentTarget.position);
			
			listPositions.Add (this.transform.position);
			
			setLineRendererPositions();
			
			// get the position of the object and the target
			Vector3 objectPos = this.transform.position;
			Vector3 targetPos = currentTarget.position;
			
			objectPos.y = 0.0f;
			targetPos.y = 0.0f;
			
			// if the object has reached the target, the directionPoint is destroyed, the target is removed  from the list
			if(Vector3.Distance(objectPos, targetPos) <=1)
			{
				listCheckpoints.RemoveAt(0);
				currentTarget = null;
			}
		}
	}
	
	public void addCheckpoint(GameObject pos)
	{
		listCheckpoints.Add(pos.transform);
	}
	
	private void setTarget()
	{
		currentTarget = listCheckpoints[0];
	}
	
	public void deleteLine(Vector3 pos)
	{
		if(listPositions.Count > 0)
		{
			if(Vector3.Distance(pos, listPositions[0]) <= distanceToDeleteLine)
			{
				listPositions.RemoveAt(0);
				setLineRendererPositions();
			}	
		}
	}
	
	private void setLineRendererPositions()
	{
		line.SetVertexCount(listPositions.Count);
		
		for(int i = 0; i < listPositions.Count; ++i)
		{
			line.SetPosition(i, listPositions[i]);
		}
	}
}
