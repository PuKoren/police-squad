using UnityEngine;
using System.Collections;

public class LoadSceneOnMouseDown : MonoBehaviour {

	public string levelToLoad;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	 * NEEDS COLLIDER !
	 */
	void OnMouseDown(){
		Application.LoadLevel(levelToLoad);
	}
}
