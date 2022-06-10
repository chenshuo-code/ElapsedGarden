using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Player Behavier controller
/// Control Player's movement, and draw line
/// </summary>
public class PlayerController : MonoBehaviour
{
    //Public parameters

    /// <summary>
    /// Speed of player move
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// Flux cosume during the road
    /// </summary>
    public float FluxConsume;

    //private parameters

    private Ray ray;
    private RaycastHit raycastHit;

    //Line
    private LineRenderer line;
    private int linePointCount;

    private bool canMove;

    //UI
    private Transform canvas;
    private Image lifeBar;
    private float lifeDisplayRate;
    private TMP_Text LifeNum;

    private GuideFluxBehaviour guideFlux;

    public void Init()
    {
        canMove = false;
        
        line = GetComponent<LineRenderer>();
        guideFlux = transform.GetComponent<GuideFluxBehaviour>();

        //UI Bar
        canvas = transform.Find("Canvas");
        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        LifeNum = transform.Find("Canvas/LifeNum").GetComponent<TMP_Text>();
    }
    private void Update()
    {

        //Raycast test
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 500f))
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                canMove = true;
            }
            if (!raycastHit.transform.CompareTag("GuideFlux") && canMove)
            {
                //Move player to cursor
                if (Vector3.Distance(raycastHit.point, transform.position) <= 10)
                {
                    this.transform.position = Vector3.MoveTowards(transform.position, raycastHit.point, MoveSpeed);
                }
                else
                {
                    this.transform.position = Vector3.Lerp(transform.position, raycastHit.point, MoveSpeed / 10);
                }

                DrawLine(this.transform.position);

                //Reduce Flux on road
                guideFlux.ReduceFlux(FluxConsume);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
        }
    }
    /// <summary>
    /// Draw line from given position
    /// </summary>
    /// <param name="_pos"></param>
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
    public void TeleportToPosition(Vector3 telePos)
    {
        this.transform.position = telePos;
    }

    /// <summary>
    /// Efface all line when we restart a match
    /// </summary>
    public void EffaceLine()
    {
        linePointCount = 0;
        line.positionCount = 0;
    }

    #endregion
}
