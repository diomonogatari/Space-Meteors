using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class EnemyMissil : Enemy, IInstantiable
{
    public float destroyInSeconds = 5f;






    // Start is called before the first frame update
    void Start()
    {
        OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        Destroy(gameObject, destroyInSeconds);
    }
}
