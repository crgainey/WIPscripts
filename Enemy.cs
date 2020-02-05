using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public Transform[] waypoints;
    public Transform player;

    private int _nextpoint;

    NavMeshAgent _agent;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        GoToNextPoint();
    }


    void GoToNextPoint()
    {
        //Returns if no points are setup
        if(waypoints.Length == 0)
            return;

        //sets path for agent
        _agent.destination = waypoints[_nextpoint].position;

        //cycles thru the wp
        _nextpoint = (_nextpoint + 1) % waypoints.Length;

    }

    void FollowPlayer()
    {
        _agent.SetDestination(player.transform.position);
    }

    private void Update()
    {
        //chooses the next point when agent is close to current wp
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            GoToNextPoint();

        //if player is a certain distance away follow
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance < 5.0)
        {
            FollowPlayer();
        }

    }

}
