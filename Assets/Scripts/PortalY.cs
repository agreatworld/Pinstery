using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalY : MonoBehaviour
{
    GameObject portal;
    public bool available = true;
    void Awake()
    {
        portal = GameObject.Find("portalB");
    }

    void OnTriggerEnter2D(Collider2D collison){
        if(collison.tag == "Ball" && available){
            collison.transform.position = portal.transform.position;
            portal.GetComponent<PortalB>().available = false;
            Invoke("enable",1f);
        }
    }
    void enable(){
        portal.GetComponent<PortalB>().available = true;  
    }
}
