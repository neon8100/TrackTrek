using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerID
{
    Player1,
    Player2
}

public class PlayerController : MonoBehaviour {

    public PlayerID playerNumber;
    int playerId;
    //public float lookAheadSpeed;
    //public float playerSpeed;

    public float speed;

    public Transform lookPointer;

    private void Awake()
    {

        playerId = (int)playerNumber + 1;

        foreach (string s in Input.GetJoystickNames())
        {
            Debug.Log(s);
        }

        onActionButtonDown = new UnityEvent();
        onActionButtonUp = new UnityEvent();
        onActionButtonStay = new UnityEvent();

        onActionButtonDown.AddListener(CheckActionInputs);


    }

    private void FixedUpdate()
    {
        
        float vertical = Input.GetAxis("VerticalP"+playerId);
        float horizontal = Input.GetAxis("HorizontalP"+playerId);

        Vector3 pos = new Vector3(transform.position.x, transform.position.y);
        Vector3 newPos = pos + new Vector3(horizontal * speed, vertical * speed);

        Vector3 vectorToTarget = newPos - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Mathf.Abs(vertical) >= 0.5 || Mathf.Abs(horizontal) >= 0.5f)
        {
            transform.rotation = q;
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);

        if (trackGhost != null)
        {
            trackGhost.transform.position = new Vector3(Mathf.Round(lookPointer.position.x), Mathf.Round(lookPointer.position.y));
        }

        if (contextButton && holdingMaterial)
        {
            PlaceTrack();
        }

        if (leftBumper)
        {
            SwapTrackGhost(-1);
        }
        else if (rightBumper)
        {
            SwapTrackGhost(1);
        }
    }

    private void LateUpdate()
    {
        //Button Event Listeners
        if (actionButton > 0)
        {
            if (!actionButtonIsDown)
            {
                onActionButtonDown.Invoke();
            }

            onActionButtonStay.Invoke();

            actionButtonIsDown = true;
        }
        else
        {
            onActionButtonUp.Invoke();
            actionButtonIsDown = false;
        }
    }


    //Checks collisions and then determines what to try and do
    public void OnCollisionStay2D(Collision2D collision)
    {
        this.collision = collision;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        this.collision = null;
    }

    bool holdingMaterial;

    public Collision2D collision;
    public CollectableMaterial material;


    void HandleMaterialCollision(CollectableMaterial material)
    {
        if (actionButton > 0 && this.material!=material)
        {
            material.Hold(gameObject);
            this.material = material;
            holdingMaterial = true;
            CreateTrackGhost();
            
        }
    }
    void HandleDestructableCollision(GatherableMaterial gatherable)
    {
        if (actionButton > 0)
        {
            gatherable.Damage();
        }
    }

    void CheckActionInputs()
    {
        if (holdingMaterial)
        {
            material.Drop();
            holdingMaterial = false;
            this.material = null;

            if (trackGhost != null)
            {
                Destroy(trackGhost);
            }
        }
        else
        {
            if(collision == null) { return; }
            if (collision.gameObject.GetComponent<CollectableMaterial>() != null)
            {
                //Handles a collision against a material object
                HandleMaterialCollision(collision.gameObject.GetComponent<CollectableMaterial>());
            }
            else if (collision.gameObject.GetComponent<GatherableMaterial>() !=null)
            {
                HandleDestructableCollision(collision.gameObject.GetComponent<GatherableMaterial>());
            }
        }
    }


    float actionButton { get { return Input.GetAxis("FireP" + playerId); } }
    bool contextButton { get { return Input.GetButtonDown("ContextP" + playerId); } }
    bool leftBumper { get { return Input.GetButtonDown("LBumpP"+playerId); } }
    bool rightBumper { get { return Input.GetButtonDown("RBumpP" + playerId); } }

    bool actionButtonIsDown;

    UnityEvent onActionButtonDown;
    UnityEvent onActionButtonStay;
    UnityEvent onActionButtonUp;

    public TrackAssets trackAssets;
    GameObject trackGhost;
    int trackIndex = 0;

    void CreateTrackGhost()
    {
        trackGhost = Instantiate(trackAssets.trackTypes[trackIndex]);
        trackGhost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
        GameEvents.events.onUIChooseTrack();
    }

    void SwapTrackGhost(int direction)
    {
        trackIndex += direction;

        if (trackIndex > trackAssets.trackTypes.Count - 1)
        {
            trackIndex = 0;
        }
        else if (trackIndex < 0)
        {
            trackIndex = trackAssets.trackTypes.Count - 1;
        }

        Destroy(trackGhost);
        CreateTrackGhost();
    }

    void PlaceTrack()
    {
        trackGhost.GetComponent<SpriteRenderer>().color = Color.white;
        
        trackGhost = null;

        Destroy(material.gameObject);

        this.material = null;

        holdingMaterial = false;

        GameEvents.events.onLayTrack();
    }
}
