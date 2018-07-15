using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofCollider : MonoBehaviour {

    /*
    SpriteRenderer sr;
    Color color;
    
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        transform.localPosition = new Vector3(0, 0, col.transform.position.z - 0.1f);
        if (col.tag == "GG")
        {
            color.a = 0.85f;
            sr.color = color;
        }
        
    }
    void OnTriggerExit2D(Collider2D col)

    {
        transform.localPosition = new Vector3(0, 0, 0);
        if (col.tag == "GG")
        {
            color.a = 1f;
            sr.color = color;
        }
    }*/
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "GG" || col.tag == "NPC" || (col.tag == "Enemy" && !col.isTrigger))
            col.gameObject.GetComponent<SpriteRenderer>().color = new Color(col.gameObject.GetComponent<SpriteRenderer>().color.r, col.gameObject.GetComponent<SpriteRenderer>().color.g, col.gameObject.GetComponent<SpriteRenderer>().color.b, 0.6f);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "GG" || col.tag == "NPC" || (col.tag == "Enemy" && !col.isTrigger))
            col.gameObject.GetComponent<SpriteRenderer>().color = new Color(col.gameObject.GetComponent<SpriteRenderer>().color.r, col.gameObject.GetComponent<SpriteRenderer>().color.g, col.gameObject.GetComponent<SpriteRenderer>().color.b, 1.0f);
    }

}
