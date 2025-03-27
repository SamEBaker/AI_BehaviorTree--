using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskHide : Node
{
    private Animator _animator;
    private Transform _transform;
    private Transform _hidePt;
    private float _waitTime = 3f; // Time to stay hidden
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskHide(Transform transform, Transform hidePt)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _hidePt = hidePt;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        EnemyManager _enemyManager = target.GetComponent<EnemyManager>();
        if (_enemyManager._healthpoints < 40)
        {
            state = NodeState.FAILURE;
            return state;
        }
        // Move towards the hiding spot
        if (Vector3.Distance(_transform.position, _hidePt.position) > 0.1f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _hidePt.position, GuardBT.speed * 2 * Time.deltaTime);
            _transform.LookAt(_hidePt.position);
            _animator.SetBool("Walking", true);

            state = NodeState.RUNNING;
            return state;
        }

        // AI reached the hiding spot
        _animator.SetBool("Walking", false);

        if (!_waiting)
        {
            _waiting = true;
            _waitCounter = Time.time; // Start wait timer
        }

        // Wait at hide spot for 3 seconds
        if (Time.time - _waitCounter >= _waitTime)
        {
            Debug.Log("Finished hiding. Returning to patrol.");
            _waiting = false; // Reset waiting state
            ClearData("target"); // Clear the enemy target to reset behavior
            state = NodeState.FAILURE; // Let the tree transition to patrol
            return state;
        }

        state = NodeState.RUNNING; // Continue hiding
        return state;
    }
}
