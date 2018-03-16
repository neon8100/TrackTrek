using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    Player1,
    Player2
}

public class PlayerController : MonoBehaviour {

    public PlayerID playerNumber;
    public float lookAheadSpeed;
    public float playerSpeed;

    Transform lookTarget;

    private void Awake()
    {
        GameObject t = new GameObject();
        lookTarget = t.transform;

        foreach(string s in Input.GetJoystickNames())
        {
            Debug.Log(s);
        }

    }

    private void Update()
    {
        int playerId = (int)playerNumber;

        float vertical = Input.GetAxis("VerticalP"+playerId);
        float horizontal = Input.GetAxis("HorizontalP"+playerId);

        Vector2 pos = new Vector2(lookTarget.position.x, lookTarget.position.y);
        Vector2 newPos = pos + new Vector2(horizontal * lookAheadSpeed, vertical * lookAheadSpeed);

        lookTarget.position = newPos;

        transform.up = lookTarget.position;
        transform.position = Vector3.Lerp(transform.position, lookTarget.position, playerSpeed);

    }
}
