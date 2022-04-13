using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PatrolPath _patrolPath;
    private static float _speed = 4f;
    private float _currentSpeed;
    private Transform _currentTarget;
    private int _currentPatrolPoint;
    private NavMeshAgent _agent;

    public static event Action EnemyTouched;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        // Set speed of agent
        _currentSpeed = _speed;
        _agent.speed = _currentSpeed;
    }

    void Start()
    {
        // Add as observer when coin is collected
        Coin.CoinCollected += IncreaseEnemyMovement;
        // Start by going to a patrol point
        GoToNextPoint();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _agent.destination) <= 0.5f)
        {
            // Get the next patrol point once at the current
            _currentPatrolPoint = (_currentPatrolPoint + 1) % _patrolPath.GetPatrolCount();
            GoToNextPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger observors when player touches this gameobject
        if (other.gameObject.tag == "Player")
        {
            EnemyTouched?.Invoke();
        }
    }

    private void IncreaseEnemyMovement()
    {
        // Increase the difficulty when coin is collected by increasing the agents speed
        _currentSpeed += 0.5f;
        _agent.speed = _currentSpeed;
    }

    private void GoToNextPoint()
    {
        // Get next point of patrol
        _currentTarget = _patrolPath.GetPointPos(_currentPatrolPoint);
        _agent.destination = _currentTarget.position;
    }

    public void OnDisable()
    {
        Coin.CoinCollected -= IncreaseEnemyMovement;
    }
}
