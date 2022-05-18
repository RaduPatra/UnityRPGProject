using UnityEngine;

public class SingletonAccessTest : MonoBehaviour
{
    public SingletonTesting singletonTesting;
    public string retrievedString = "bruh";
    private void Awake()
    {
        singletonTesting = SingletonTesting.Instance;
    }

    [ContextMenu("access")]
    public void AccessAgainTest()
    {
        singletonTesting = SingletonTesting.Instance;
    }
    
    
}