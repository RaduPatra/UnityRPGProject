using UnityEngine;
using ExtensionMethods;

public class ProjectileFactory //maybe move factory to gameobject instead of SO and reference it from there?
{
    private GameObject caster;
    private GameObject projectileFXPrefab;

    public ProjectileFactory(GameObject caster, GameObject projectileFXPrefab)
    {
        this.caster = caster;
        this.projectileFXPrefab = projectileFXPrefab;
    }

    public FireBallProjectile CreateProjectile(Transform spawnLocation)
    {
        return CreateProjectileWithItem(null, spawnLocation);
    }

    public FireBallProjectile CreateProjectileWithItem(ItemWithAttributes item, Transform spawnLocation)
    {
        if (!projectileFXPrefab) return default;
        var spawnTransform = GetSpawnTransform(spawnLocation);
        spawnTransform.Clear();
        var projectileGO = GameObject.Instantiate(projectileFXPrefab, spawnTransform);
        var projectile = projectileGO.GetComponent<FireBallProjectile>();
        projectile.Initialize(item, caster);
        return projectile;
    }

    private Transform GetSpawnTransform(Transform weaponLocation)
    {
        var spawnPosition = weaponLocation.GetComponentInChildren<ProjectileSpawnPosition>().transform;
        return spawnPosition;
    }
}