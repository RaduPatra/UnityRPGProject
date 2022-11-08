using UnityEngine;

namespace ExtensionMethods
{
    public static class MyExtensionMethods
    {
        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void SetAllChildrenActive(this Transform transform, bool status)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(status);
            }
        }
    }
}
