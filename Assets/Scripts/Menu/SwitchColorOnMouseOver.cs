using UnityEngine;
using System.Collections;

public class SwitchColorOnMouseOver : MonoBehaviour {
	public Color switchColor;
	private Color baseColor;
	// Use this for initialization
	void Start () {
		TextMesh component = this.GetComponent<TextMesh>();
		if(component)
			this.baseColor = component.color;
		else{
			this.baseColor = this.renderer.material.color;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	/*
	 * NEEDS A COLLIDER !!!
	 */
	void OnMouseEnter() {

		TextMesh component = this.GetComponent<TextMesh>();
		if(component)
			component.color = this.switchColor;
		else{
			renderer.material.color = this.switchColor;
		}
	}

	void OnMouseExit(){

		TextMesh component = this.GetComponent<TextMesh>();
		if(component)
			component.color = this.baseColor;
		else{
			renderer.material.color = this.baseColor;
		}
	}
}
