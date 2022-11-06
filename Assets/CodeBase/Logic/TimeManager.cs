using Infrastructure;
using Logic;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour, IManager
{
    [Header("Время для игрока")]
    public float WinterTime = 5 * 60f;

    [Header("Время тика таймера")]
    public float WinterTickTime = 5f;

    public TimeManagerUI TimeManagerUI;

    private float _currentTime;

    public void EnableManager(bool instant)
    {
        StartCoroutine(FaderBeforeManager());
    }

    public void DisableManager()
    {
    }

    private void StartWrapper()
        => StartCoroutine(TickWinter());

    IEnumerator FaderBeforeManager()
    {
        yield return new WaitForSeconds(Constants.IntroTime);
     
        StartWrapper();
    }

    IEnumerator TickWinter()
    {
        while (_currentTime < WinterTime)
        {
            yield return new WaitForSeconds(WinterTickTime);
            _currentTime += WinterTickTime;
            TimeManagerUI.ChangeFrozen(_currentTime / WinterTime);
        }
    }
}
