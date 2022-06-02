using UnityEngine;

public class EnemyDeathTransition : EnemyTransition
{
    protected override void OnEnter()
    {
        Enemy.Died += OnEnemyDied;
    }

    protected override void OnExit()
    {
        Enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(IBeing being)
    {
        Finish();
    }
}
