using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check point, for functions launche a round
/// </summary>
public class CheckPoint : MonoBehaviour
{
    /// <summary>
    /// Mesh after activate
    /// </summary>
    public Mesh TreeMesh;
    /// <summary>
    /// Max flux to be Increased on check point activate 
    /// </summary>
    public float RewardFlux;
    /// <summary>
    /// defaut active
    /// </summary>
    public bool IsActive;

    private GuideFluxBehaviour guideFlux;
    private PlayerController playerController;

    private MeshFilter meshFilter;

    private void Start()
    {
        IsActive = false;

        guideFlux = GameManager.Instance.GuideFlux;
        playerController = GameManager.Instance.PlayerController;

        meshFilter = transform.GetComponent<MeshFilter>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!IsActive)
            {
                ActiveCheckPoint();
            }
            else
            {
                ReloadCheckPoint();
            }
        }
    }

    /// <summary>
    /// Reload from a Active CheckPoint
    /// </summary>
    private void ReloadCheckPoint()
    {
        GameManager.Instance.CheckGame();
    }
    public void ActiveCheckPoint()
    {
        IsActive = true;
        meshFilter.mesh = TreeMesh;
        guideFlux.IncreaseMaxFlux(RewardFlux); //Add max flux// To be test
        GameManager.Instance.AddCheckPointActive(this);
        GameManager.Instance.ActivateCheckPoint();
    }
    
}
