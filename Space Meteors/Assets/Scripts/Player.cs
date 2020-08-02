using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Player : MonoBehaviour, IActions, IControllable
{
    #region Publics

    public GameObject playerObject;

    public float playerVelocity = 5.0f;

    public GameObject missileObject;
    [Range(1, 5)]
    public uint maxLives = 3;
    public float fireCooldown = 1.5f;
    [Range(1f, 10f)]
    public float missileSpeed = 5.0f;

    #endregion


    #region Privates

    /*We can get xMinWorldBounds by multiplying + -1 of max as the value is the negative solution*/
    private float[] worldBounds = { -8.454f, 8.454f };
    private uint currentLives;
    private float currentCooldown = 0f;
    private bool isDead = false;
    private Transform shootingLocation;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        currentLives = maxLives;

        /*Get the shootingLocation*/
        shootingLocation = playerObject.transform.Find(Constants.GameSceneObjects.shootingLocation);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        FiringCooldown(); //Time never stops going forward

        /*Check for Input*/
        if (IsPlayerMoving())
            Move();
        if (IsPlayerFiring())
            Fire();
    }
    #region Movement Methods

    public void Move()
    {
        // If he can move then let him move
        if (CanMove(this.worldBounds, playerObject))
        {
            playerObject.transform.position += new Vector3(Input.GetAxis(Constants.InputAxis.horizontalAxis), 0, 0) * playerVelocity * Time.fixedDeltaTime;
        }
        else /*else the position is the allowed bound*/
        {
            /*if it's on the right side of the screen*/
            if (playerObject.transform.position.x > 0)
                playerObject.transform.position = new Vector3(worldBounds[1], playerObject.transform.position.y, 0);//clip on the x position
            else/*else he is on the left side*/
                playerObject.transform.position = new Vector3(worldBounds[0], playerObject.transform.position.y, 0);//clip on the x position
        }
        //else he doesn't move

    }


    public bool IsPlayerMoving()
    {
        //If the player is pressing the move keys then he is trying to move
        if (Input.GetButton(Constants.InputAxis.horizontalAxis))
        {
            return true;
        }
        else
            return false;
    }


    public bool CanMove(float[] worldBounds, GameObject obj)
    {
        /*if the object position is between the lowest bound and the highest bound then he can move*/
        if (obj.transform.position.x >= worldBounds[0] && obj.transform.position.x <= worldBounds[1] && !isDead)
            return true;
        else
            return false;
    }

    #endregion

    #region Firing Methods
    public bool IsPlayerFiring()
    {
        //If the player is pressing the fire keys then he is trying to fire
        if (Input.GetButton(Constants.InputAxis.fire1))
        {
            return true;
        }
        else
            return false;
    }

    public void Fire()
    {
        if (CanFire())
        {
            //instanciate a missile from the shootingLocation moving at a certain speed in Y up direction
            GameObject missile = Instantiate(missileObject, shootingLocation.position, shootingLocation.rotation);
            Rigidbody2D body = missile.GetComponent<Rigidbody2D>();

            body.velocity = transform.TransformDirection(Vector2.up * missileSpeed);
            ResetCooldown();
        }
    }

    /*he can only fire after the cooldown has passed*/
    public bool CanFire()
    {
        if (currentCooldown.Equals(0) && !isDead)
            return true;
        return false;
    }
    private void FiringCooldown()
    {
        if (this.currentCooldown > 0f)
            this.currentCooldown -= Time.fixedDeltaTime;

        if (this.currentCooldown < 0f)
            this.currentCooldown = 0f;
    }
    #endregion


    private void ResetCooldown()
    {
        currentCooldown = fireCooldown;
    }
}
