using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PatrolPath _patrolPath;
    private Transform _currentTarget;
    private int _currentPatrolPoint;
    [SerializeField] private float _speed = 4f;
    private NavMeshAgent _agent;

    

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
    }

    void Start()
    {
        // Start by going to a patrol point
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _agent.destination) <= 0.5f)
        {
            // Get the next patrol point once at the current
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPath.GetPatrolCount();
            GoToNextPoint();
        }
    }

    private void OnCollisionEnter(Collision other)
    {

    }


    private void GoToNextPoint()
    {
        // Get next point of patrol
        _currentTarget = _patrolPath.GetPointPos(_currentPatrolPoint);
        _agent.destination = _currentTarget.position;
    }
}
