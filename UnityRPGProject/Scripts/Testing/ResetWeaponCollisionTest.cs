using UnityEngine;

[RequireComponent(typeof(WeaponCollider))]
public class ResetWeaponCollisionTest : MonoBehaviour
{
    private WeaponCollider weaponCollider;
    public float resetTimer = 2f;
    private float currentTimer = 2f;
    private void Awake()
    {
        weaponCollider = GetComponent<WeaponCollider>();
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            weaponCollider.hasCollided = false;
            currentTimer = resetTimer;
        }
    }
}