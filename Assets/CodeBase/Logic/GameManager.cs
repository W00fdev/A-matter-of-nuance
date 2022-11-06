using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public ResourcesManager ResourcesManager;

    public GameObject DefeatScreen;
    public GameObject WinScreen;

    public float ResourcesMultiplier;

    private void Start()
    {
        ResourcesManager.WinEvent += OnWin;
        ResourcesManager.LoseEvent += OnLose;
    }

    private void OnLose()
    {
        DefeatScreen.SetActive(true);
    }

    private void OnWin()
    {
        WinScreen.SetActive(true);
    }
}
