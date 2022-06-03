using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    public GameObject BulletPrefab;
    public float BulletSpeed;
    public float FireDelay;

    Rigidbody2D player;
    Vector2 moveInput, moveVelocity;
    float lastFire;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        lastFire = Time.time;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("HorizontalMoving"), Input.GetAxisRaw("VerticalMoving"));
        moveVelocity = Speed * moveInput.normalized;

        float shootHorizontal = Input.GetAxisRaw("HorizontalShooting"),
              shootVertical = Input.GetAxisRaw("VerticalShooting");

        if ( (shootHorizontal != 0 || shootVertical != 0)
              && Time.time > lastFire + FireDelay
              && (Math.Abs(shootHorizontal) - Math.Abs(shootVertical) != 0))
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }

        player.MovePosition(player.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Shoot(float x, float y) 
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(x*BulletSpeed, y*BulletSpeed, 0);
    }
}
