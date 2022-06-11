using UnityEngine;
class ShootingEnemy : Enemy
{
    public float MinShootDistance, MaxShootDistance;
    public GameObject Bullet;
    public float BulletSpeed;
    public float Range;
    public float RateOfFire;

    float lastTime;
    public override void Move()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance >= MinShootDistance)
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

        if (distance < MaxShootDistance)
        {
            Vector3 difference = target.transform.position - transform.position;
            if (lastTime + RateOfFire < Time.time)
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
        GameObject bullet = Instantiate(Bullet, transform.position + new Vector3(x * 0.3f, y * 0.3f, 0), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            x * BulletSpeed,
            y * BulletSpeed,
            0);
        bullet.GetComponent<BulletController>().Lifetime = Range;
    }
}
