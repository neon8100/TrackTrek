using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPiece : MonoBehaviour
{
    BoxCollider2D boxCollider;

    [Tooltip("The connection point co-ordinates for this track piece.")]
    public Vector2[] connectionPoints;

    private void Awake()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

}
