using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAnimator : MonoBehaviour {

    private void OnEnable()
    {
        LeanTween.scale(gameObject, new Vector3(0.85f, 0.85f, 0.85f), 0.5f).setLoopPingPong();
    }
}
