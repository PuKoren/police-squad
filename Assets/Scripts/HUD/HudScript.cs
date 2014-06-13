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

	// Use this for initialization
	void Start () {
		this.numberOfCops = this.squad.Length;
	}
	
	// Update is called once per frame
	void Update () {		

	}

	void OnGUI() {
		GUI.skin = this.skin;

        //float screenWidth = Screen.width;
		//float screenHeigth = Screen.height;

        //GUI.BeginGroup (new Rect (10, screenHeigth - 60, 200, 50));
        //GUI.Box (new Rect (0, 0, 200, 50), "HUD scale");
        //hudScale = GUI.HorizontalSlider(new Rect(10, 25, 140, 30), hudScale, 0.7f, 1.0f );
        //GUI.Label(new Rect(155, 20, 50, 30), "(" + hudScale.ToString("f2") + ")");
        //GUI.EndGroup ();

		GUI.BeginGroup (new Rect (10, 10, this.squadBoxWidth * hudScale, this.squadBoxHeight * hudScale + numberOfCops * 2));

		for (int i = 0; i<this.numberOfCops; i++) {
			float height = this.squadBoxHeight / numberOfCops * hudScale;

			GUI.BeginGroup (new Rect (0, i * (height + 2), this.squadBoxWidth * hudScale, height));

			Color temp = GUI.color;
            GUI.color = new Color(0f, 0f, 0f, 0f);
			if(GUI.Button(new Rect (0, 0, this.squadBoxWidth * hudScale, height), "")) {
				//select cop
				this.squad[i].GetComponent<PolicemanScript>().SelectUnit();
			}
			GUI.color = temp;

			GUI.Box (new Rect (0, 0, this.squadBoxWidth * hudScale, height), "");
			GUI.DrawTexture(new Rect(0, 5, height - 10, height - 10), copImg);			
			GUI.color = this.guiColor;
			GUI.Label(new Rect(height - 10, 5, this.squadBoxWidth * hudScale, 30), copNames[i]);			
			GUI.color = temp;
			GUI.DrawTexture(new Rect(height - 10, height - 15, this.squadBoxWidth * hudScale - height + 5, 10 * hudScale), this.healthBarDarkTex);
			GUI.DrawTexture(new Rect(height - 10, height - 15, (this.squadBoxWidth * hudScale - height + 5) * 0.1f * (i + 1), 10 * hudScale), this.healthBarTex);
			GUI.EndGroup ();
		}

		GUI.EndGroup ();
	}	
}