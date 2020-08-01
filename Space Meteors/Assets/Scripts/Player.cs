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

    #endregion


    #region Privates

    /*We can get xMinWorldBounds by multiplying + -1 of max as the value is the negative solution*/
    private float[] worldBounds = { -8.454f, 8.454f };
    //private Rigidbody2D rgbPlayer;
    private uint currentLives;


    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        currentLives = maxLives;
        //rgbPlayer = playerObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
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
        if (obj.transform.position.x >= worldBounds[0] && obj.transform.position.x <= worldBounds[1])
            return true;
        else
            return false;
    }

    #endregion

    #region Firing Methods
    public bool IsPlayerFiring()
    {
        return false;
    }

    public void Fire()
    {
        Debug.Log("Non implemented method fired!!!");
    }

    public bool CanFire()
    {
        return false;
    }
    #endregion


}
