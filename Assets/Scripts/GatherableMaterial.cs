using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableMaterial : MonoBehaviour {

    public GameObject dropOnDesttroy;

    public Vector2 healthRange;
    int maxHealth;

    int currentHealth;

    Rigidbody2D body;

    public GameObject promptPrefab;

    public HealthBar healthBar;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        maxHealth = (int)Random.Range(healthRange.x, healthRange.y);
        currentHealth = maxHealth;
    }

    public void Damage()
    {
        healthBar.gameObject.SetActive(true);

        GameEvents.events.onInteractResource();

        currentHealth--;

        healthBar.currentValue = currentHealth;

        Pop();

        if (currentHealth < 0)
        {
            Drop();
            Destroy(gameObject);
        }
    }

    void Drop()
    {
        GameObject obj = Instantiate(dropOnDesttroy);
        obj.transform.position = transform.position;
    }

    void Pop()
    {
        transform.localScale = new Vector3(1f,1f);
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f), 0.1f).setLoopPingPong(1);
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
        healthBar.gameObject.SetActive(false);

        if (prompt != null)
        {
            Destroy(prompt);
        }
    }


}
