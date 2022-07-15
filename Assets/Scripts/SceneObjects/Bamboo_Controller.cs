using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo_Controller : MonoBehaviour
{
    public List<Obstacle_Bamboo> ObstacleBamboosList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Obstacle_Bamboo bamboo in ObstacleBamboosList)
            {
                if (bamboo.IsActive) bamboo.DeactivateObstacle();
                else bamboo.ActivateObstacle();
            }
        }
    }
}
