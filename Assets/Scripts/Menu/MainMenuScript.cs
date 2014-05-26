using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public GUISkin skin;
	private int menuWidth = 200;
	private int menuHeigth = 150;
	private int buttonWidth = 120;
	private int buttonHeight = 20;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {	
		GUI.skin = skin;	
		GUI.BeginGroup (new Rect (Screen.width / 2 - this.menuWidth / 2, Screen.height / 2 - this.menuHeigth / 2, this.menuWidth, this.menuHeigth));
		//GUI.Box (new Rect (0, 0, this.menuWidth, this.menuHeigth), "");
		if (GUI.Button (new Rect (this.menuWidth / 2 - this.buttonWidth / 2, 20, this.buttonWidth, this.buttonHeight), "START GAME")) {
			Application.LoadLevel("Mission-01");
		}

		if (GUI.Button (new Rect (this.menuWidth / 2 - this.buttonWidth / 2, 50, this.buttonWidth, this.buttonHeight), "CREDITS")) {
			Application.LoadLevel("Credits");
		}

		if(GUI.Button (new Rect(this.menuWidth / 2 - this.buttonWidth / 2, 80, this.buttonWidth, this.buttonHeight), "QUIT GAME")) {
			Application.Quit();
		}

		GUI.EndGroup ();
	}
}
