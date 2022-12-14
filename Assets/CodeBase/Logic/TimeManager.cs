using Infrastructure;
using Logic;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour, IManager
{
    public TimeManagerUI TimeManagerUI;

    [Header("Время для игрока")]
    public float WinterTime = 5 * 60f;

    [Header("Время тика таймера")]
    public float WinterTickTime = 5f;

    private float _currentTime = 0f;

    public float winterFirstThreshold;
    public float winterSecondThreshold;

    public AudioSource firstSource;
    public AudioSource secondSource;

    public void EnableManager(bool instant)
    {
        if (instant == false)
            StartCoroutine(FaderBeforeManager());
        else
            StartWrapper();
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
        TimeManagerUI.ChangeFrozen(0f);

        while (_currentTime < WinterTime)
        {
            yield return new WaitForSeconds(WinterTickTime);
            _currentTime += WinterTickTime;
            TimeManagerUI.ChangeFrozen(_currentTime / WinterTime);

            if (_currentTime <= winterFirstThreshold)
            {
                if (!firstSource.isPlaying)
                    firstSource.Play();
            }
            else
            if (_currentTime <= winterSecondThreshold)
            {
                if (firstSource.isPlaying)
                    firstSource.Stop();

                if (!secondSource.isPlaying)
                    secondSource.Play();
            }
        }
    }
}
