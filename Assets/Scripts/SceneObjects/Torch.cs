using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private Transform fire;
    private void Start()
    {
        fire = transform.GetChild(0);
        fire.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fire.gameObject.SetActive(true);
            GameManager.Instance.ObstacleDoor.TorchResolve();
        }
    }
}
