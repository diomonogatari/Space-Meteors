using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GameController : MonoBehaviour
{
    public GameObject player; //knowing who the player is is nice. allows me to call his actions :)
    //I don't know who the enemies will be because there will be a lot of them
    public GameObject enemyContainer;
    public float moveCooldown = 1.0f;
    //The GameController will not know who the enemies are, but the enemies, as they trigger lose conditions, will be able to communicate with the GameController

    private float[] worldBounds = { -3.38f, 3.552f };
    private float lowestPositionY = -3.59f;
    private float currentMoveCooldown = 0f;
    private bool isPlayerDead = false;
    private bool enemyIsMovingLeft = true;
    private bool enemyMovesDown = false;
    private float xMovementOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(enemyContainer);
        MovementCooldown();
    }

    private bool CanMoveSides(float[] worldBounds, GameObject obj)
    {
        /*if the object position is between the lowest bound and the highest bound then he can move*/
        if (currentMoveCooldown.Equals(0))
        {
            if (obj.transform.position.x >= worldBounds[0] && !isPlayerDead && enemyIsMovingLeft) //try move left
            {
                //enemyIsMovingLeft = true;
                return true;
            }
            else if (obj.transform.position.x <= worldBounds[1] && !isPlayerDead && !enemyIsMovingLeft) //try move right
            {
                //enemyIsMovingLeft = false;
                return true;
            }
            else //welp, try move down
            {
                enemyMovesDown = CanMoveDown(this.lowestPositionY, obj);
                enemyIsMovingLeft = !enemyIsMovingLeft;
                return false;
            }
        }
        else
            return false;
    }
    private bool CanMoveDown(float worldBound, GameObject obj)
    {
        /*if the object position is between the lowest bound and the highest bound then he can move*/
        if (obj.transform.position.x >= worldBound && !isPlayerDead)
        {
            return true;
        }
        else
            return false;
    }

    void Move(GameObject obj)
    {
        
        if (CanMoveSides(this.worldBounds, obj))
        {
            if (enemyIsMovingLeft)//move left
                obj.transform.position -= new Vector3(xMovementOffset, 0, 0); //Moves on ticks
            else
                obj.transform.position += new Vector3(xMovementOffset, 0, 0); //Move right
            ResetMovementCoolDown();
        }
        else /*else the position is the allowed bound*/
        {
            /*if it's on the right side of the screen*/
            //if (obj.transform.position.x > 0)
            //    obj.transform.position = new Vector3(worldBounds[1], obj.transform.position.y, 0);//clip on the x position
            //else/*else he is on the left side*/
            //    obj.transform.position = new Vector3(worldBounds[0], obj.transform.position.y, 0);//clip on the x position
        }
        //else he doesn't move
    }

    /*
     * Movement is kinda tricky here
     * 1st you need to know if he wants to move left (original direction)
     * he will move that direction until he can no longer move (world bounds)
     * 
     *
     */
    void MovementCooldown()
    {
        if (currentMoveCooldown > 0f)
            currentMoveCooldown -= Time.fixedDeltaTime;
        else
            currentMoveCooldown = 0;
    }
    void ResetMovementCoolDown()
    {
        currentMoveCooldown = moveCooldown;
    }
}
