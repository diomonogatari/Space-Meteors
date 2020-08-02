using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy : MonoBehaviour, IActions
{

    public GameObject explosion;

    /* meteors bounds
     *      min x:-8.665
     *      max x: 8.42 
     */
    private float[] worldBounds = { -8.665f, 8.42f };
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Firing

    public bool CanFire()
    {
        Debug.Log("not implement yet");
        return false;
    }

    public void Fire()
    {
        Debug.Log("not implement yet");
    }

    #endregion

    #region Movement

    public bool CanMove(float[] worldBounds, GameObject obj)
    {
        /*if the object position is between the lowest bound and the highest bound then he can move*/
        if (obj.transform.position.x >= worldBounds[0] && obj.transform.position.x <= worldBounds[1] && !isDead)
            return true;
        else
            return false;
    }

    public void Move()
    {
        Debug.Log("not implement yet");
    }

    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.name)
        {
            case "Missile(Clone)":
                Destroy(this.gameObject);
                Destroy(collision.collider.gameObject);
                Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
                break;
            default:
                break;
        }
    }

}
