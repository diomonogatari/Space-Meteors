using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class MissileDestruction : MonoBehaviour, IInstantiable
{
    public float destroyInSeconds = 5f;



    // Start is called before the first frame update
    void Start()
    {
        OnDestroy();
    }

    public void OnDestroy()
    {
        Destroy(gameObject, destroyInSeconds);
    }
}
