using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControllerScript : MonoBehaviour
{
    public float DelayBetweenTwoFires = 1.0f;
    public int Pv = 10;
    public GameObject BulletPrefab;
    public GameManagerScript GameManager;
    public SquadManagerScript squad;
    public float MaximalDistanceForSeeFriends = 10.0f;
    public float SizeRadiusWalk = 5.0f;

    private List<PolicemanScript> PolicemanVisible = new List<PolicemanScript>();
    private List<EnemyControllerScript> FriendsArround = new List<EnemyControllerScript>();
    private bool IsFighting = false;
    private bool AlreadySeePoliceman = false;
    private Transform PositionExit;
    private NavMeshAgent NavMeshAgent;
    private bool executeActions;
    public List<Vector3> PositionToMove = new List<Vector3>();

	// Use this for initialization
	void Start ()
    {
        this.PositionExit = GameObject.FindGameObjectWithTag("Exit").transform;
        this.NavMeshAgent = this.GetComponent<NavMeshAgent>();

        Vector3 maxSize = (this.PositionExit.position - this.transform.position) / (this.squad.nbActionsPerTurn * this.GameManager.numberRoundMaximum);
        for (int i = 0; i < this.squad.nbActionsPerTurn * this.GameManager.numberRoundMaximum; ++i)
        {
            Vector3 pos = this.transform.position + maxSize * (i + 1);
            pos.x += Random.Range(-this.SizeRadiusWalk, this.SizeRadiusWalk);
            pos.z += Random.Range(-this.SizeRadiusWalk, this.SizeRadiusWalk);

            this.PositionToMove.Add(pos);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (executeActions)
        {
            // Update the list
            this.FriendsArround.Clear();
            EnemyControllerScript[] enemies = GameObject.FindObjectsOfType<EnemyControllerScript>();
            foreach (EnemyControllerScript ennemy in enemies)
            {
                if (ennemy != this)
                {
                    if (Vector3.Distance(ennemy.transform.position, this.transform.position) <= this.MaximalDistanceForSeeFriends)
                    {
                        this.FriendsArround.Add(ennemy);
                    }
                }
            }
        }
        else
        {
            this.NavMeshAgent.Stop();
        }*/
	}

    public void Fire()
    {
        this.IsFighting = true;
        
        // Find the closest policeman
        float minDistance = Mathf.Infinity;
        PolicemanScript policeman = null;
        for (int i = 0; i < this.PolicemanVisible.Count; ++i)
        {
            float tempDistance = Vector3.Distance(this.PolicemanVisible[i].transform.position, this.transform.position);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                policeman = this.PolicemanVisible[i];
            }
        }

        if (policeman != null)
        {
            // Instanciate the bullet
            GameObject go = Instantiate(this.BulletPrefab, this.transform.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(policeman.transform);
        }
    }

	// Allow the unit to do its actions (moving)
    public void setExecuteActions(bool execute)
    {
        executeActions = execute;
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

    // Getter
    public List<PolicemanScript> GetPolicemanVisible()
    {
        return PolicemanVisible;
    }
    public List<EnemyControllerScript> GetFriendsArround()
    {
        return FriendsArround;
    }
    public bool GetIsFighting()
    {
        return this.IsFighting;
    }
    public bool GetAlreadySeePoliceman()
    {
        return this.AlreadySeePoliceman;
    }

    // Differents functions
    public void Touch(int damage)
    {
        this.Pv -= damage;
        if (this.Pv <= 0)
            Destroy(this.gameObject);
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
        if (collider.tag == "Cop")
            this.PolicemanVisible.Add(collider.GetComponent<PolicemanScript>());
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Cop")
            this.PolicemanVisible.Remove(collider.GetComponent<PolicemanScript>());
    }
}
