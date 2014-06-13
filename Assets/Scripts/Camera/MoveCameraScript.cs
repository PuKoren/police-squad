using UnityEngine;
using System.Collections;

public class MoveCameraScript : MonoBehaviour {

    public float speed = 30.0F;
    public Vector2 sizeForMovementInPercent = new Vector2(4, 8);

    private CharacterController controller;
    private Vector2 limitMinScreenMove;
    private Vector2 limitMaxScreenMove;

	// Use this for initialization
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.limitMinScreenMove = new Vector2((Screen.width * this.sizeForMovementInPercent.x) / 100, (Screen.height * this.sizeForMovementInPercent.y) / 100);
        this.limitMaxScreenMove = new Vector2(Screen.width - this.limitMinScreenMove.x, Screen.height - this.limitMinScreenMove.y);
	}
	
	// Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Input.mousePosition;
        Vector3 goodDirection = new Vector3();

        if (moveDirection.x < this.limitMinScreenMove.x)
            goodDirection.x = -1;
        if (moveDirection.x > this.limitMaxScreenMove.x)
            goodDirection.x = 1;
        if (moveDirection.y < this.limitMinScreenMove.y)
            goodDirection.z = -1;
        if (moveDirection.y > this.limitMaxScreenMove.y)
            goodDirection.z = 1;

        //moveDirection = transform.TransformDirection(moveDirection);
        goodDirection *= speed;
        this.controller.Move(goodDirection * Time.deltaTime);
        
		if(Input.GetKeyUp(KeyCode.Escape))
			Application.Quit();
    }
}
