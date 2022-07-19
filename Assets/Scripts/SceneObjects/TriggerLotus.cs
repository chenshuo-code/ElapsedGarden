using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLotus : MonoBehaviour
{
    private Plant_Lotus lotus;
    private void Start()
    {
        lotus=transform.GetComponentInParent<Plant_Lotus>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lotus.ActiveLotus();
        }
    }
}
