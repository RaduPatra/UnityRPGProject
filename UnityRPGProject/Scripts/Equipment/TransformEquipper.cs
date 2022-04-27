using UnityEngine;

public class TransformEquipper : MonoBehaviour, IEquipper
{
    public GameObject Equip(GameObject go)
    {
        return Instantiate(go, transform);
    }

    public void Unequip()
    {
        if (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}