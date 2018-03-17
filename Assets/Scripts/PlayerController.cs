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

    public GameObject sprite;

    public PlayerSpriteAnimator animator;

    private void Awake()
    {

        playerId = (int)playerNumber + 1;

        lookPointer.gameObject.SetActive(false);

        foreach (string s in Input.GetJoystickNames())
        {
            Debug.Log(s);
        }

        onActionButtonDown = new UnityEvent();
        onActionButtonUp = new UnityEvent();
        onActionButtonStay = new UnityEvent();

        onActionButtonDown.AddListener(CheckActionInputs);


    }

    float x = 0;
    float y = 0;

    float horizontal;
    float vertical;

    private void Update()
    {
         vertical = Input.GetAxis("VerticalP" + playerId);
        horizontal = Input.GetAxis("HorizontalP" + playerId);

        if (actionButton > 0)
        {
            animator.SwitchState(PlayerSpriteAnimator.State.Action, true);
        }
        else if (vertical == 0 && horizontal == 0)
        {
            animator.SwitchState(PlayerSpriteAnimator.State.Idle);
        }
        else
        {
            animator.SwitchState(PlayerSpriteAnimator.State.Moving);
            if (horizontal > 0)
            {
                sprite.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                sprite.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void FixedUpdate()
    {

        Vector3 pos = new Vector3(transform.position.x, transform.position.y);
        Vector3 newPos = pos + new Vector3(horizontal * speed, vertical * speed);

        Vector3 vectorToTarget = newPos - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);


        if (Mathf.Abs(vertical) >= 0.5 || Mathf.Abs(horizontal) >= 0.5f)
        {
            transform.rotation = q;
            sprite.transform.localRotation = Quaternion.Inverse(transform.rotation);
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

        if (lookPointer.gameObject.activeInHierarchy)
        {
           
            if (vertical > 0.5f)
            {
                y = 1;
                x = 0;
                x = Mathf.Round(transform.position.x) + x;
                y = Mathf.Round(transform.position.y) + y;

            }
            else if(vertical < -0.5f)
            {
                y = -1;
                x = 0;
                x = Mathf.Round(transform.position.x) + x;
                y = Mathf.Round(transform.position.y) + y;
            }
            else if(horizontal > 0.5f)
            {
                x = 1;
                y = 0;
                x = Mathf.Round(transform.position.x) + x;
                y = Mathf.Round(transform.position.y) + y;
               
            }
            else if (horizontal < -0.5f)
            {
                x = -1;
                y = 0;
                x = Mathf.Round(transform.position.x) + x;
                y = Mathf.Round(transform.position.y) + y;
            }

            lookPointer.position = new Vector3(x, y, 0);
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
        lookPointer.gameObject.SetActive(true);
        trackGhost = Instantiate(trackAssets.trackTypes[trackIndex]);
        trackGhost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
        trackGhost.GetComponent<TrackPiece>().boxCollider.enabled = false;
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
        trackGhost.GetComponent<TrackPiece>().boxCollider.enabled = true;
        trackGhost = null;

        Destroy(material.gameObject);

        this.material = null;

        holdingMaterial = false;

        GameEvents.events.onLayTrack();

        lookPointer.gameObject.SetActive(false);
    }
}
