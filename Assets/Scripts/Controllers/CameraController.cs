using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed;
    float _rotateY = 0;
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
            float _mouseX = Input.GetAxis("Mouse X") * Speed;
            transform.RotateAround(transform.position, Vector3.up, _mouseX);
        }
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            _rotateY += Input.GetAxis("Mouse ScrollWheel")*20;
            _rotateY = Mathf.Clamp(_rotateY * Speed, -15, 15);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x + _rotateY, transform.rotation.eulerAngles.y, 0));
            
        }

    }
}
