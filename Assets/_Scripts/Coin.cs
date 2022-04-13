using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static float _speed = 4f;
    private static float _heightOffset = 1f;
    private Vector3 _pointA, _pointB;
    private bool _quitting;
    public static event Action CoinCollected;

    private void Start() {
        // Get offset for 'moving up and down' movement
        Vector3 heightOffsetVector = new Vector3(0f, _heightOffset, 0f);
        
        // Get the two offsets for this movement
        _pointA = transform.position + heightOffsetVector;
        _pointB = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(0.1f, 0.3f), transform.position.z) + heightOffsetVector;
    }

    private void FixedUpdate() {
        //Spin the object on the y-axis
        transform.Rotate(0, _speed, 0);
        // Oscillate coin up and down
        transform.position = Vector3.Lerp(_pointA, _pointB, Mathf.PingPong(Time.time, 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the coin when in contact with the player
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        } 
    }

    private void OnDestroy()
    {
        // Invoke any observors of the coin being collected
        if (!_quitting)
        {
            Debug.Log("here?");
            CoinCollected?.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
        // Ensure that action doesnt trigger when closing the game
        _quitting = true;
    }
}
