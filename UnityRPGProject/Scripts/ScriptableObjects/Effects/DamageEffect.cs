using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Effects/Damage Effect", order = 1)]
public class DamageEffect : EffectBase
{
    public float attackDamageModifier = 100f;
    public float magicDamageModifier = 100f;

    private CharacterStats stats;

    //public override void UseEffect(GameObject target, Action<EffectBase> onFinished)
    public override void UseEffect(GameObject target)
    {
        //make UseEffect virtual and move this to base
        base.UseEffect(target);
        stats = target.GetComponent<CharacterStats>();
        stats.ActiveModifiers[StatType.AttackDamage] += attackDamageModifier;
        stats.ActiveModifiers[StatType.MagicDamage] += magicDamageModifier;
        timeRemaining = effectExpireTime;
    }

    public override void UpdateEffect(float deltaTime)
    {
        timeRemaining -= deltaTime;
        if (IsDone())
        {
            stats.ActiveModifiers[StatType.AttackDamage] -= attackDamageModifier;
            stats.ActiveModifiers[StatType.MagicDamage] -=  magicDamageModifier;
        }
    }
}