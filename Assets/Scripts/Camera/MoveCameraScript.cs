using UnityEngine;
using System.Collections;

public class MoveCameraScript : MonoBehaviour {

    public float speed = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

	// Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
        
		if(Input.GetKeyUp(KeyCode.Escape))
			Application.Quit();
    }
}
