using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorZoneBehaviour : MonoBehaviour
{
    private float scaleSize;
    public void ActiveColorZone(float scaleSize)
    {
        this.scaleSize = scaleSize;
        transform.DOScale(new Vector3(7* scaleSize, 0.1f, 7* scaleSize), 2f).OnComplete(ScaleMore);
    }
    private void ScaleMore()
    {
        transform.DOScale(new Vector3(10 * scaleSize, 0.1f, 10 * scaleSize), 5f);
    }
}
