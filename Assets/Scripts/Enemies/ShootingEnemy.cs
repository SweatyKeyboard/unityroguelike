using System;
using UnityEngine;
class ShootingEnemy : Enemy
{
    [SerializeField] float minShootDistance, maxShootDistance;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float range;
    [SerializeField] float rateOfFire;
    [SerializeField] AudioClip sound;

    float lastTime;

    public override void Start()
    {
        base.Start();
        bulletSpeed += (int)(Math.Pow(1.5, GameController.FloorsCompleted) * 2) / 3;
        rateOfFire += (int)(Math.Pow(1.5, GameController.FloorsCompleted) * 2) / 2;
        GetComponent<AudioSource>().clip = sound;
    }
    public override void Move()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance >= minShootDistance)
        {
            /* Vector3 difference = target.transform.position - transform.position;
             float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
             Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);
             transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
             transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);*/
            
            Vector3 dirr = target.transform.position - transform.position;
            dirr = Vector3.Normalize(dirr);
            GetComponent<Rigidbody2D>().AddForce(dirr * Speed * 3, ForceMode2D.Force);

            Vector3 difference = target.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }

        if (distance < maxShootDistance)
        {
            Vector3 difference = target.transform.position - transform.position;
            difference.Normalize();
            if (lastTime + rateOfFire < Time.time)
            {
                Shoot(difference.x, difference.y);
                lastTime = Time.time;
            }
        }

        /*float distance = Vector3.Distance(target.transform.position, transform.position);
        float speedModiefer = (distance < 1f) ? 10 : 1;
        Vector3 dir = target.transform.position - transform.position;
        dir *= 1 / distance;
        dir = Vector3.Normalize(dir);
        GetComponent<Rigidbody2D>().AddForce(dir*Speed*5*speedModiefer, ForceMode2D.Impulse;*/

    }

    public void Shoot(float x, float y)
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
        GameObject bull = Instantiate(bullet, transform.position + new Vector3(x * 0.66f, y * 0.66f, 0), transform.rotation);
        bull.GetComponent<Rigidbody2D>().velocity = new Vector3(
            x * bulletSpeed,
            y * bulletSpeed,
            0);
        bull.GetComponent<BulletController>().Lifetime = range;
    }
}
