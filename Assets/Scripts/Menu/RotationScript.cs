using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {

	public float rotationSpeed;
	private Vector3 rot;

	// Use this for initialization
	void Start () {
		rot = new Vector3 (0f, rotationSpeed, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (rot);
	}
}
