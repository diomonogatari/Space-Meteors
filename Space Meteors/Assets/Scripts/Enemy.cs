using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
    private float firingCooldown;
    private Transform shootingLocation;
    private float randomShootingRange = 8.0f;
    private float shootingOffset = 2.5f;
    private float cooldownOffset = 4.0f;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        firingCooldown = Random.Range(randomShootingRange, randomShootingRange + 4f);
        shootingLocation = this.gameObject.transform.Find(Constants.GameSceneObjects.shootingLocation);
        gameController = this.gameObject.GetComponentInParent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
    #region Firing

    public bool CanFire()
    {
        //GetPlayerLocation
        var c = GameObject.FindGameObjectsWithTag(Constants.Tags.player).First();
        if (c.gameObject.transform.position.x + cooldownOffset >= this.gameObject.transform.position.x
                &&                                                                            /*I'm using this because the randomness creates meteor showers that are impossible to dodge*/
             c.gameObject.transform.position.x - cooldownOffset <= this.gameObject.transform.position.x
           )
        {
            if (firingCooldown.Equals(0)
                &&
                c.gameObject.transform.position.x + shootingOffset >= this.gameObject.transform.position.x
                &&
                c.gameObject.transform.position.x - shootingOffset <= this.gameObject.transform.position.x
                ) return true;

            FiringCooldown();
            return false;
        }
        else
        {
            return false;
        }
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
        firingCooldown = Random.Range(randomShootingRange, randomShootingRange + 4f);
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
            case Constants.CollidableNames.missileInstances:
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
                gameController.moveCooldown -= 0.055f;//I have 22 rocks and I start at 1.2f cooldown. Every rock that gets shot subtracts a relative equal part to the cooldown (1.2f / 22 = 0.(54)...
                Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
                //this must add points to the score
                break;
            case Constants.CollidableNames.player:
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
                Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
                //this doesnt
                break;
            default:
                break;
        }
    }

}
