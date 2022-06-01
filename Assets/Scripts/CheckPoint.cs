using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Mesh TreeMesh;

    private PlayerController playerController;
    private FirstTreeBehaviour firstTree;

    private MeshFilter meshFilter;

    private void Start()
    {
        playerController = GameManager.Instance.PlayerController;
        firstTree = GameManager.Instance.FirstTreeBehaviour;

        meshFilter = transform.GetComponent<MeshFilter>();

    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            if (playerController.PlayerState == PlayerState.OnSpend)
            {
                ActiveCheckPoint();
            }
        }
    }
    private void ActiveCheckPoint()
    {
        meshFilter.mesh = TreeMesh;
        ResetTrace();
    }
    private void ResetTrace()
    {
        playerController.EffaceLine();
        for (int i = 0; i < GameManager.Instance.Plants.Length; i++)
        {
            GameManager.Instance.Plants[i].DeactivateStartFrom();
        }

    }
}
