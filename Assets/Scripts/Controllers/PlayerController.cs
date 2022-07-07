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

    /// <summary>
    /// Prefab to coloring zone
    /// </summary>
    public GameObject ColorZonePrefab;
    /// <summary>
    /// Distance between two color zones
    /// </summary>
    public float ColorZoneRate;
    

    //private parameters

    private Ray rayCursor;
    private RaycastHit raycastHitCursor;
    private Ray rayForward;
    private RaycastHit raycastHitForward;

    private Transform colorZoneManager;

    private new Rigidbody rigidbody;
    private Camera gameCamera;

    private bool canMove;
    private bool isBlocked;

    private float colorZoneCount = 0;
    private Vector3 lastColorZonePos = Vector3.zero;

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

        rigidbody = GetComponent<Rigidbody>();
        guideFlux = transform.GetComponent<GuideFluxBehaviour>();
        gameCamera = transform.Find("Main Camera").GetComponent<Camera>();
        colorZoneManager = GameObject.Find("ColorZoneManager").transform;

        //UI Bar
        canvas = transform.Find("Canvas");
        lifeBar = transform.Find("Canvas/LifeBar").GetComponent<Image>();
        LifeNum = transform.Find("Canvas/LifeNum").GetComponent<TMP_Text>();

    }

    private void Update()
    {

        rayCursor = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayCursor, out raycastHitCursor,float.MaxValue, 3<<LayerMask.NameToLayer("Ground")))
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
                    isBlocked = true;
                }
            }
            else
            {
                isBlocked = false;
            }
            if (canMove && !isBlocked)
            {
                float _distance = Vector3.Distance(transform.position, raycastHitCursor.point);
                //float _haut = (raycastHitForward.point.z - transform.position.z);
                //_distance -= _haut;

                //Limit distance range
                switch (_distance)
                {
                    case float n when (n <= 0):
                        _distance = 0;
                        break;
                    case float n when (n >= 15):
                        _distance = 15;
                        break;
                    default:
                        break;
                }
                //Move player to cursor
                rigidbody.velocity = (raycastHitCursor.point - transform.position).normalized*_distance;


                if (guideFlux.IsPlayerAlive)
                {

                    if (Vector3.Distance(lastColorZonePos, transform.position) >= ColorZoneRate)
                    {
                        SpawnColorZone();
                        lastColorZonePos = SpawnColorZone();
                    }
                    //Reduce Flux on road
                    guideFlux.ReduceFlux(FluxConsume);
                }

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
        }
    }

    private Vector3 SpawnColorZone()
    {
        GameObject _gm = GameObject.Instantiate(ColorZonePrefab, colorZoneManager, true);
        _gm.GetComponent<ColorZoneBehaviour>().ActiveColorZone(guideFlux.CurrentFlux/guideFlux.MaxFlux);
        _gm.transform.position = this.transform.position+Vector3.down;
        return _gm.transform.position;
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


