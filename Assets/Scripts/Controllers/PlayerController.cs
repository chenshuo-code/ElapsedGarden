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

    private Ray rayCursor;
    private RaycastHit raycastHitCursor;
    private Ray rayForward;
    private RaycastHit raycastHitForward;
    
    //Trail
    private TrailRenderer trail;

    private new Rigidbody rigidbody;
    private Camera gameCamera;

    private bool canMove;
    private bool isBlocked;

    //Particle Trace
    private ParticleSystem particleTrace;

    //UI
    private Transform canvas;
    private Image lifeBar;
    private float lifeDisplayRate;
    private TMP_Text LifeNum;

    private GuideFluxBehaviour guideFlux;

    public void Init()
    {
        canMove = false;
        isBlocked = false;

        trail = GetComponentInChildren<TrailRenderer>();
        particleTrace = transform.Find("ParticleTrace").GetComponent<ParticleSystem>();
        rigidbody = GetComponent<Rigidbody>();
        guideFlux = transform.GetComponent<GuideFluxBehaviour>();
        gameCamera = transform.GetComponentInChildren<Camera>();
        //UI Bar
        canvas = transform.Find("Canvas");
        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        LifeNum = transform.Find("Canvas/LifeNum").GetComponent<TMP_Text>();
    }
    private void Update()
    {

        rayCursor = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayCursor, out raycastHitCursor,Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, raycastHitCursor.point - transform.position, Color.red);

            if (Input.GetMouseButtonDown(0)) 
            {
                canMove = true;
            }

            //If face to an obstacle, stop move
            if (Physics.Raycast(transform.position, raycastHitCursor.point - transform.position, out raycastHitForward, 3)) 
            {
                if (raycastHitForward.collider.CompareTag("Obstacle"))
                {
                    print("Hit obstacle" + raycastHitForward.collider.gameObject.name);
                    isBlocked = true;
                    
                }
            }
            else
            {
                isBlocked = false;
            }
            if (canMove && !isBlocked)
            {
                //Move player to cursor
                print("Move");
                if ((raycastHitCursor.point - transform.position).magnitude<=20)
                {
                    transform.position = Vector3.Lerp(transform.position, raycastHitCursor.point, MoveSpeed / 100);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, raycastHitCursor.point, MoveSpeed / 10);
                }


                if (guideFlux.IsPlayerAlive)
                {
                    trail.emitting = true;
                    particleTrace.Play(true);

                    //Reduce Flux on road
                    guideFlux.ReduceFlux(FluxConsume);
                }
                else
                {
                    trail.emitting = false;
                    particleTrace.Stop(true);
                }

            }
            //else if (!raycastHitCursor.transform.CompareTag("Player")&&canMove) //If Hit Game objet other than player, we move player to this GM
            //{
            //    this.transform.position = Vector3.MoveTowards(transform.position,raycastHitCursor.collider.transform.position,MoveSpeed/10);
            //}

        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
        }
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

    #endregion
}
