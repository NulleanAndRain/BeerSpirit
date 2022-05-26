using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StopMenu : MonoBehaviour
{
    public GameObject stopWindow;
    public float winStartPos;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        stopWindow.transform.DOMoveY(winStartPos - 1500f, 0).SetEase(Ease.InOutBack);
        winStartPos = stopWindow.transform.position.y;
    }
    public void ShowWindow()
    {
        stopWindow.transform.DOMoveY(winStartPos, speed).SetEase(Ease.InOutBack);
    }
    public void HideWindow()
    {
        stopWindow.transform.DOMoveY(winStartPos - 1500f, speed).SetEase(Ease.InOutBack);
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
