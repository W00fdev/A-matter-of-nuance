using UnityEngine;
using System;


public enum ResourceWinType { RELIGION, ARMY, FOOD };


public class ResourcesManager : MonoBehaviour
{
    [Header("Settings")]
    public bool CanMultiplyIncreasing;
    public bool CanMultiplyDecreasing;

    [Header("Values")]
    public float Religion;
    public float Army;
    public float Food;

    [Header("Finish conditions")]
    public float MaxResource;
    public float CriticalResource;
    public ResourceWinType WinType;

    [Header("References")]
    public ResourcesUI ResourcesUI;
    public GameManager GameManager;

    public event Action LoseEvent; 
    public event Action WinEvent; 

    void Start()
    {
        ResourcesUI.UpdateReligion(Religion / MaxResource);
        ResourcesUI.UpdateArmy(Army / MaxResource);
        ResourcesUI.UpdateFood(Food / MaxResource);
    }

    public void ChangeReligion(float changed)
    {
        Religion = Mathf.Clamp(Religion + Multiply(changed), 0f, MaxResource);
        ResourcesUI.UpdateReligion(Religion / MaxResource);

        if (Religion <= CriticalResource)
            LoseEvent?.Invoke();

        if (Religion >= MaxResource && WinType == ResourceWinType.RELIGION)
            WinEvent?.Invoke();
    }

    public void ChangeArmy(float changed)
    {
        Army = Mathf.Clamp(Army + Multiply(changed), 0f, MaxResource);
        ResourcesUI.UpdateArmy(Army / MaxResource);

        if (Army <= CriticalResource)
            LoseEvent?.Invoke();

        if (Army >= MaxResource && WinType == ResourceWinType.ARMY)
            WinEvent?.Invoke();
    }

    public void ChangeFood(float changed)
    {
        Food = Mathf.Clamp(Food + Multiply(changed), 0f, MaxResource);
        ResourcesUI.UpdateFood(Food / MaxResource);

        if (Food <= CriticalResource)
            LoseEvent?.Invoke();

        if (Food >= MaxResource && WinType == ResourceWinType.FOOD)
            WinEvent?.Invoke();
    }

    private float Multiply(float value)
    {
        if (CanMultiplyIncreasing && value > 0)
            return GameManager.ResourcesMultiplier * value;
        else
        if (CanMultiplyDecreasing && value < 0)
            return GameManager.ResourcesMultiplier * value;

        return value;
    }
}
