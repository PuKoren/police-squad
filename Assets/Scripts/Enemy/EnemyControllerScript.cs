using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControllerScript : MonoBehaviour
{
    public float DelayBetweenTwoFires = 1.0f;
    public int Pv = 10;
    public GameObject BulletPrefab;
    public float MaximalDistanceForSeeFriends = 10.0f;

    private List<PolicemanScript> PolicemanVisible = new List<PolicemanScript>();
    private List<EnemyControllerScript> FriendsArround = new List<EnemyControllerScript>();
    private bool IsFighting = false;
    private bool AlreadySeePoliceman = false;
    private EnemyBehaviorTree Behavior;
    private Transform PositionExit;
    private NavMeshAgent NavMeshAgent;
    private bool executeActions;

	// Use this for initialization
	void Start ()
    {
        this.Behavior = new EnemyBehavior();
        this.Behavior.UserData = this;
        this.PositionExit = GameObject.FindGameObjectWithTag("Exit").transform;
        this.NavMeshAgent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (executeActions)
        {
            this.Behavior.Update();

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
        }
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

            // Stop moving
            this.NavMeshAgent.Stop();
        }
    }

    public void Escape()
    {
        // Move the object
        this.NavMeshAgent.SetDestination(this.PositionExit.position);
    }

    public void GoToHelpFriend(EnemyControllerScript friend)
    {
        // Move the object
        this.NavMeshAgent.SetDestination(friend.transform.position);
    }

    public void GoToExit()
    {
        // Move the object
        this.NavMeshAgent.SetDestination(this.PositionExit.position);
    }

	// Allow the unit to do its actions (moving)
    public void setExecuteActions(bool execute)
    {
        executeActions = execute;
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
