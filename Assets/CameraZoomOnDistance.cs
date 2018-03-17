using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOnDistance : MonoBehaviour {

    Cinemachine.CinemachineVirtualCamera cam;
    public GameObject p1;
    public GameObject p2;
    public float minZoom = 6;
    public float maxZoom = 8;

    public float zoomSpeed;

    public void Awake()
    {
        cam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        currentSize = minZoom;
    }

    float currentSize;

    float distance;

    float maxDistance = 20f;

    public void Update()
    {
        cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, currentSize, Time.deltaTime * zoomSpeed);

        //determine new size based on distnace;
        distance = Vector2.Distance(p1.transform.position, p2.transform.position);

        float percent = distance / maxDistance;

        currentSize = (maxZoom * percent) + minZoom;

        if (currentSize > maxZoom)
        {
            currentSize = maxZoom;
        }



        /*
        if (currentSize < minZoom)
        {
            currentSize = minZoom;
        }*/
        
        
    }
}
