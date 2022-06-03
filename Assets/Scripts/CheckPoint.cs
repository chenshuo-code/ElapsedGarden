using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Mesh TreeMesh;

    private bool isActive;

    private PlayerController playerController;
    private FirstTreeBehaviour firstTree;

    private MeshFilter meshFilter;

    private void Start()
    {
        isActive = false;

        playerController = GameManager.Instance.PlayerController;
        firstTree = GameManager.Instance.FirstTreeBehaviour;

        meshFilter = transform.GetComponent<MeshFilter>();

    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            if (playerController.PlayerState == PlayerState.OnSpend && !isActive)
            {
                ActiveCheckPoint();
            }
        }
    }
    private void OnMouseDown()
    {
        if (isActive)
        {
            SpendingTime();
        }
    }
    private void ActiveCheckPoint()
    {
        isActive = true;
        meshFilter.mesh = TreeMesh;
        firstTree.ReceiveTime(20);
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
    private void SpendingTime()
    {
        playerController.PlayerState = PlayerState.OnSpend;
        playerController.ActivateDrawLine(true);
    }
}
