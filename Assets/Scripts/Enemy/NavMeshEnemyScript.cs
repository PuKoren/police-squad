using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshEnemyScript : MonoBehaviour {

    private Vector3 currentTarget;
    private int indexTarget = -1;
    private NavMeshAgent agent;

    private List<Vector3> listTarget = new List<Vector3>();
    private bool executeActions;
    private bool hasReachedTheLastPoint;

    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        executeActions = false;
        hasReachedTheLastPoint = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (executeActions)
        {
            // if there are targets in the list but none is selected yet
            if (listTarget.Count > 0 && indexTarget == -1)
                setTarget();

            // if a target has been defined
            if (indexTarget != -1)
            {
                // move the object
                agent.SetDestination(currentTarget);

                // get the position of the object and the target
                Vector3 objectPos = this.transform.position;
                Vector3 targetPos = currentTarget;

                objectPos.y = 0.0f;
                targetPos.y = 0.0f;

                // if the object has reached the target, the directionPoint is destroyed, the target is removed  from the list
                if (Vector3.Distance(objectPos, targetPos) <= 1)
                {
                    listTarget.RemoveAt(0);
                    indexTarget = -1;
                }

                // Define if the unit has reached its last checkpoint
                if (listTarget.Count == 0)
                    hasReachedTheLastPoint = true;
                else
                    hasReachedTheLastPoint = false;
            }
        }
    }

    // Add a checkpoint where the unit will have to go
    public void addTarget(Vector3 pos)
    {
        listTarget.Add(pos);
    }

    // Define the next checkpoint the unit has to go
    private void setTarget()
    {
        currentTarget = listTarget[0];
        indexTarget = 0;
    }

    // Allow the unit to do its actions (moving)
    public void setExecuteActions(bool execute)
    {
        executeActions = execute;

        // Define if the unit has reached its last checkpoint
        if (listTarget.Count == 0)
            hasReachedTheLastPoint = true;
        else
            hasReachedTheLastPoint = false;
    }

    // Return if the unit has finish its path, therefore reached its last checkpoint
    public bool hasFinishedItsPath()
    {
        return hasReachedTheLastPoint;
    }
}
