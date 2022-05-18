using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public PlayerController PlayerController;
    [HideInInspector] public FirstTreeBehaviour FirstTreeBehaviour;
    private void Awake()
    {
        Instance = this;

        PlayerController = FindObjectOfType<PlayerController>();
        PlayerController.Init();
        FirstTreeBehaviour = FindObjectOfType<FirstTreeBehaviour>();
        FirstTreeBehaviour.Init();
    }
}
