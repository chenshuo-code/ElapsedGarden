using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideFlux : MonoBehaviour
{
    //Public parameter
    public float MoveSpeed;

    private Ray ray;
    private RaycastHit raycastHit;

    //Line
    private LineRenderer line;
    private int linePointCount;

    private  bool canMove;
    public void Init()
    {
        canMove = false;
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        //Raycast test
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 500f))
        {
            if (!raycastHit.transform.CompareTag("GuideFlux") && canMove)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, raycastHit.point, Time.deltaTime * MoveSpeed);
                DrawLine(this.transform.position);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
        }
    }
    private void DrawLine(Vector3 _pos)
    {
        linePointCount++;
        line.positionCount = linePointCount;
        line.SetPosition(linePointCount - 1, _pos);
    }

    #region Public functions

    /// <summary>
    /// Teleport this guide flux to a giving position
    /// </summary>
    /// <param name="telePos">Position to teleport</param>
    public void TeleportToMove(Vector3 telePos)
    {
        this.transform.position = telePos;
        canMove = true;
    }

    /// <summary>
    /// Efface all line when we restart a match
    /// </summary>
    public void EffaceLine()
    {
        linePointCount = 0;
        line.positionCount = 0;
    }

    public void ReservedLastPoint()
    {

    }
    #endregion
}
