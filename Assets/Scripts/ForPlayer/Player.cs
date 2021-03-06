using System;
using System.Linq;
using UnityEngine;
using System.Collections;

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

    [SerializeField] AudioClip hurtSound;

    [SerializeField] GameObject ChilliTrail;
    [SerializeField] GameObject GarlicSplash;


    Rigidbody2D player;
    Vector2 moveInput, moveVelocity;
    float lastFire;
    float rotation;
    float lastRotation;
    bool invulnerable = false;
    bool mobile = false;

    void Start()
    {
         Health = new int[8];

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
        Health[0] = MaxHealth;

        UpdateCharacteristics();
    }

    public void UpdateCharacteristics()
    {
        RateOfFire = 1.6f - (float)(Math.Pow(RateOfFireBar, 0.9) / 5);
        Damage = 2.6f + (float)(Math.Pow(1.5, DamageBar) / 7.5f);
        Range = 0.45f + (float)(Math.Pow(RangeBar, 0.9) / 7.94);
        Speed = 11 + SpeedBar * 2;
        BulletSpeed = 7 + BulletSpeedBar / 3;
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

    void Shoot(float x, float y) 
    {
        GetComponent<AudioSource>().Play();
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


                        switch (c)
                        {
                            case 2: AddEffect(Common.Effects.ShotSpeedUp, 5f); break;
                            case 3: AddEffect(Common.Effects.SpeedsUp, 3.5f); break;
                            case 4: GlobalEffects.AddEffectForAllEnemies(Common.Effects.Slowdown, 5f); break;
                            case 5: Instantiate(GarlicSplash, transform.position, transform.rotation); break;
                            case 6: GetComponent<TrailCreator>().MakeTrail(); break;
                            case 7: ChilliTrailStart(); break;
                        }
                    }

                    break;
                }
            }

            AudioSource.PlayClipAtPoint(hurtSound, new Vector3(0, 0, -10));
            GameController.Score -= 10;
            player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            invulnerable = true;
            FindObjectOfType<HudController>().UpdateHP();

            if (Health[0] == 0)
            {
                FindObjectOfType<GameController>().EndGame();
            }
            else
            Invoke("stillAlive", 0.5f);           
        }
    }

    void ChilliTrailStart()
    {
        invulnerable = true;
        Speed += 10;
        StartCoroutine(ChilliTrailCreator());
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
            Hurt("Bullet");
            Destroy(collision.gameObject);
        }
    }

    public void AddEffect(Common.Effects effect, float duration)
    {
        switch (effect)
        {
            case Common.Effects.ShotSpeedUp:
                StartCoroutine(ShotSpeedUpEffect(duration));
                break;

            case Common.Effects.SpeedsUp:
                {
                    StartCoroutine(BulletSppedUp(duration));
                    StartCoroutine(SpeedUp(duration));
                }
                break;
        }
    }

    IEnumerator ChilliTrailCreator()
    {
        for (int c = 0; c < 30; c++)
        {
            invulnerable = true;
            Instantiate(ChilliTrail, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.05f);
        }
        Speed -= 10;
        invulnerable = false;
    }

    IEnumerator ShotSpeedUpEffect(float duration)
    {
        RateOfFire /= 2;
        yield return new WaitForSeconds(duration);
        RateOfFire *= 2;
    }

    IEnumerator SpeedUp(float duration)
    {
        Speed += 3;
        yield return new WaitForSeconds(duration);
        Speed -= 3;
    }

    IEnumerator BulletSppedUp(float duration)
    {
        BulletSpeed += 3;
        yield return new WaitForSeconds(duration);
        BulletSpeed -= 3;
    }
}
