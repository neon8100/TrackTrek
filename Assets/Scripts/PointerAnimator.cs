using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAnimator : MonoBehaviour {

    BoxCollider2D box;
    private void Awake()
    {
        box = gameObject.GetComponent<BoxCollider2D>();
        
    }

    public bool unbuildable;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            unbuildable = true;
        }
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            unbuildable = true;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Block")
        {
            unbuildable = true;

        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            unbuildable = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        unbuildable = false;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        unbuildable = false;
    }
}
