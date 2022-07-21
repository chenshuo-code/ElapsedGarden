using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorZoneManager : MonoBehaviour
{
    private bool isActive = false;
    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isActive)
            {
                print("玩家进入" + this.gameObject.name);
                GameManager.Instance.PlayerController.SignColorZoneManager(this.transform);
            }
            else
            {
                GameManager.Instance.PlayerController.CanSpawnColorZone = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("玩家离开" + this.gameObject.name);
            GameManager.Instance.PlayerController.DeSignColorZoneManager();
            GameManager.Instance.PlayerController.CanSpawnColorZone = true;
        }
    }

    public void ActiveColorZone()
    {
        isActive = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(0).gameObject);
        }
    }

}
