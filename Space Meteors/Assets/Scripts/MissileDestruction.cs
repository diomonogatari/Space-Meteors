using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class MissileDestruction : MonoBehaviour
{
    public float destroyInSeconds = 5f;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject, destroyInSeconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();
        if(collider.name.Equals(Constants.CollidableNames.meteorMissileInstances) && this.gameObject.name.Equals(Constants.CollidableNames.missileInstances))
        {
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
                Instantiate(explosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);//explode the rock
        }
    }
}
