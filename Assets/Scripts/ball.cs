using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private float speed = 4f;
    private float drec = 20;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(1,-1)*speed;
    }

    void Update()
    {
        
    }
}
