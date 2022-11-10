using System.Collections;
using Logic.Actors;
using UnityEngine;
using Logic;
using System;
using Infrastructure;

public class GameManager : MonoBehaviour, IManager
{
    [Header("Win conditions")]
    public bool HasToWinWhenWinterEnds;

    [Header("Tutorial Options")]
    public bool HasToShowTutorialAtStart = false;
    public bool CanInterruptTutorial = false;
    public float TutorialShowDelay = 3f;
    public float TutorialHideDelay = 5f;

    [Header("Vassal multipliers")]
    public float SpeedByVassal = 0.5f;
    public float ResourcesMultuplierByVassal = 0.5f;
    public float ResourcesMultiplier = 1f;

    [Header("Audios")]
    public AudioSource win;
    public AudioSource lose;
    public AudioSource main;

    [Header("References")]
    public ResourcesManager ResourcesManager;
    public VassalSpawner VassalSpawner;
    public TimeManager TimeManager;
    public Unit KingImmortalUnit;
    [Space]
    public GameObject DefeatScreen;
    public GameObject WinScreen;
    public GameObject TutorialScreen;

    private void Start()
    {
        ResourcesManager.WinEvent += OnWin;
        ResourcesManager.LoseEvent += OnLose;

        if (HasToWinWhenWinterEnds)
            TimeManager.onWinterEnds += OnWin;

        VassalSpawner.VassalSpawnedEvent += RiseMultiplier;
        VassalSpawner.VassalDeletedEvent += DownMultiplier;

        KingImmortalUnit.DiedEvent += ClearMultiplier;

        if (!HasToShowTutorialAtStart)
            KingImmortalUnit.DiedEvent += ShowTutorialDelayed;
    }

    private void RiseMultiplier()
    {
        ResourcesMultiplier += ResourcesMultuplierByVassal;
        
        Constants.SpeedRoom += SpeedByVassal;
        Constants.TrapChance -= 0.07f;
        Constants.BetrayChance -= 0.04f;
    }

    private void ClearMultiplier()
    {
        ResourcesMultiplier = 1.0f;

        Constants.SpeedRoom = 3f;
        Constants.TrapChance = 1f;
        Constants.BetrayChance = 1f;
    }

    private void DownMultiplier()
    {
        ResourcesMultiplier -= ResourcesMultuplierByVassal;

        Constants.SpeedRoom -= SpeedByVassal;
        Constants.TrapChance += 0.07f;
        Constants.BetrayChance += 0.04f;
    }

    public void OnLose()
    {
        DefeatScreen.SetActive(true);
        lose.Play();
        main.Stop();
        Constants.IsGameStarted = false;
        DisablePlayer();
    }

    public void OnWin()
    {
        WinScreen.SetActive(true);
        win.Play();
        main.Stop();
        Constants.IsGameStarted = false;
        DisablePlayer();
    }

    private float lastBetrayChance;
    private void DisablePlayer(bool value = false)
    {
        if (!value)
            lastBetrayChance = Constants.BetrayChance;

        Constants.AllowedMovement = value;
        Constants.BetrayChance = value ? lastBetrayChance : 1f;
    }

    private void ShowTutorialDelayed()
    {
        DisablePlayer();
        StartCoroutine(TutorialDelay());

        if (!HasToShowTutorialAtStart)
            KingImmortalUnit.DiedEvent -= ShowTutorialDelayed;
    }

    IEnumerator TutorialDelay()
    {
        yield return new WaitForSeconds(TutorialShowDelay);
        
        TutorialScreen.SetActive(true);
        StartCoroutine(TutorialScreenShowOff());
    }

    IEnumerator TutorialScreenShowOff()
    {
        DisablePlayer();

        float timeSpent = 0;

        while (timeSpent < TutorialHideDelay)
        {
            yield return new WaitForEndOfFrame();
            timeSpent += Time.deltaTime;

            if (CanInterruptTutorial)
                if (InputService.Instance.IsLeftMouseButton())
                    break;
        }

        TutorialScreen.SetActive(false);
        DisablePlayer(true);
    }

    public void EnableManager(bool instant)
    {
        if (!HasToShowTutorialAtStart)
            return;

        if (instant == false)
            StartCoroutine(WaitAndDo(Constants.IntroTime, ShowTutorialDelayed));
        else
            ShowTutorialDelayed();
    }

    public void DisableManager() { }

    IEnumerator WaitAndDo(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
