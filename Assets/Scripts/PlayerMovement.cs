using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;
    
    private Vector2 _movementInput;
    
    [SerializeField] private Rigidbody2D Rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        MovePlayer();
    }


    void GetInputs()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");
        
        _movementInput.Normalize();
    }

    void MovePlayer()
    {
        Rb.velocity = _movementInput * MoveSpeed;
    }
}
