using Infrastructure;
using Logic;
using System.Collections;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour, IManager
{
    public TimeManagerUI TimeManagerUI;

    [Header("Время для игрока")]
    // 0.4f * 60f removed
    public float WinterTime;

    [Header("Время тика таймера")]
    public float WinterTickTime = 5f;

    private float _currentTime = 0f;

    public float winterFirstThreshold;
    public float winterSecondThreshold;

    public float initialSpeed = Constants.SpeedRoom;
    public float maxSpeed;

    public AudioSource firstSource;
    public AudioSource secondSource;

    public ParticleSystem WinterParticleSystem;

    public event Action onWinterEnds;

    private ParticleSystem.MainModule _winterParticleModule;
    private Coroutine _winterCoroutine;

    private const float FirstWinterParticlesSpeed = 5f;
    private const float SecondWinterParticlesSpeed = 7f;
    private const float ThirdWinterParticlesSpeed = 9f;

    public void EnableManager(bool instant)
    {
        if (instant == false)
            StartCoroutine(FaderBeforeManager());
        else
            StartWrapper();
    }

    public void DisableManager()
    {
        if (_winterCoroutine != null)
        {
            StopCoroutine(_winterCoroutine);
            _winterCoroutine = null;
        }    

        firstSource.Stop();
        secondSource.Stop();

        _winterParticleModule.startSpeed = FirstWinterParticlesSpeed;
    }

    private void StartWrapper() 
        => _winterCoroutine = StartCoroutine(TickWinter());

    IEnumerator FaderBeforeManager()
    {
        yield return new WaitForSeconds(Constants.IntroTime);

        _winterParticleModule = WinterParticleSystem.main;
        _winterParticleModule.startSpeed = FirstWinterParticlesSpeed;

        StartWrapper();
    }

    IEnumerator TickWinter()
    {
        TimeManagerUI.ChangeFrozen(0f);
        
        Constants.IsGameStarted = true;
        Constants.AllowedMovement = true;

        // Return original values from restart
        Constants.SpeedRoom = 3f;
        Constants.TrapChance = 0.4f;
        Constants.BetrayChance = 1f;
        // Return original values from restart

        while (_currentTime < WinterTime)
        {
            float winterTickTimeElapsed = 0f;
            while (winterTickTimeElapsed < WinterTickTime)
            {
                // Prevent stop-game win
                yield return null;

                if (Constants.AllowedMovement == true)
                    winterTickTimeElapsed += Time.deltaTime;
            }    
            _currentTime += WinterTickTime;
            TimeManagerUI.ChangeFrozen(_currentTime / WinterTime);

            Constants.SpeedRoom = Mathf.Clamp(initialSpeed + (maxSpeed - initialSpeed) * (_currentTime / WinterTime), initialSpeed, maxSpeed);

            if (_currentTime >= winterSecondThreshold)
            {
                if (firstSource.isPlaying)
                    firstSource.Stop();

                if (!secondSource.isPlaying)
                    secondSource.Play();

                // Speed up winterParticles
                _winterParticleModule.startSpeed = ThirdWinterParticlesSpeed;
            }
            else
            if (_currentTime >= winterFirstThreshold)
            {
                if (!firstSource.isPlaying)
                    firstSource.Play();

                _winterParticleModule.startSpeed = SecondWinterParticlesSpeed;
            }
        }

        onWinterEnds.Invoke();
        _winterCoroutine = null;

        DisableManager();
    }
}
