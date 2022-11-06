using UnityEngine;
using System;
using Logic.Actors;
using Logic;

public class GameManager : MonoBehaviour
{
    public ResourcesManager ResourcesManager;
    public VassalSpawner VassalSpawner;

    public GameObject DefeatScreen;
    public GameObject WinScreen;

    public float SpeedByVassal = 0.5f;
    public float ResourcesMultuplierByVassal = 0.5f;
    public float ResourcesMultiplier = 1f;

    private void Start()
    {
        ResourcesManager.WinEvent += OnWin;
        ResourcesManager.LoseEvent += OnLose;

        VassalSpawner.VassalSpawnedEvent += RiseMultiplier;
        //VassalSpawner.VassalDeletedEvent += ClearMultiplier;
    }

    private void RiseMultiplier()
    {
        ResourcesMultiplier += ResourcesMultuplierByVassal;
        Constants.SpeedRoom += SpeedByVassal;
    }

    private void ClearMultiplier()
    {
        ResourcesMultiplier = 1.0f;
        Constants.SpeedRoom = 3f;
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
