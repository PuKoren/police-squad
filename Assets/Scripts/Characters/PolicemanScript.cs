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
    public float attackSpeed = 1.0f;

    private List<EnemyControllerScript> enemyInVision = new List<EnemyControllerScript>();
    private EnemyControllerScript target = null;
    private float previousTime = 0.0f;
    private bool executeActions = false;

	// Use this for initialization
	void Start ()
    {
		this.transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load("Materials/ColorWMediumTransparency/" + visionColor + "_MT", typeof(Material)) as Material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManagerScript.gameState == GameManagerScript.GameState.START && this.Pv > 0 && this.executeActions)
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
                if (this.target.Pv > 0)
                {
                    if (Time.time >= this.previousTime + this.attackSpeed)
                    {
                        //Instanciate the bullet
                        GameObject go = Instantiate(this.BulletPrefab, this.transform.position, Quaternion.identity) as GameObject;
                        go.transform.LookAt(this.target.transform);
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

    public void setExecuteActions(bool execute)
    {
        this.executeActions = execute;
    }
	
	void OnMouseDown()
	{
		this.SelectUnit();
	}
	
	public void activate()
	{
		this.transform.GetChild(2).GetComponent<LoopScaleScript>().activate();
	}
	
	public void deactivate()
	{
		// deactivate the torus scale changing
		this.transform.GetChild(2).GetComponent<LoopScaleScript>().deactivate();
	}

	public void SelectUnit()
    {
		// Tell the squad object (parent) to select this object
        if(this.Pv > 0)
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
        if (this.Pv > 0)
        {
            this.Pv -= damage;
            if (this.Pv <= 0)
            {
                GameManagerScript.NbCopsAlive--;
                this.GetComponent<NavMeshAgent>().Stop();
                this.GetComponent<NavMeshScript>().setExecuteActions(false);
                this.particleSystem.Stop();
                this.particleSystem.enableEmission = true;
                this.particleSystem.Play();
            }
        }
    }

    // Trigger
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            if (!this.enemyInVision.Contains(collider.gameObject.GetComponent<EnemyControllerScript>()) && collider.gameObject.GetComponent<EnemyControllerScript>().Pv > 0)
            {
                this.enemyInVision.Add(collider.gameObject.GetComponent<EnemyControllerScript>());
                collider.gameObject.renderer.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            if (this.target == collider.gameObject.GetComponent<EnemyControllerScript>())
            {
                this.target = null;
            }
            this.enemyInVision.Remove(collider.gameObject.GetComponent<EnemyControllerScript>());

            GameObject[] gos = GameObject.FindGameObjectsWithTag("Cop");
            bool find = false;
            foreach (GameObject go in gos)
            {
                PolicemanScript ps = go.GetComponent<PolicemanScript>();
                if (ps.enemyInVision.Contains(collider.gameObject.GetComponent<EnemyControllerScript>()))
                {
                    find = true;
                    break;
                }
            }
            if (!find && collider.gameObject.GetComponent<EnemyControllerScript>().Pv > 0)
                collider.gameObject.renderer.enabled = false;
        }
    }
}
