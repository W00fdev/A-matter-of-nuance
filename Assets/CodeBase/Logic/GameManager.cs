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
        Constants.TrapChance += 0.07f;
    }

    private void ClearMultiplier()
    {
        ResourcesMultiplier = 1.0f;

        Constants.SpeedRoom = 3f;
        Constants.TrapChance = 0f;
    }

    private void OnLose()
    {
        DefeatScreen.SetActive(true);
    }

    private void OnWin()
    {
        WinScreen.SetActive(true);
    }

    private void ShowTutorialDelayed()
    {
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
        yield return new WaitForSeconds(TutorialHideDelay);
        TutorialScreen.SetActive(false);
    }

}
