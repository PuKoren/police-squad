using UnityEngine;
using System.Collections;

public class PolicemanScript : MonoBehaviour {

	public enum colors{Blue, Green, Red, Orange, Yellow};
	public colors visionColor;
    public int Pv = 10;
    public GameObject BulletPrefab;
    static public int RangeRandomPercentLife = 10;

    private bool IsAlive = true;
	
	// Use this for initialization
	void Start ()
    {
		this.transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load("Materials/ColorWMediumTransparency/" + visionColor + "_MT", typeof(Material)) as Material;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
	
	void OnMouseDown()
	{
		this.SelectUnit();
	}
	
	public void activate()
	{
		this.transform.GetChild(2).GetComponent<LoopScaleScript>().activate();
        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
	}
	
	public void deactivate()
	{
		// deactivate the torus scale changing
		this.transform.GetChild(2).GetComponent<LoopScaleScript>().deactivate();
        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
	}

	public void SelectUnit()
    {
		// Tell the squad object (parent) to select this object
        if(this.IsAlive)
		    this.transform.parent.GetComponent<SquadManagerScript>().switchCurrentCop(this);
	}

    public float GetRandomPV()
    {
        // Use by the enemies to know how many pv have the policeman
        float newPV = this.Pv + (float)(this.Pv * Random.Range(-PolicemanScript.RangeRandomPercentLife, PolicemanScript.RangeRandomPercentLife)) / 100;
        if (newPV < 1)
            newPV = 1;
        return newPV;
    }

    public void Touch(int damage)
    {
        this.Pv -= damage;
        if (this.Pv <= 0)
        {
            this.IsAlive = false;
            GameManagerScript.NbCopsAlive--;
            //Destroy(this.gameObject);
        }
    }

    // Trigger
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            EnemyControllerScript enemy = collider.gameObject.GetComponent<EnemyControllerScript>();

            // Instanciate the bullet
            GameObject go = Instantiate(this.BulletPrefab, this.transform.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(enemy.transform);
            if (!enemy.isSeeingPoliceman(this))
            {
                go.GetComponent<BulletScript>().Damage = enemy.Pv;
            }
        }
    }
}
