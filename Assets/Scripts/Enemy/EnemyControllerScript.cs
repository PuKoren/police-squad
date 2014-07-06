using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControllerScript : MonoBehaviour
{
    public float DelayBetweenTwoFires = 1.0f;
    public int Pv = 4;
    public GameObject BulletPrefab;
    public GameManagerScript GameManager;
    public SquadManagerScript squad;
    public float SizeRadiusWalk = 5.0f;
    public float attackSpeed = 1.0f;

    private List<PolicemanScript> PolicemanVisible = new List<PolicemanScript>();
    private Transform PositionExit;
    public List<Vector3> PositionToMove = new List<Vector3>();
    private float previousTime = 0.0f;
    private PolicemanScript target = null;
    private bool executeActions = false;

	// Use this for initialization
	void Start ()
    {
        this.PositionExit = GameObject.FindGameObjectWithTag("Exit").transform;

        Vector3 maxSize = (this.PositionExit.position - this.transform.position) / (this.squad.nbActionsPerTurn * this.GameManager.numberRoundMaximum);
        for (int i = 0; i < this.squad.nbActionsPerTurn * this.GameManager.numberRoundMaximum - 1; ++i)
        {
            Vector3 pos = this.transform.position + maxSize * (i + 1);
            pos.x += Random.Range(-this.SizeRadiusWalk, this.SizeRadiusWalk);
            pos.z += Random.Range(-this.SizeRadiusWalk, this.SizeRadiusWalk);

            this.PositionToMove.Add(pos);
        }
        this.PositionToMove.Add(this.PositionExit.position);

        this.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManagerScript.gameState == GameManagerScript.GameState.START && this.Pv > 0 && this.executeActions)
        {
            if (this.target == null)
            {
                if (this.PolicemanVisible.Count > 0)
                {
                    this.target = this.PolicemanVisible[0];
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
                        this.previousTime = Time.time;
                    }
                }
                else
                {
                    this.PolicemanVisible.Remove(this.target);
                    this.target = null;
                }
            }
        }
	}

	// Allow the unit to do its actions (moving)
    public void setExecuteActions(bool execute)
    {
        if (this.Pv > 0)
        {
            this.executeActions = execute;
            this.transform.GetChild(0).GetComponent<MeshCollider>().enabled = execute;

            NavMeshEnemyScript nav = this.GetComponent<NavMeshEnemyScript>();
            if (execute)
            {
                for (int i = this.squad.nbActionsPerTurn * (this.GameManager.GetRound() - 1); i < this.squad.nbActionsPerTurn * this.GameManager.GetRound(); ++i)
                {
                    nav.addTarget(this.PositionToMove[i]);
                }
            }
            nav.setExecuteActions(execute);
        }
    }

    // Getter
    public List<PolicemanScript> GetPolicemanVisible()
    {
        return PolicemanVisible;
    }

    // Differents functions
    public void Touch(int damage)
    {
        if (this.Pv > 0)
        {
            this.Pv -= damage;
            if (this.Pv <= 0)
            {
                GameManagerScript.NbEnemyAlive--;
                this.GetComponent<NavMeshAgent>().Stop();
                this.GetComponent<NavMeshEnemyScript>().setExecuteActions(false);
            }
        }
    }

    public bool isSeeingPoliceman(PolicemanScript policeman)
    {
        foreach (PolicemanScript police in this.PolicemanVisible)
        {
            if (police == policeman)
                return true;
        }
        return false;
    }

    // Trigger
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Cop" && !this.PolicemanVisible.Contains(collider.GetComponent<PolicemanScript>()))
            this.PolicemanVisible.Add(collider.GetComponent<PolicemanScript>());
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Cop")
        {
            if (this.target == collider.gameObject.GetComponent<PolicemanScript>())
            {
                this.target = null;
            }
            this.PolicemanVisible.Remove(collider.GetComponent<PolicemanScript>());
        }
    }

    // Collision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Exit")
            GameManagerScript.gameState = GameManagerScript.GameState.LOSTROUND;
    }
}
