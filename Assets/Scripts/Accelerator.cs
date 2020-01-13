using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    private float acc = 70f;
    private float trac = 3f;
    void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Ball"){
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * acc);
            Vector3 dis = transform.position - collision.transform.position;
            Vector3 dist = dis - Vector2.Dot(dis,transform.up.normalized) * transform.up.normalized;
            if(dist.magnitude > 1f){
                rb.AddForce(dist.normalized * trac);
            }
        }
    }
}
