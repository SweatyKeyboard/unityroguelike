using UnityEngine;

public class RoamingEnemy : Enemy
{
    [SerializeField] Common.RoamingType type;
    [SerializeField] float trailLength;
    [SerializeField] GameObject trail;

    bool movingNow = false;
    bool trailStopper = false;
    Vector3 targetPosition;
    Quaternion targetAngle;

    public override void Start()
    {
        base.Start();
          
        trail.GetComponent<EnemyTrail>().Length = trailLength;
    }
    public override void Move()
    {
        if (type == Common.RoamingType.FollowPlayer)
        {
            Vector3 difference = target.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        }
        else if (type == Common.RoamingType.Random)
        {
           
            if (!movingNow)
            {
                movingNow = true;
                targetPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f)) - transform.position;
                targetAngle = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
            }

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                movingNow = false;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }
        if (!trailStopper)
        {
            Instantiate(trail, transform.position, transform.rotation);
            trailStopper = true;
            Invoke("TrailStart", 0.2f / Speed);
        }


    }

    void TrailStart()
    {
        trailStopper = false;
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            Hurt(target.Damage, 0.1f);
        }
        else
        {
            movingNow = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        movingNow = false;
    }
}
