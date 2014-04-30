using UnityEngine;
using System.Collections;

public class LoopPositionScript : MonoBehaviour {

	public enum wayToMove{X, Z};
	public wayToMove direction;
	
	public float max;
	public float increment;
	private float startX;
	private float startZ;
	private float endX;
	private float endZ;
	
	private bool isMovingForward;
	private bool activated;
	private float coef;
	
	// Use this for initialization
	void Start () {
	
		startX = this.transform.localPosition.x;
		endX = startX + max;
		
		startZ = this.transform.localPosition.z;
		endZ = startZ + max;
		
		coef = 1;
		
		if(direction == wayToMove.X && startX <= 0.0f)
			coef = -1;
		else if(direction == wayToMove.Z && startZ <= 0.0f)
			coef = -1;
		
		activated = true;
		isMovingForward = true;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(activated)
		{
			float step = increment * Time.deltaTime * coef;

			if(isMovingForward)
			{
				if(direction == wayToMove.X)
				{
					if(this.transform.localPosition.x * coef < endX * coef)
						this.transform.localPosition = new Vector3(this.transform.localPosition.x + step, this.transform.localPosition.y, this.transform.localPosition.z);
					else
						isMovingForward = false;
				}
				else
				{
					if(this.transform.localPosition.z * coef < endZ * coef)
						this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + step);
					else
						isMovingForward = false;
				}
			}
			else
			{
				if(direction == wayToMove.X)
				{
					if(this.transform.localPosition.x * coef > startX * coef)
						this.transform.localPosition = new Vector3(this.transform.localPosition.x - step, this.transform.localPosition.y, this.transform.localPosition.z);
					else
						isMovingForward = true;
				}
				else
				{
					if(this.transform.localPosition.z * coef > startZ * coef)
						this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - step);
					else
						isMovingForward = true;
				}
			}
		}
	}
}
