using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControllerScript : MonoBehaviour
{
    public List<PolicemanScript> PolicemanVisible = new List<PolicemanScript>();
    public List<EnemyControllerScript> FriendsArround = new List<EnemyControllerScript>();
    public float DelayBetweenTwoFires = 1.0f;
    public int pv = 10;
    public bool IsFighting = false;
    public bool AlreadySeePoliceman = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Fire()
    {
        this.IsFighting = true;
    }

    public void Escape()
    {
    }

    public void GoToHelpFriend(EnemyControllerScript friend)
    {
    }

    public void GoToExit()
    {
    }
}
