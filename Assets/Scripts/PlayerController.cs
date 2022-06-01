using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player state enum
/// </summary>
public enum PlayerState 
{
    OnSpend,
    OnReturn,
    OnStay,
}

/// <summary>
/// Player Behavier controller
/// </summary>
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerState PlayerState;
    public float TimeShipping; //Time on way to return back
  
    //Line
    private LineRenderer line;
    private int linePointCount;
    private bool canDrawLine;

    //Ray
    private Ray ray;
    private RaycastHit raycastHit;

    private Vector3 savePointLocation;

    public void Init()
    {
        line = GetComponent<LineRenderer>();
        canDrawLine = false;
        PlayerState = PlayerState.OnStay;
    }
    void Update()
    {
        //Raycast test
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 500f))
        {
            //Draw line
            if (canDrawLine) 
            {
                if (raycastHit.collider.CompareTag("Ground"))
                {
                    DrawLine(raycastHit.point);
                }
            }
        }


        if (Input.GetMouseButtonUp(0)|| Input.GetMouseButtonUp(1)) //On mouse up, we cancel the draw line
        {
            ActivateDrawLine(false);
            PlayerState = PlayerState.OnStay;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }
    private void DrawLine(Vector3 _pos)
    {
        linePointCount++;
        line.positionCount = linePointCount;
        line.SetPosition(linePointCount - 1, _pos);
    }
    //Efface all line when we restart a match
    public void EffaceLine()
    {
        linePointCount = 0;
        line.positionCount = 0;
    }
    public void ActivateDrawLine(bool drawLine)
    {
        if (drawLine)
        {
            canDrawLine = true;
        }
        else
        {
            canDrawLine = false;
        }
    }
}
