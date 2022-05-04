using System.Runtime.InteropServices.ComTypes;
using UnityEngine;


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
        var playerAnimator = go.GetComponent<PlayerAnimator>();
        var playerManager = go.GetComponent<PlayerManager>();
        var playerEquipment = go.GetComponent<EquipmentManager>();
        var playerAttack = go.GetComponent<PlayerCombat>();
        var playerStamina = go.GetComponent<Stamina>();
        playerAttack.CanCastSpell = false;
        if (playerManager.IsInteracting) return;
        if (playerStamina.CurrentStamina <= 0) return;
        playerAttack.CanCastSpell = true;
        playerAnimator.PlayAnimation(PlayerAnimator.leftHandCharge, false);
        playerManager.IsAiming = true;

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
        var playerAnimator = go.GetComponent<PlayerAnimator>();
        var playerManager = go.GetComponent<PlayerManager>();
        var playerEquipment = go.GetComponent<EquipmentManager>();


        if (!playerAttacker.CanCastSpell) return;
        var spawnPosition = GetSpawnPosition(item, playerEquipment);

        if (playerAttacker.ChargePerformed)
        {
            playerAnimator.PlayAnimation(PlayerAnimator.offHandShoot, true);
        }
        else
        {
            spawnPosition.Clear();
        }

        playerAttacker.ChargePerformed = false;
        playerManager.IsAiming = false;
    }

    public override void FinalizeAction(ItemWithAttributes item, GameObject go)
    {
        var playerEquipment = go.GetComponent<EquipmentManager>();
        var playerStamina = go.GetComponent<Stamina>();

        var spawnPosition = GetSpawnPosition(item, playerEquipment);
        spawnPosition.transform.Clear();

        var chargedFXPrefab = item.GetAttribute<GameObjectData>(projectileShootAttribute)?.value;
        if (!chargedFXPrefab) return;
        var projectileGO = Instantiate(chargedFXPrefab, spawnPosition);
        var projectile = projectileGO.GetComponent<FireBallProjectile>();
        projectile.Initialize(item, go);

        //move this to its own class for shooting rays
        var cam = Camera.main;
        var center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        if (cam is { })
        {
            var ray = cam.ScreenPointToRay(center);
            Vector3 direction;
            if (Physics.Raycast(ray, out RaycastHit hitInfo, shootMaxDistance))
            {
                direction = (hitInfo.point - spawnPosition.position).normalized;
            }
            else
            {
                var endPoint = ray.GetPoint(shootMaxDistance);
                direction = (endPoint - spawnPosition.position).normalized;
            }

            projectileGO.transform.parent = null;
            projectileGO.GetComponent<Rigidbody>().velocity = direction * projectileForce;
        }

        playerStamina.DrainItemStamina(item, staminaConsumptionAttribute);
    }

    private static Transform GetSpawnPosition(ItemWithAttributes item, EquipmentManager playerEquipment)
    {
        var equipLocation = playerEquipment.GetEquippedItemLocation(item);
        var spawnPosition = equipLocation.GetComponentInChildren<ProjectileSpawnPosition>().transform;
        return spawnPosition;
    }
}


public static class TransformEx
{
    public static Transform Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }

        return transform;
    }
}

/*
 var equippedCategory = playerEquipment.FindEquippedCategory(item, playerEquipment.equipmentLocations);
 var equipLocation = playerEquipment.equipmentLocations[equippedCategory];
 var spawnPosition = equipLocation.GetComponentInChildren<ProjectileSpawnPosition>().transform;*/