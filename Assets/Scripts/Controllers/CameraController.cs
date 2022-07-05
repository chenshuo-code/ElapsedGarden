using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed;
    private Camera gameCamera;

    // Use this for initialization
    void Start()
    {
        gameCamera = transform.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * Speed;
            transform.RotateAround(transform.position, Vector3.up, mouseX );
        }
    }
}
