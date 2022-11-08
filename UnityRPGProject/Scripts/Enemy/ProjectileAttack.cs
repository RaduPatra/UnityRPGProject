using System.Net;
using UnityEngine;
using Object = UnityEngine.Object;


[CreateAssetMenu(fileName = "New enemy attack", menuName = "Enemy Attacks/ProjectileAttack", order = 1)]
public class ProjectileAttack : EnemyAttack
{
    public GameObject projectileFXPrefab;
    public float shootMaxDistance;
    public float projectileForce;

    public override void Attack(EnemyAI enemy)
    {
        base.Attack(enemy);
    }

    public override void FinalizeAttack(EnemyAI enemy)
    {
        var playerEquipment = enemy.GetComponent<EnemyEquipment>();
        var rayProvider = enemy.GetComponent<IRayProvider>();
        
        var weaponLocation = GetWeaponLocation(enemy.currentAttack.attackHandType, playerEquipment);
        var projFactory = new ProjectileFactory(enemy.gameObject, projectileFXPrefab);
        var projectile = projFactory.CreateProjectile(weaponLocation);
        var ray = rayProvider.CreateRay();
        var shooter = new ProjectileShooter(enemy.gameObject);
        shooter.ShootProjectile(ray, shootMaxDistance, projectileForce, projectile);
    }

    private static Transform GetWeaponLocation(ItemCategory category, EnemyEquipment enemyEquipment)
    {
        return enemyEquipment.EquipmentLocations[category];
    }
}