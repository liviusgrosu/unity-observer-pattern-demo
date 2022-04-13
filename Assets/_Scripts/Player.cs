using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody _rb;
    private bool inputOff;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Enemy.EnemyTouched += TurnOffInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputOff)
        {
            // Stop all input
            _rb.velocity = Vector3.zero;
            return;
        }
        // Eliminate double speed with multiple inputs
        Vector3 movementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movementDirection = movementDirection.normalized;

        // Rotate the player based on their movement
        if (movementDirection != Vector3.zero)
        {        
            // Apply speed to rigidbody velocity
            _rb.velocity = movementDirection * speed;
        }
        else 
        {
            // Stop moving
            _rb.velocity = Vector3.zero;
        }
    }

    private void TurnOffInput()
    {
        inputOff = true;
    }
}
