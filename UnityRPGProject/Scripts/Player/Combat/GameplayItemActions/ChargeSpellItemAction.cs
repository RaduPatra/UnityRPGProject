using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using ExtensionMethods;


[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/ChargeItemAction", order = 1)]
public class ChargeSpellItemAction : ItemGameplayActions
{
    [SerializeField] private AttributeBaseSO projectileWarmupFXAttribute;
    [SerializeField] private AttributeBaseSO projectileChargeFXAttribute;
    [SerializeField] private AttributeBaseSO projectileShootAttribute;
    [SerializeField] private float projectileForce = 10f;
    [SerializeField] private float shootMaxDistance = 999f;
    [SerializeField] private AttributeBaseSO staminaConsumptionAttribute;

    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<CharacterAnimator>();
        var playerEquipment = go.GetComponent<EquipmentManager>();
        var playerAttack = go.GetComponent<PlayerCombat>();
        var playerStamina = go.GetComponent<Stamina>();

        playerAttack.CanCastSpell = false;
        if (playerAnimator.IsInteracting) return;
        if (playerStamina.CurrentStamina <= 0) return;
        playerAttack.CanCastSpell = true;
        playerAnimator.PlayAnimation(CharacterAnimator.leftHandCharge, false);
        playerAnimator.IsAiming = true;

        var warmupFXPrefab = item.GetAttribute<GameObjectData>(projectileWarmupFXAttribute)?.value;
        if (!warmupFXPrefab) return;

        var spawnPosition = GetSpawnPosition(item, playerEquipment);
        Instantiate(warmupFXPrefab, spawnPosition);
    }

    public override void PerformedAction(ItemWithAttributes item, GameObject go)
    {
        var playerAttacker = go.GetComponent<PlayerCombat>();
        var playerEquipment = go.GetComponent<EquipmentManager>();

        if (!playerAttacker.CanCastSpell) return;

        playerAttacker.ChargePerformed = true;
        var spawnPosition = GetSpawnPosition(item, playerEquipment);
        if (spawnPosition.childCount != 0) Destroy(spawnPosition.GetChild(0).gameObject, 1f);

        var chargedFXPrefab = item.GetAttribute<GameObjectData>(projectileChargeFXAttribute)?.value;
        if (!chargedFXPrefab) return;
        Instantiate(chargedFXPrefab, spawnPosition);
    }

    //drain stamina update if charge performed?
    public override void CancelledAction(ItemWithAttributes item, GameObject go)
    {
        var playerAttacker = go.GetComponent<PlayerCombat>();
        var playerAnimator = go.GetComponent<CharacterAnimator>();
        var playerEquipment = go.GetComponent<EquipmentManager>();

        if (!playerAttacker.CanCastSpell) return;
        var spawnPosition = GetSpawnPosition(item, playerEquipment);

        if (playerAttacker.ChargePerformed)
        {
            playerAnimator.PlayAnimation(CharacterAnimator.offHandShoot, true);
        }
        else
        {
            spawnPosition.Clear();
        }

        playerAttacker.ChargePerformed = false;
        playerAnimator.IsAiming = false;
    }

    public override void FinalizeAction(ItemWithAttributes item, GameObject go)
    {
        var playerEquipment = go.GetComponent<EquipmentManager>();
        var rayProvider = go.GetComponent<IRayProvider>();
        var playerStamina = go.GetComponent<Stamina>();

        var weaponLocation = GetWeaponLocation(item, playerEquipment);
        var chargedFXPrefab = item.GetAttribute<GameObjectData>(projectileShootAttribute)?.value;
        var projFactory = new ProjectileFactory(go, chargedFXPrefab);
        var projectile = projFactory.CreateProjectileWithItem(item, weaponLocation);

        var ray = rayProvider.CreateRay();
        var shooter = new ProjectileShooter(go);
        shooter.ShootProjectile(ray, shootMaxDistance, projectileForce, projectile);
        playerStamina.DrainItemStamina(item, staminaConsumptionAttribute);
    }

    private static Transform GetSpawnPosition(ItemWithAttributes item, EquipmentManager playerEquipment)
    {
        var equipLocation = playerEquipment.GetEquippedItemLocation(item);
        var spawnPosition = equipLocation.GetComponentInChildren<ProjectileSpawnPosition>().transform;
        return spawnPosition;
    }

    private static Transform GetWeaponLocation(ItemWithAttributes item, EquipmentManager playerEquipment)
    {
        return playerEquipment.GetEquippedItemLocation(item);
    }
}