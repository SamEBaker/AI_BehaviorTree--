using BehaviorTree;
using System.Collections.Generic;

public class GuardBT : BTree
{
    public UnityEngine.Transform[] waypoints;
    public UnityEngine.Transform hidePt;
    public static float speed = 20f;
    public static float fovRange = 30f;
    public static float attackRange = 20f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {

            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskHide(transform, hidePt),

            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskVictory(transform),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
