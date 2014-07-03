using UnityEngine;
using System.Collections;

public class MoveCameraScript : MonoBehaviour {

    public float speed = 30.0F;
    public Vector2 sizeForMovementInPercent = new Vector2(4.0f, 8.0f);
    public Vector2 height = new Vector2(70.0f, 100.0f);
    public float speedZoom = 1.0f;

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

        goodDirection *= speed;
        this.controller.Move(goodDirection * Time.deltaTime);
        
		if(Input.GetKeyUp(KeyCode.Escape))
			Application.Quit();

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + ((-1 * Input.GetAxis("Mouse ScrollWheel")) * speedZoom), this.transform.position.z);

            if (this.transform.position.y < this.height.x)
                this.transform.position = new Vector3(this.transform.position.x, this.height.x, this.transform.position.z);

            if (this.transform.position.y > this.height.y)
                this.transform.position = new Vector3(this.transform.position.x, this.height.y, this.transform.position.z);
        }
    }
}
