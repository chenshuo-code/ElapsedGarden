using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestColorZone : MonoBehaviour
{
    //private bool isActive=false;

    private void Awake()
    {
        transform.DOScale(new Vector3(5, 0.1f, 5), 2f).OnComplete(ScaleMore);
        //isActive = true;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isActive)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            transform.DOScale(new Vector3(5, 0.1f, 5), 2f).OnComplete(ScaleMore);
    //            isActive = true;
    //        }

    //    }

    //}
    private void ScaleMore()
    {
        transform.DOScale(new Vector3(10, 0.1f, 10), 5f);
    }
}
