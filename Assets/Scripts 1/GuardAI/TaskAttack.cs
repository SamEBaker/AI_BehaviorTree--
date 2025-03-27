using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Unity.VisualScripting;

public class TaskAttack : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private EnemyManager _enemyManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }
        if (_enemyManager._healthpoints > 40)
        {
            state = NodeState.FAILURE;
            return state;
        }
        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.TakeHit();
            if (enemyIsDead)
            {
                ClearData("target");
                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }

}
