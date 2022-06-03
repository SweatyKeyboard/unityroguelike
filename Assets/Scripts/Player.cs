using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;

    Rigidbody2D player;
    Vector2 moveInput, moveVelocity;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
}
