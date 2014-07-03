using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolicemanScript : MonoBehaviour {

	public enum colors{Blue, Green, Red, Orange, Yellow};
    //public enum CopStatus { STAND, MOVING, ATTACKING };
	public colors visionColor;
    public int Pv = 10;
    public GameObject BulletPrefab;
    static public int RangeRandomPercentLife = 10;

    public bool IsAlive = true;

    private List<GameObject> enemyInVision;
    private GameObject target = null;
    private float attackSpeed = 1f;
    private float previousTime = 0f;

	// Use this for initialization
	void Start ()
    {
        this.enemyInVision = new List<GameObject>();
		this.transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load("Materials/ColorWMediumTransparency/" + visionColor + "_MT", typeof(Material)) as Material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManagerScript.gameState == GameManagerScript.GameState.START)
        {
            if (this.target == null)
            {
                if (this.enemyInVision.Count > 0)
                {
                    this.target = this.enemyInVision[0];
                }
            }

            if (this.target)
            {
                if (this.target.GetComponent<EnemyControllerScript>().IsAlive)
                {
                    if (Time.time >= this.previousTime + this.attackSpeed)
                    {
                        //Instanciate the bullet
                        EnemyControllerScript enemy = this.target.GetComponent<EnemyControllerScript>();

                        GameObject go = Instantiate(this.BulletPrefab, this.transform.position, Quaternion.identity) as GameObject;
                        go.transform.LookAt(enemy.transform);
                        //if (!enemy.isSeeingPoliceman(this))
                        //{
                        //    go.GetComponent<BulletScript>().Damage = enemy.Pv;
                        //}

                        this.previousTime = Time.time;
                    }
                }
                else
                {
                    this.enemyInVision.Remove(this.target);
                    this.target = null;
                }
            }
        }        
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
        if (this.IsAlive)
        {
            this.Pv -= damage;
            if (this.Pv <= 0)
            {
                this.IsAlive = false;
                GameManagerScript.NbCopsAlive--;
                //Destroy(this.gameObject);
            }
        }
    }

    // Trigger
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy") {

            if (!this.enemyInVision.Contains(collider.gameObject) && collider.gameObject.GetComponent<EnemyControllerScript>().IsAlive)
            {
                this.enemyInVision.Add(collider.gameObject);
            }
            //Debug.Log(this.enemyInVision.Count);
            //EnemyControllerScript enemy = collider.gameObject.GetComponent<EnemyControllerScript>();

            //// Instanciate the bullet
            //GameObject go = Instantiate(this.BulletPrefab, this.transform.position, Quaternion.identity) as GameObject;
            //go.transform.LookAt(enemy.transform);
            //if (!enemy.isSeeingPoliceman(this))
            //{
            //    go.GetComponent<BulletScript>().Damage = enemy.Pv;
            //}
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            if (this.enemyInVision.Contains(collider.gameObject))
            {
                if (this.target == collider.gameObject)
                {
                    this.target = null;
                }
                this.enemyInVision.Remove(collider.gameObject);
            }
        }
    }
}
