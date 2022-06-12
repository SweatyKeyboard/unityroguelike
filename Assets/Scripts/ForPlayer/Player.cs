using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int SpeedBar;
    public int BulletSpeedBar;
    public int RateOfFireBar;
    public int RangeBar;
    public int DamageBar;
    float Speed;
    float BulletSpeed;
    float RateOfFire;
    float Range;
    public float Damage;


    public GameObject BulletPrefab;
    public GameObject ParticlesPrefab;
    public int MaxHealth;
    public int[] Health;
    public FixedJoystick MovingJoystick;
    public FixedJoystick ShootingJoystick;


    Rigidbody2D player;
    Vector2 moveInput, moveVelocity;
    float lastFire;
    float rotation;
    float lastRotation;
    bool invulnerable = false;
    bool mobile = false;

    void Start()
    {
        if (Application.isMobilePlatform)
            mobile = true;

        player = GetComponent<Rigidbody2D>();
        lastFire = Time.time;

        SpeedBar = PlayerPrefs.GetInt("SkillsSpeed");
        BulletSpeedBar = PlayerPrefs.GetInt("SkillsBulletSpeed");
        RateOfFireBar = PlayerPrefs.GetInt("SkillsRoF");
        DamageBar = PlayerPrefs.GetInt("SkillsDamage");
        RangeBar = PlayerPrefs.GetInt("SkillsRange");
        MaxHealth = PlayerPrefs.GetInt("SkillsMaxHealth") * 2;

        UpdateCharacteristics();
    }

    public void UpdateCharacteristics()
    {
        RateOfFire = 1.66f - (float)(Math.Pow(RateOfFireBar, 0.9) / 5);
        Damage = 2.5f + (float)(Math.Pow(1.5, DamageBar) / 10);
        Range = 0.4f + (float)(Math.Pow(RangeBar, 0.9) / 7.94);
        Speed = 10 + SpeedBar * 2;
        BulletSpeed = 5 + BulletSpeedBar / 4;
    }

    void Update()
    {
        if (!FindObjectOfType<GameController>().Pause)
        {
            float shootHorizontal, shootVertical;
            if (!mobile)
            {
                shootHorizontal = Input.GetAxisRaw("HorizontalShooting");
                shootVertical = Input.GetAxisRaw("VerticalShooting");
            }
            else
            {
                shootHorizontal = ShootingJoystick.Horizontal;
                shootVertical = ShootingJoystick.Vertical;
            }


            if (shootHorizontal != 0 || shootVertical != 0)
                rotation = Mathf.Atan2(-shootHorizontal, shootVertical) * Mathf.Rad2Deg;

            if ((shootHorizontal != 0 || shootVertical != 0)
                  && Time.time > lastFire + RateOfFire
                  && (Math.Abs(shootHorizontal) - Math.Abs(shootVertical) != 0))
            {
                Shoot(shootHorizontal, shootVertical);
                lastFire = Time.time;
            }
        }
    }

    void FixedUpdate()
    {
        Move();
        player.transform.rotation = Quaternion.Euler(new Vector3(0,0,rotation));
    }

    void Move()
    {
        float moveHorizontal, moveVertical;
        if (!mobile)
        {
            moveHorizontal = Input.GetAxisRaw("HorizontalMoving");
            moveVertical = Input.GetAxisRaw("VerticalMoving");
        }
        else
        {
            moveHorizontal = MovingJoystick.Horizontal;
            moveVertical = MovingJoystick.Vertical;
        }
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        player.AddForce(movement * Speed * 5);
        
    }

   /* void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Hurt();
        }
    }*/

    void Shoot(float x, float y) 
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position + new Vector3(x*0.5f,y*0.5f,0), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            moveVelocity.x * 0.25f + x * BulletSpeed,
            moveVelocity.y * 0.25f + y * BulletSpeed,
            0);
        bullet.GetComponent<BulletController>().Lifetime = Range;

        GameObject particles = Instantiate(ParticlesPrefab, transform.position + new Vector3(x*0.5f,y*0.5f,0), Quaternion.Euler(new Vector3(0,0,rotation)));
        particles.AddComponent<Rigidbody2D>().velocity = new Vector3(
            moveVelocity.x,
            moveVelocity.y,
            0);
        particles.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void stillAlive()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        invulnerable = false;

    }

    public void Hurt(string who)
    {
        if (!invulnerable)
        {
            for (int c = Health.Length - 1; c >= 0; c--)
            {
                if (Health[c] > 0)
                {
                    Health[c]--;
                    if (c!=0 && Health[c]%2==0)
                    {
                        GameObject[] bars = GameObject.FindGameObjectsWithTag("HealthHUD");
                        Destroy(bars.Last());
                        Destroy(bars.Last());

                        if (c == 2)
                            GetComponent<TrailCreator>().MakeTrail();
                    }

                    break;
                }
            }

            if (Health[0] == 0)
            {
                FindObjectOfType<GameController>().EndGame();
            }

            GameController.Score -= 10;
            player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            invulnerable = true;
            FindObjectOfType<HudController>().UpdateHP();
            Invoke("stillAlive", 0.5f);
        }


    }

    public void AddHP(Common.HealthType type)
    {
        if (Health.Sum() < 20)
        {
            if (type == Common.HealthType.Ketchup)
            {
                if (Health[0] + 2 < MaxHealth)
                    Health[0] += 2;
                else
                    Health[0] = MaxHealth;
            }
            else
            {
                Health[(int)type] += 2;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Hurt("Пуля");
            Destroy(collision.gameObject);
        }
    }
}
