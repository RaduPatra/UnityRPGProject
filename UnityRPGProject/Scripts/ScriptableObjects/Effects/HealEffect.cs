using UnityEngine;


[CreateAssetMenu(fileName = "New Effect", menuName = "Effects/Heal Effect", order = 1)]

public class HealEffect : EffectBase
{
    public float healAmount = 10f;
    private Health playerHealth;

    //public override void UseEffect(GameObject target, Action<EffectBase> onFinished)
    public override void UseEffect(GameObject target)
    {
        base.UseEffect(target);

        playerHealth = target.GetComponent<Health>();
        playerHealth.Heal(healAmount);
        timeRemaining = effectExpireTime;
    }

    public override void UpdateEffect(float deltaTime)
    {
        // timeRemaining -= deltaTime;
        // if (IsDone())
        // {
        //     player.JumpHeight = player.BaseJumpHeight;
        // }
    }
}