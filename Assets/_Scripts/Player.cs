using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rb;
    private bool _inputOff;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Enemy.EnemyTouched += TurnOffInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputOff)
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
            _rb.velocity = movementDirection * _speed;
        }
        else 
        {
            // Stop moving
            _rb.velocity = Vector3.zero;
        }
    }

    private void TurnOffInput()
    {
        // Disable player input
        _inputOff = true;
    }
}
