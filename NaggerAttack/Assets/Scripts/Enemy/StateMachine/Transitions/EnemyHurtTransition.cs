using UnityEngine;

public class EnemyHurtTransition : EnemyTransition
{
    protected override void OnEnter()
    {
        Enemy.HealthChanged += OnEnemyHealthChanged;
    }

    protected override void OnExit()
    {
        Enemy.HealthChanged -= OnEnemyHealthChanged;
    }

    private void OnEnemyHealthChanged(IBeing being)
    {
        if (being.Health < being.MaxHealth)
        {
            Finish();
        }
    }
}
