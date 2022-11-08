using UnityEngine;

public class PlayerDamageHandlerTest : MonoBehaviour
{
    private CharacterAnimator characterAnimator;
    private ParticleSystem particleSystem;
    private Health enemyHealth;
    private DamageHandler damageHandler;

    public bool gotDamaged;

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDamage += HandleDamage;
        characterAnimator = GetComponent<CharacterAnimator>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void HandleDamage(DamageInfo damageInfo)
    {
        if (!enemyHealth.IsDead)
        {
            characterAnimator.animator.SetTrigger("TakeDamage");
        }
    }

    private void PlayDamageParticle()
    {
        particleSystem.Play();
    }
}