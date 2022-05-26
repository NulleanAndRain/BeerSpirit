using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowStats : MonoBehaviour
    
{   
    public GameObject stats;
    public float winStartPos;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
        
    {   winStartPos = stats.transform.position.y;
        stats.transform.DOMoveY(winStartPos + 500f, 0).SetEase(Ease.InOutBack);
        stats.transform.DOMoveY(winStartPos, speed).SetEase(Ease.InOutBack);
    }
    public void ShowStatsWin()
    {
        stats.transform.DOMoveY(winStartPos, speed).SetEase(Ease.InOutBack);
    }
    public void HideStatsWin()
    {
       stats.transform.DOMoveY(winStartPos + 500f, speed).SetEase(Ease.InOutBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
