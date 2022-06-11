using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    
    public override void Move()
    {
        Vector3 difference = target.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);


        /*float distance = Vector3.Distance(target.transform.position, transform.position);
        float speedModiefer = (distance < 1f) ? 10 : 1;
        Vector3 dir = target.transform.position - transform.position;
        dir *= 1 / distance;
        dir = Vector3.Normalize(dir);
        GetComponent<Rigidbody2D>().AddForce(dir*Speed*5*speedModiefer, ForceMode2D.Impulse);*/

    }
}
