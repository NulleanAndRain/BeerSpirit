using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExitWindow : MonoBehaviour
{
    public GameObject exitWindow;
    public float winStartPos;
    public float speed=1f;
    // Start is called before the first frame update
    void Start()
    {
        exitWindow.transform.DOMoveY(winStartPos - 1500f, 0).SetEase(Ease.InOutBack);
        winStartPos = exitWindow.transform.position.y;
        
    }
    public void ShowWindow()
    {
        exitWindow.transform.DOMoveY(winStartPos, speed).SetEase(Ease.InOutBack);
    }
    public void HideWindow()
    {
        exitWindow.transform.DOMoveY(winStartPos-1500f, speed).SetEase(Ease.InOutBack);
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
