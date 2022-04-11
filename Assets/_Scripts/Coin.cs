using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool x, y, z;
    public float speed;

    public bool floatUpAndDown;
    Vector3 pointA, pointB;
    public float heightOffset;

    private bool quitting;

    public static event Action CoinCollected;

    private void Start() {
        Vector3 heightOffsetVector = new Vector3(0f, heightOffset, 0f);
        
        pointA = transform.position + heightOffsetVector;
        pointB = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(0.1f, 0.3f), transform.position.z) + heightOffsetVector;
    }

    private void FixedUpdate() {
        //Spin the object depending on what axis the user requested in edit mode
        if (x) 
        {
            transform.Rotate(speed, 0, 0);
        }
        if (y)
        {
            transform.Rotate(0, speed, 0);
        } 
        if (z)
        {
            transform.Rotate(0, 0, speed);
        }

        if(floatUpAndDown) 
        {
            transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        } 
    }

    private void OnDestroy()
    {
        if (!quitting)
        {
            CoinCollected?.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }
}
