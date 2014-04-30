using UnityEngine;
using System.Collections;

public class NavMeshScript : MonoBehaviour {

	public Transform target;
	public NavMeshAgent agent;
	
	public bool isMoving;
	
	// Use this for initialization
	void Start () {
	
		isMoving = true;
		
		agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isMoving && target != null)
			agent.SetDestination(target.position);
	}
	
	public void setTarget(GameObject pos)
	{
		target = pos.transform;
	}
}
