using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check point, for functions launche a round
/// </summary>
public class CheckPoint : MonoBehaviour
{
    public Mesh TreeMesh;
    public float RewardFlux;

    private bool isActive;

    private GuideFlux guideFlux;
    private FirstTreeBehaviour firstTree;

    private MeshFilter meshFilter;

    private void Start()
    {
        isActive = false;

        guideFlux = GameManager.Instance.GuideFlux;
        firstTree = GameManager.Instance.FirstTreeBehaviour;

        meshFilter = transform.GetComponent<MeshFilter>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GuideFlux"))
        {
            ActiveCheckPoint();
        }
    }

    private void OnMouseDown()
    {
        if (isActive)
        {
            guideFlux.TeleportToMove(this.transform.position);
        }
    }
    private void ActiveCheckPoint()
    {
        isActive = true;
        meshFilter.mesh = TreeMesh;
        firstTree.IncreaseMaxFlux(RewardFlux); // To be test
        GameManager.Instance.CheckGame();
    }
}
