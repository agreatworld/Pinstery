using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baffle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Ball"){
            Vector2 v = collision.GetComponent<Rigidbody2D>().velocity;
            Vector2 vt = Vector2.Dot(v,transform.up.normalized) * transform.up.normalized;
            collision.GetComponent<Rigidbody2D>().velocity = v - 2*vt;
            Destroy(gameObject);
        }
    }
}
