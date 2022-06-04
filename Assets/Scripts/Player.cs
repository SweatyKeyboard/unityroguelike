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
    float rotation;
    float lastRotation;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        lastFire = Time.time;
    }

    void Update()
    {
        float shootHorizontal = Input.GetAxisRaw("HorizontalShooting"),
              shootVertical = Input.GetAxisRaw("VerticalShooting");

       float moveHorizontal = Input.GetAxisRaw("HorizontalMoving"),
              moveVertical = Input.GetAxisRaw("VerticalMoving");

        moveInput = new Vector2(moveHorizontal, moveVertical);
        moveVelocity = Speed * moveInput.normalized;        


        if (moveHorizontal != 0 || moveVertical != 0)
            rotation = Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg;

        if ( (shootHorizontal != 0 || shootVertical != 0)
              && Time.time > lastFire + FireDelay
              && (Math.Abs(shootHorizontal) - Math.Abs(shootVertical) != 0))
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }
    }

    void FixedUpdate()
    {
        player.MovePosition(player.position + moveVelocity * Time.fixedDeltaTime);
        player.transform.rotation = Quaternion.Euler(new Vector3(0,0,rotation));
    }

    void Shoot(float x, float y) 
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position + new Vector3(x*0.7f,y*0.7f,0), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            moveVelocity.x * 0.5f + x * BulletSpeed,
            moveVelocity.y * 0.5f + y * BulletSpeed,
            0);
    }
}
