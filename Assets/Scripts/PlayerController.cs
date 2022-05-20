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
    public PlantBehaviour Plant;
    //Line
    private LineRenderer line;
    private int linePointCount;
    private bool canDrawLine;

    //Ray
    private Ray ray;
    private RaycastHit raycastHit;
    
    public void Init()
    {
        line = GetComponent<LineRenderer>();
        canDrawLine = false;
        PlayerState = PlayerState.OnStay;
    }
    void Update()
    {
        if (canDrawLine)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit,500f))
            {
                if (raycastHit.collider.CompareTag("Ground"))
                {
                    DrawLine(raycastHit.point);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            DrawLine(false);
            PlayerState = PlayerState.OnStay;
        }
    }
   private void DrawLine(Vector3 _pos)
    {
        linePointCount++;
        line.positionCount = linePointCount;
        line.SetPosition(linePointCount - 1, _pos);
    }
   public void DrawLine(bool drawLine)
    {
        if (drawLine)
        {
            canDrawLine = true;
        }
        else
        {
            canDrawLine = false;
            linePointCount = 0;
            line.positionCount = 0;
        }
    }
}
