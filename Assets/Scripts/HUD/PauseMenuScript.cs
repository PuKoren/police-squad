using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{
	public GUISkin skin;
	
	private float gldepth = -0.5f;
	private float startTime = 0.1f;
	
	public Material mat;

	private float savedTimeScale;
	
	private bool showfps;
	
	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;
	
	public int lowFPS = 30;
	public int highFPS = 50;
	
	public GameObject start;
	
	public string url = "unity.html";
	
	public Color statColor = Color.green;
	
	public string[] credits= {
		"A Drilling Studio game",
		"Programming by ",
		"Jonathan MULLER, Christian NGO, Guillaume NOISETTE & Fabien SACRISTE ",
		"Copyright (c) 2014 Drilling Studio"};
	public Texture[] crediticons;
	
	public enum Page {
		None,Main,Options,Credits,MainMenu
	}
	
	private Page currentPage;
	
	private float[] fpsarray;
	private float fps;
	
	private int toolbarInt = 0;
	private string[]  toolbarstrings =  {"Audio", "Graphics", "Stats"};
	
	
	void Start() {
		fpsarray = new float[Screen.width];
		Time.timeScale = 1;
		//PauseGame();
	}
	
	void LateUpdate () {
		if (showfps) {
			FPSUpdate();
		}
		
		if (Input.GetKeyDown("escape")) 
		{
			switch (currentPage) 
			{
			case Page.None: 
				PauseGame(); 
				break;
				
			case Page.Main: 
				if (!IsBeginning()) 
					UnPauseGame(); 
				break;
				
			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}
	
	void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}
		ShowStatNums();
		if (IsGamePaused()) {
			GUI.color = statColor;
			switch (currentPage) {
			case Page.Main: MainPauseMenu(); break;
			case Page.Options: ShowToolbar(); break;
			case Page.Credits: ShowCredits(); break;
            case Page.MainMenu: MainMenu(); break;
			}
		}   
	}
	
	void ShowToolbar() {
		BeginPage(300,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) {
		case 0: VolumeControl(); break;
		//case 3: ShowDevice(); break;
		case 1: Qualities(); QualityControl(); break;
		case 2: StatControl(); break;
		}
		EndPage();
	}
	
	void ShowCredits() {
		BeginPage(300,300);
		foreach(string credit in credits) {
			GUILayout.Label(credit);
		}
		foreach( Texture credit in crediticons) {
			GUILayout.Label(credit);
		}
		EndPage();
	}
	
	void ShowBackButton() {
		if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20),"Back")) {
			currentPage = Page.Main;
		}
	}

    void MainMenu()
    {
        Application.LoadLevel("Crime_Scene_Street_Jonathan");
    }
	
//	void ShowDevice() {
//		GUILayout.Label("Unity player version "+Application.unityVersion);
//		GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
//		                SystemInfo.graphicsMemorySize+"MB\n"+
//		                SystemInfo.graphicsDeviceVersion+"\n"+
//		                SystemInfo.graphicsDeviceVendor);
//		GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
//		GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
//		GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
//	}
	
	void Qualities() {
		switch (QualitySettings.currentLevel) 
		{
		case QualityLevel.Fastest:
			GUILayout.Label("Fastest");
			break;
		case QualityLevel.Fast:
			GUILayout.Label("Fast");
			break;
		case QualityLevel.Simple:
			GUILayout.Label("Simple");
			break;
		case QualityLevel.Good:
			GUILayout.Label("Good");
			break;
		case QualityLevel.Beautiful:
			GUILayout.Label("Beautiful");
			break;
		case QualityLevel.Fantastic:
			GUILayout.Label("Fantastic");
			break;
		}
	}
	
	void QualityControl() {
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Decrease")) {
			QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button("Increase")) {
			QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();

        GUILayout.Label("Hud scale");
        this.GetComponent<HudScript>().hudScale = GUILayout.HorizontalSlider(this.GetComponent<HudScript>().hudScale, 0.7f, 1f);
	}
	
	void VolumeControl() {
		GUILayout.Label("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
	}
	
	void StatControl() {
		GUILayout.BeginHorizontal();
		showfps = GUILayout.Toggle(showfps,"FPS");
		GUILayout.EndHorizontal();
	}
	
	void FPSUpdate() {
		float delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) {
			fps = 1 / delta;
		}
	}
	
	void ShowStatNums() {
		GUILayout.BeginArea( new Rect(Screen.width - 100, 10, 100, 200));
		if (showfps) {
			string fpsstring= fps.ToString ("#,##0 fps");
			GUI.color = Color.Lerp(lowFPSColor, highFPSColor,(fps-lowFPS)/(highFPS-lowFPS));
			GUILayout.Label (fpsstring);
		}
		GUILayout.EndArea();
	}
	
	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Main) {
			ShowBackButton();
		}
	}
	
	bool IsBeginning() {
		return (Time.time < startTime);
	}
	
	
	void MainPauseMenu() {
		BeginPage(200,200);
		if (GUILayout.Button (IsBeginning() ? "Play" : "Continue")) {
			UnPauseGame();
			
		}
		if (GUILayout.Button ("Options")) {
			currentPage = Page.Options;
		}
        //if (GUILayout.Button ("Credits")) {
        //    currentPage = Page.Credits;
        //}
        if (GUILayout.Button("Quit to Main Menu"))
        {
            currentPage = Page.MainMenu;
        }
		EndPage();
	}
	
	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		currentPage = Page.Main;
	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		
		currentPage = Page.None;
		
		if (IsBeginning() && start != null) {
			start.active = true;
		}
	}
	
	bool IsGamePaused() {
		return (Time.timeScale == 0);
	}
	
	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}
}