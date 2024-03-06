using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public ObjectiveType type;
    public ScriptableObject targetDetail;
    public int requiredAmount = 1;
    public int currentAmount = 0;
    public bool isCompleted = false;

    public Objective(Objective objective)
    {
        type = objective.type;
        targetDetail = objective.targetDetail;
        requiredAmount = objective.requiredAmount;
        currentAmount = objective.currentAmount;
        isCompleted = objective.isCompleted;
    }
}

public enum ObjectiveType
{
    Hunt,
    Gather,
    Deliver,
    Go
}
