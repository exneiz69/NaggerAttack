public interface IBeing
{
    public float Health { get; }

    public float MaxHealth { get; }

    event UnityEngine.Events.UnityAction<IBeing> HealthChanged;

    event UnityEngine.Events.UnityAction<IBeing> Died;

    void TakeDamage(float damage);
}
