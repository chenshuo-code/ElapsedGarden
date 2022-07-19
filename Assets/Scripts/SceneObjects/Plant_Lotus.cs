using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Lotus : MonoBehaviour
{
    /// <summary>
    /// Time delay to active plate of lotus, in secound
    /// </summary>
    public float PlateShowTime = 6;

    private float shapeWeight;
    private float lerpSpeed = 0;

    private TriggerLotus triggerLotus;
    private SkinnedMeshRenderer lotusMesh;
    private bool canFishActiveLotus=false;
    private bool isLotusShow = false;
    private bool canActivePlate = false;
    private bool canPlayAnim = false;


    private void Start()
    {
        triggerLotus =  transform.GetComponentInChildren<TriggerLotus>();
        lotusMesh = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        lotusMesh.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (canPlayAnim)
        {
            lerpSpeed += Time.deltaTime;
            if (canActivePlate)
            {
                shapeWeight = Mathf.Lerp(0, 100, lerpSpeed);
                lotusMesh.SetBlendShapeWeight(0, shapeWeight);
                if (shapeWeight >= 100)
                {
                    canPlayAnim = false;
                    lerpSpeed = 0;
                }
            }
            else
            {
                shapeWeight = Mathf.Lerp(100, 0, lerpSpeed);
                lotusMesh.SetBlendShapeWeight(0, shapeWeight);
                if (shapeWeight <= 0)
                {
                    HideLotus();
                    canPlayAnim = false;
                    lerpSpeed = 0;
                }
            }
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish")&&canFishActiveLotus)
        {
            ShowLotus();
        }
        if (other.CompareTag("Player")&& isLotusShow) //Can activate plate only when lotus is activated and showed
        {
            ActivePlate();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish")&&canFishActiveLotus)
        {
            HideLotus();
        }
    }
    private void ShowLotus()
    {
        print("然后在鱼经过时显现");
        //triggerLotus.gameObject.SetActive(true);
        lotusMesh.gameObject.SetActive(true);
        isLotusShow = true;
    }
    private void HideLotus()
    {
        print("鱼走了就隐藏");
        //triggerLotus.gameObject.SetActive(false);
        lotusMesh.gameObject.SetActive(false);
        isLotusShow = false;
    }
    private void ActivePlate()
    {
        print("玩家进来激活盘子");
        canFishActiveLotus = false;
        canActivePlate = true;
        canPlayAnim = true;
        Invoke("DeactivatePlate", PlateShowTime);
    }
    private void DeactivatePlate()
    {
        print("玩家不动盘子10秒消失");
        canActivePlate = false;
        canFishActiveLotus = true;
        canPlayAnim = true;
    }
    public void ActiveLotus()
    {
        print("首先优雅的激活lotus");
        canFishActiveLotus = true;
    }
}
