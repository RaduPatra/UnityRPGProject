using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class MeshEquipper : SerializedMonoBehaviour, IEquipper
{
    [SerializeField]
    private readonly Dictionary<string, EquipmentPartInfo> defaultParts = new Dictionary<string, EquipmentPartInfo>();

    [SerializeField]
    private Transform defaultTransform;

    public GameObject Equip(GameObject go)
    {
        var parentTransform = go.transform;
        for (var i = 0; i < parentTransform.childCount; i++)
        {
            var currentChild = parentTransform.GetChild(i);
            var childName = currentChild.name;
            var childType = childName.Remove(childName.LastIndexOf('_'));
            var childMesh = currentChild.GetComponent<SkinnedMeshRenderer>();

            defaultParts.TryGetValue(childType, out var correspondingPartInfo);
            if (correspondingPartInfo == null) return default;

            childMesh.rootBone = correspondingPartInfo.skinnedMeshRenderer.rootBone;
            childMesh.bones = correspondingPartInfo.skinnedMeshRenderer.bones;

            foreach (var part in correspondingPartInfo.defaultPartsToToggle)
            {
                part.gameObject.SetActive(false);
            }
        }
        return Instantiate(go, transform);

    }

    public void Unequip()
    {
        /*go through each child as above
        get equipment type
        enable default type associated with equipment type
        toggle defaultPartsToToggle back on 
        destroy children from equipLocation*/
        
        defaultTransform.gameObject.SetActive(true);
        for (var i = 0; i < defaultTransform.childCount; i++)
        {
            var currentChild = defaultTransform.GetChild(i);
            currentChild.gameObject.SetActive(true);
        }

        for (var i = 0; i < transform.childCount; i++)
        {
            var currentChild = transform.GetChild(i);
            Destroy(currentChild.gameObject);
        }
    }
}

public class EquipmentPartInfo
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public List<Transform> defaultPartsToToggle = new List<Transform>();
}