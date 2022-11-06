using UnityEngine;
using System;


public enum ResourceWinType { RELIGION, ARMY, FOOD };


public class ResourcesManager : MonoBehaviour
{
    public float Religion;
    public float Army;
    public float Food;

    public float MaxResource;
    public float CriticalResource;

    public ResourceWinType WinType;
    public ResourcesUI ResourcesUI;

    public GameManager GameManager;

    public event Action LoseEvent; 
    public event Action WinEvent; 

    void Start()
    {
        ResourcesUI.UpdateReligion(Religion);
        ResourcesUI.UpdateArmy(Army);
        ResourcesUI.UpdateFood(Food);
    }

    public void ChangeReligion(float changed)
    {
        Religion = Mathf.Clamp(Religion + GameManager.ResourcesMultiplier * changed, 0f, MaxResource);
        ResourcesUI.UpdateReligion(Religion);

        if (Religion <= CriticalResource)
            LoseEvent?.Invoke();

        if (Religion >= MaxResource && WinType == ResourceWinType.RELIGION)
            WinEvent?.Invoke();
    }

    public void ChangeArmy(float changed)
    {
        Army = Mathf.Clamp(Army + GameManager.ResourcesMultiplier * changed, 0f, MaxResource);
        ResourcesUI.UpdateArmy(Army);

        if (Army <= CriticalResource)
            LoseEvent?.Invoke();

        if (Army >= MaxResource && WinType == ResourceWinType.ARMY)
            WinEvent?.Invoke();
    }

    public void ChangeFood(float changed)
    {
        Food = Mathf.Clamp(Food + GameManager.ResourcesMultiplier * changed, 0f, MaxResource);
        ResourcesUI.UpdateFood(Food);

        if (Food <= CriticalResource)
            LoseEvent?.Invoke();

        if (Food >= MaxResource && WinType == ResourceWinType.FOOD)
            WinEvent?.Invoke();
    }
}
