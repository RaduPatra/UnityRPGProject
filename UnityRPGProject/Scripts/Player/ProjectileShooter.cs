using System;
using System.Linq;
using UnityEngine;

public class ProjectileShooter
{
    private CollisionIgnorer collisionIgnorer;
    public ProjectileShooter(GameObject caster)
    {
        collisionIgnorer = caster.GetComponent<CollisionIgnorer>();
    }

    public void ShootProjectile(Ray ray, float shootMaxDistance, float projectileForce,
        FireBallProjectile projectileGO)
    {
        Vector3 shootDirection = Vector3.zero;

        RaycastHit[] hitObjects = Physics.RaycastAll(ray, shootMaxDistance, -1,QueryTriggerInteraction.Ignore);
        Array.Sort(hitObjects, (x, y) => x.distance.CompareTo(y.distance));

        foreach (var hitObject in hitObjects)
        {
            if (collisionIgnorer.allColliders.Contains(hitObject.collider)) continue;
            shootDirection = (hitObject.point - projectileGO.transform.position).normalized;
            break;
        }

        if (hitObjects.Length == 0)
        {
            var endPoint = ray.GetPoint(shootMaxDistance);
            shootDirection = (endPoint - projectileGO.transform.position).normalized;
        }

        projectileGO.transform.parent = null;
        projectileGO.GetComponent<Rigidbody>().velocity = shootDirection * projectileForce;
    }
}