using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{
    class ExplosionDestruction : MonoBehaviour, IInstantiable
    {
        public float destroyInSeconds = 5f;


        private void Start()
        {
            OnDestroy();
        }

        public void OnDestroy()
        {
            Destroy(gameObject, this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length -0.178f);
        }
    }
}
