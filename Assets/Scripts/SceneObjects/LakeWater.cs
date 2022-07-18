using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeWater : MonoBehaviour
{
    public Transform StartPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController _player = other.GetComponent<PlayerController>();
            _player.TeleportToPosition(StartPoint.position);
        }
    }
}
