using UnityEngine;


    public class TestMono : MonoBehaviour
    {
        public AttributeBaseSO baseTest;

        [ContextMenu("TestType")]
        public void Test()
        {
            Debug.Log(baseTest.GetType());
        }
    }
