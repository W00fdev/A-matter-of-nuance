using System.Collections;
using Logic.Actors;
using UnityEngine;
using Logic;

public class GameManager : MonoBehaviour
{
    public ResourcesManager ResourcesManager;
    public VassalSpawner VassalSpawner;

    public Unit KingImmortalUnit;

    public GameObject DefeatScreen;
    public GameObject WinScreen;
    public GameObject TutorialScreen;

    public float TutorialShowDelay = 3f;
    public float TutorialHideDelay = 5f;

    public float SpeedByVassal = 0.5f;
    public float ResourcesMultuplierByVassal = 0.5f;
    public float ResourcesMultiplier = 1f;

    public AudioSource win;
    public AudioSource lose;
    public AudioSource main;

    private void Start()
    {
        ResourcesManager.WinEvent += OnWin;
        ResourcesManager.LoseEvent += OnLose;

        VassalSpawner.VassalSpawnedEvent += RiseMultiplier;
        VassalSpawner.VassalDeletedEvent += DownMultiplier;

        KingImmortalUnit.DiedEvent += ClearMultiplier;
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

        yield return new WaitForSeconds(TutorialHideDelay);
        TutorialScreen.SetActive(false);
        DisablePlayer(true);
    }

}
