using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy : MonoBehaviour, IActions
{

    public GameObject explosion;
    public GameObject enemyMissile;
    public float missileSpeed = 5.0f;

    /* meteors bounds
     *      min x:-8.665
     *      max x: 8.42 
     */
    private float[] worldBounds = { -8.665f, 8.42f };
    private bool isDead = false;
    private float firingCooldown;
    private Transform shootingLocation;

    // Start is called before the first frame update
    void Start()
    {
        firingCooldown = Random.Range(4f, 6.0f);
        shootingLocation = this.gameObject.transform.Find(Constants.GameSceneObjects.shootingLocation);
    }

    // Update is called once per frame
    void Update()
    {
        FiringCooldown(); //Time never stops going forward
        Fire();
    }
    #region Firing

    public bool CanFire()
    {
        if (firingCooldown.Equals(0) && !isDead)
            return true;
        return false;
    }
    private void FiringCooldown()
    {
        if (this.firingCooldown > 0f)
            this.firingCooldown -= Time.fixedDeltaTime;

        if (this.firingCooldown < 0f)
            this.firingCooldown = 0f;
    }
    public void Fire()
    {
        if (CanFire())
        {
            //instanciate a missile from the shootingLocation moving at a certain speed in Y up direction
            GameObject missile = Instantiate(enemyMissile, shootingLocation.position, shootingLocation.rotation);
            Rigidbody2D body = missile.GetComponent<Rigidbody2D>();

            body.velocity = transform.TransformDirection(Vector2.down * missileSpeed);
            ResetCooldown();
        }
    }

    private void ResetCooldown()
    {
        firingCooldown = Random.Range(8f, 15f);
    }

    #endregion

    #region Movement

    public bool CanMove(float[] worldBounds, GameObject obj)
    {
        return false;
    }

    public void Move()
    {
        Debug.Log("not implement");
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();
        switch (collider.name)
        {
            case "Missile(Clone)":
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
                Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
                break;
            default:
                break;
        }
    }
}
