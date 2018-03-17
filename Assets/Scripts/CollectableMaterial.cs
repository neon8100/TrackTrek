using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMaterial : MonoBehaviour
{
    Rigidbody2D body;

    public GameObject promptPrefab;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        GameEvents.events.onResourceCreated();
    }

    GameObject parent;

    public void Hold(GameObject character)
    {

        body.simulated = false;
        parent = character;
        GameEvents.events.onPickupResource(character.GetComponent<PlayerController>());
    }

    public void Drop()
    {
        body.simulated = true;
        parent = null;
        GameEvents.events.onDropItem();
    }

    public void Update()
    {
        if (parent == null) { return; }
        else
        {
            transform.position = parent.transform.position;
        }
    }

    GameObject prompt;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            prompt = Instantiate(promptPrefab);
            prompt.transform.position = new Vector3(transform.position.x, transform.position.y + 1);
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (prompt != null)
            {
                prompt.transform.position = new Vector3(transform.position.x, transform.position.y + 1);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (prompt != null)
        {
            Destroy(prompt);
        }
    }






}
