using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableMaterial : MonoBehaviour {

    public GameObject dropOnDesttroy;

    public int maxHealth;

    int currentHealth;

    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void Damage()
    {
        GameEvents.events.onInteractResource();

        currentHealth--;

        Pop();

        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    void Pop()
    {
        transform.localScale = new Vector3(1f,1f);
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f), 0.1f).setLoopPingPong(1);
    }

    public void OnDestroy()
    {
        GameObject obj = Instantiate(dropOnDesttroy);
        obj.transform.position = transform.position;
    }




}
