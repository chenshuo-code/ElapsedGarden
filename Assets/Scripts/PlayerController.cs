using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Behavier controller
/// </summary>
public class PlayerController : MonoBehaviour
{
    //Line
    private LineRenderer line;
    private int linePointCount;

    private Ray ray;
    private RaycastHit raycastHit;

    
    void Start()
    {
        line = GetComponent<LineRenderer>();
        
    }
    void Update()
    {

        if (Input.GetMouseButton(0))
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
            CancelLine();
        }
    }
   private void DrawLine(Vector3 _pos)
    {
        linePointCount++;
        line.positionCount = linePointCount;
        line.SetPosition(linePointCount - 1, _pos);
    }
   private void CancelLine()
    {
        linePointCount = 0;
        line.positionCount = 0;
    }
}
