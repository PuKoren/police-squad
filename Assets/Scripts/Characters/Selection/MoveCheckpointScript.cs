/*
SCRIPT TO DRAG AND DROP A CHECKPOINT
*/

using UnityEngine;
using System.Collections;

public class MoveCheckpointScript : MonoBehaviour {

	private int leftButton = 0;
	private float rodentRange = 250.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDrag()
	{
		RaycastHit hit;
		
		if(Input.GetMouseButton(leftButton))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			Physics.Raycast(ray, out hit, rodentRange);
			
		}
			this.transform.position = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);

	}
}
