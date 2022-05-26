using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static float RespawnTime => _instance._respawnTime;
    public static bool IsPaused { get; private set; }

    [SerializeField]
    private float _respawnTime;

    private void Awake()
    {
        _instance = this;
    }
}
