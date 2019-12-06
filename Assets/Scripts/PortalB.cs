using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalB : MonoBehaviour
{
    GameObject portal; 
    public bool available = true;
    void Awake()
    {
        portal = GameObject.Find("portalY");
    }

    void OnTriggerEnter2D(Collider2D collison){
        if(collison.tag == "Ball" && available){
            collison.transform.position = portal.transform.position;
            portal.GetComponent<PortalY>().available = false;
            Invoke("enable",1f);
        }
    }
    void enable(){
        portal.GetComponent<PortalY>().available = true;  
    }
}
