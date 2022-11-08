using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Utils
{
    public static void Swap<T>(ref T left, ref T right)
    {
        T temp;
        temp = left;
        left = right;
        right = temp;
    }

    public static Transform GetClosestTransformInList(Transform transformToCheck, List<Transform> targets)
    {
        var minDist = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (var target in targets)
        {
            var distFromEnemyToTarget = Vector3.Distance(transformToCheck.transform.position, target.position);
            if (distFromEnemyToTarget < minDist)
            {
                closestTarget = target;
                minDist = distFromEnemyToTarget;
            }
        }

        return closestTarget;
    }

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}


[Serializable]
public class LazyValue<T>
{
    [SerializeField] private T _value;
    private bool _initialized = false;
    private InitializerDelegate _initializer;

    public delegate T InitializerDelegate();

    /// <summary>
    /// Setup the container but don't initialise the value yet.
    /// </summary>
    /// <param name="initializer"> 
    /// The initialiser delegate to call when first used. 
    /// </param>
    public LazyValue(InitializerDelegate initializer)
    {
        _initializer = initializer;
    }

    /// <summary>
    /// Get or set the contents of this container.
    /// </summary>
    /// <remarks>
    /// Note that setting the value before initialisation will initialise 
    /// the class.
    /// </remarks>
    public T value
    {
        get
        {
            // Ensure we init before returning a value.
            ForceInit();
            return _value;
        }
        set
        {
            // Don't use default init anymore.
            _initialized = true;
            _value = value;
        }
    }

    public T ValueNoInit => _value;

    /// <summary>
    /// Force the initialisation of the value via the delegate.
    /// </summary>
    public void ForceInit()
    {
        if (!_initialized)
        {
            _value = _initializer();
            _initialized = true;
        }
    }
}

//a lazy value that can be initialized directly from inspector
public class LazyValueTest<T> where T : new()
{
    [SerializeField] private T _value = new T();
    private bool _initialized = false;
    private InitializerDelegate _initializer;

    public delegate T InitializerDelegate();

    public void Init(InitializerDelegate initializer)
    {
        _initializer = initializer;
        _initialized = false;
    }

    public T value
    {
        get
        {
            ForceInit();
            return _value;
        }
        set
        {
            _initialized = true;
            _value = value;
        }
    }

    //this should only be used inside the initializer method
    public T ValueNoInit => _value;


    public void ForceInit()
    {
        if (_initializer != null && !_initialized)
        {
            _value = _initializer();
            _initialized = true;
        }
    }
}