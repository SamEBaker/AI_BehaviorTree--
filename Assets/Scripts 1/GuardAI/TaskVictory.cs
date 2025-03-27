using BehaviorTree;
using UnityEngine;

internal class TaskVictory : Node
{
    private Transform transform;

    public TaskVictory(Transform transform)
    {
        this.transform = transform;
    }
}