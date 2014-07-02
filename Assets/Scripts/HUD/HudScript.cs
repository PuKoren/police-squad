using UnityEngine;
using System.Collections;

public class HudScript : MonoBehaviour {

	public GUISkin skin;
	public float hudScale = 1f;
	public float squadBoxWidth = 150f;
	public float squadBoxHeight = 300f;
	public Texture2D copImg;
	public Texture2D healthBarDarkTex;
	public Texture2D healthBarTex;
	public GameObject[] squad;
	public Color guiColor;

	private int numberOfCops;
	private string[] copNames = {"Fabien", "Jonathan", "Guillaume", "Christian", "M. Bismi"};

    private float screenWidth;
    private float screenHeight;

	// Use this for initialization
	void Start () {
		this.numberOfCops = this.squad.Length;
        this.screenWidth = Screen.width;
        this.screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {		

	}

	void OnGUI() {
        GUISkin tempSkin = GUI.skin;
		GUI.skin = this.skin;
        Color temp = GUI.color;

        //GUI.BeginGroup (new Rect (10, screenHeigth - 60, 200, 50));
        //GUI.Box (new Rect (0, 0, 200, 50), "HUD scale");
        //hudScale = GUI.HorizontalSlider(new Rect(10, 25, 140, 30), hudScale, 0.7f, 1.0f );
        //GUI.Label(new Rect(155, 20, 50, 30), "(" + hudScale.ToString("f2") + ")");
        //GUI.EndGroup ();

        //TOP MIDDLE BOX
        GUI.BeginGroup(new Rect(this.screenWidth / 2 - 100, 10, 200 * hudScale, 200 * hudScale));
            GUI.skin = tempSkin;
            if (GUI.Button(new Rect(0, 50, 200 * hudScale, (100 * hudScale) / 2), "Action"))
            {
                this.gameObject.GetComponent<GameManagerScript>().LaunchTurn();
            }

            GUI.skin = this.skin;
            GUI.Box(new Rect(0, 0, 200 * hudScale, 40 * hudScale), "");

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            GUI.color = this.guiColor;
            string text = "Round : " + this.gameObject.GetComponent<GameManagerScript>().GetRound();
            GUI.Label(new Rect(0, 0, 200 * hudScale, (40 * hudScale) / 2), text);
            text = ((this.gameObject.GetComponent<GameManagerScript>().GetTurn() == 1) ? "Tactical Phase" : "Action");
            GUI.Label(new Rect(0, ((40 - 10) * hudScale) / 2, 200 * hudScale, (40 * hudScale) / 2), text);
            GUI.color = temp;           
        GUI.EndGroup();

		GUI.BeginGroup (new Rect (10, 50, this.squadBoxWidth * hudScale, this.squadBoxHeight * hudScale + numberOfCops * 2));

		for (int i = 0; i<this.numberOfCops; i++) {
			float height = this.squadBoxHeight / numberOfCops * hudScale;

			GUI.BeginGroup (new Rect (0, i * (height + 2), this.squadBoxWidth * hudScale, height));

            GUI.color = new Color(0f, 0f, 0f, 0f);
			if(GUI.Button(new Rect (0, 0, this.squadBoxWidth * hudScale, height), "")) {
				//select cop
				this.squad[i].GetComponent<PolicemanScript>().SelectUnit();
			}
			GUI.color = temp;

			GUI.Box (new Rect (0, 0, this.squadBoxWidth * hudScale, height), "");
			GUI.DrawTexture(new Rect(0, 5, height - 10, height - 10), copImg);			
			GUI.color = this.guiColor;
            centeredStyle.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(height - 10, 5, this.squadBoxWidth * hudScale, 30), copNames[i]);			
			GUI.color = temp;
			GUI.DrawTexture(new Rect(height - 10, height - 15, this.squadBoxWidth * hudScale - height + 5, 10 * hudScale), this.healthBarDarkTex);
			GUI.DrawTexture(new Rect(height - 10, height - 15, this.squad[i].GetComponent<PolicemanScript>().Pv * ((this.squadBoxWidth * hudScale - height + 5) / 10), 10 * hudScale), this.healthBarTex);
			GUI.EndGroup ();
		}

		GUI.EndGroup ();


        if (this.gameObject.GetComponent<GameManagerScript>().gameState == GameManagerScript.GameState.LOST || this.gameObject.GetComponent<GameManagerScript>().gameState == GameManagerScript.GameState.WIN)
        {
            bool test = this.gameObject.GetComponent<GameManagerScript>().gameState == GameManagerScript.GameState.LOST;

            string text1 = (test) ? "MISSION FAILED" : "MISSION SUCCEEDED";
            string text2 = (test) ? "Your cops have eaten too much donuts!" : "Your cops are the best!";
            GUI.color = (test) ? Color.red : Color.green;

            GUI.skin = tempSkin;
            GUI.BeginGroup(new Rect(this.screenWidth / 2 - 100, this.screenHeight / 2 - 100, 200 * hudScale, 160 * hudScale));
                GUI.Box(new Rect(0, 0, 200 * hudScale, 200 * hudScale), "");

                centeredStyle = GUI.skin.GetStyle("Label");
                centeredStyle.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(0, 10, 200 * hudScale, (40 * hudScale) / 2), text1);             
                GUI.Label(new Rect(0, 30, 200 * hudScale, (80 * hudScale) / 2), text2);
                GUI.color = temp;
                if (GUI.Button(new Rect(100 - 120 * hudScale / 2, 70, 120 * hudScale, (70 * hudScale) / 2), "RESTART"))
                {
                    Application.LoadLevel(Application.loadedLevelName);
                }

                if (GUI.Button(new Rect(100 - 120 * hudScale / 2, 110, 120 * hudScale, (70 * hudScale) / 2), "MAIN MENU"))
                {
                    Application.LoadLevel("Crime_Scene_Street_Jonathan");
                }
            GUI.EndGroup();
        }
	}	
}