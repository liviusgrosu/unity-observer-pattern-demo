using System;
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
    private float _currentSpeed;
    private NavMeshAgent _agent;

    public static event Action EnemyTouched;

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentSpeed = _speed;
        _agent.speed = _currentSpeed;
    }

    void Start()
    {
        Coin.CoinCollected += IncreaseEnemyMovement;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnemyTouched?.Invoke();
        }
    }

    private void IncreaseEnemyMovement()
    {
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
