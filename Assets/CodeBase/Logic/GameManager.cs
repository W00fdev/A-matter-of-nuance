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

    private float _prevBetrayChance = 1f;

    public AudioSource win;
    public AudioSource lose;

    private void Start()
    {
        ResourcesManager.WinEvent += OnWin;
        ResourcesManager.LoseEvent += OnLose;

        VassalSpawner.VassalSpawnedEvent += RiseMultiplier;
        //VassalSpawner.VassalDeletedEvent += ClearMultiplier;

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

    public void OnLose()
    {
        DefeatScreen.SetActive(true);
        lose.Play();

        Constants.AllowedMovement = false;
        Constants.BetrayChance = 1f;
    }

    public void OnWin()
    {
        WinScreen.SetActive(true);
        win.Play();

        Constants.AllowedMovement = false;
        Constants.BetrayChance = 1f;
    }

    private void ShowTutorialDelayed()
    {
        StartCoroutine(TutorialDelay());

        KingImmortalUnit.DiedEvent -= ShowTutorialDelayed;
    }

    IEnumerator TutorialDelay()
    {
        yield return new WaitForSeconds(TutorialShowDelay);

        Constants.AllowedMovement = false;
        _prevBetrayChance = Constants.BetrayChance;
        Constants.BetrayChance = 1f;

        TutorialScreen.SetActive(true);
        StartCoroutine(TutorialScreenShowOff());
    }

    IEnumerator TutorialScreenShowOff()
    {
        yield return new WaitForSeconds(TutorialHideDelay);
        TutorialScreen.SetActive(false);

        Constants.BetrayChance = _prevBetrayChance;
    }

}
