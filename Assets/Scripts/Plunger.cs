using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[12];
    private int spid = 11;
    private GameObject ball;
    private Rigidbody2D rb;
    private bool ejecting = false;
    void Awake()
    {
        ball = GameObject.Find("Ball");
        rb = ball.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(spid >= 0)
            GetComponent<SpriteRenderer>().sprite = sprites[spid];
        if(Input.GetKeyDown(KeyCode.E)&&!ejecting){
            ejecting = true;
            eject();
        }
        if(ejecting){
            rb.AddForce(new Vector2(0f,200f));
        }        
    }
    void eject(){
        Debug.Log("e");
    }
    void updateSprite(){
        if(spid >= 0)
            spid--;
    }
    void OnTriggerExit2D(){
        Debug.Log("Exit");
        ejecting = false;
    }
    IEnumerator wait(float waittime){
        yield return new WaitForSeconds(waittime);
        Debug.Log(Time.time);
        updateSprite();
    }
}
