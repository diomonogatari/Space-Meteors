using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using System.Threading;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player; //knowing who the player is is nice. allows me to call his actions :)
    //I don't know who the enemies will be because there will be a lot of them
    public GameObject enemyContainer;
    public float moveCooldown = 1.2f;
    public Text winText;
    public Text loseText;
    public bool isPlayerDead = false;
    //The GameController will not know who the enemies are, but the enemies, as they trigger lose conditions, will be able to communicate with the GameController

    private float[] worldBounds = { -3.38f, 3.552f };
    private float lowestPositionY = -3.59f;
    private float currentMoveCooldown = 0f;
    private bool enemyIsMovingLeft = true;
    private bool enemyMovesDown = false;
    private float xMovementOffset = 0.5f;
    private float yMovementOffset = 0.35f;

    private Scene activeScene;

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckInput();

        Move(enemyContainer);
        MovementCooldown();

        if (isPlayerDead)
            Lose();
        if (!AreAnyEnemiesAlive() && !isPlayerDead)
            Win();
    }

    private bool CanMoveSides(float[] worldBounds, GameObject obj)
    {
        /*if the object position is between the lowest bound and the highest bound then he can move*/
        if (currentMoveCooldown.Equals(0))
        {
            if (obj.transform.position.x >= worldBounds[0] + xMovementOffset && !isPlayerDead && enemyIsMovingLeft) //try move left
            {
                //enemyIsMovingLeft = true;
                return true;
            }
            else if (obj.transform.position.x + xMovementOffset <= worldBounds[1] && !isPlayerDead && !enemyIsMovingLeft) //try move right
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
        if (obj.transform.position.y >= worldBound + yMovementOffset && !isPlayerDead)
        {
            return true;
        }
        else
            return false;
    }

    void Move(GameObject obj)
    {
        if (enemyMovesDown)
        {
            obj.transform.position -= new Vector3(0, yMovementOffset, 0); //Moves on ticks
            enemyMovesDown = !enemyMovesDown;
            ResetMovementCoolDown();
        }
        else
        {
            if (CanMoveSides(this.worldBounds, obj))
            {
                if (enemyIsMovingLeft)//move left
                    obj.transform.position -= new Vector3(xMovementOffset, 0, 0); //Moves on ticks
                else
                    obj.transform.position += new Vector3(xMovementOffset, 0, 0); //Move right
                ResetMovementCoolDown();
            }
        }
    }

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

    void CheckInput()
    {
        if (Input.GetButton(Constants.InputAxis.cancel))
            if (activeScene.name.Equals(Constants.Scenes.game))
                LoadHomeMenu();
            else if (activeScene.name.Equals(Constants.Scenes.mainMenu))
                Application.Quit();

    }

    void LoadHomeMenu()
    {
        SceneManager.LoadScene(Constants.Scenes.mainMenu);
    }

    bool AreAnyEnemiesAlive()
    {
        if (enemyContainer.transform.childCount.Equals(0))
            return false;//no enemies alive
        return true; //enemies are alive
    }

    void Win()
    {
        var playerScript = player.GetComponent<Player>();
        playerScript.fireCooldown = 0.125f;
        StartCoroutine(DisplayTextAndLoadHome(winText));
    }
    void Lose()
    {
        StartCoroutine(DisplayTextAndLoadHome(loseText));
    }


    IEnumerator DisplayTextAndLoadHome(Text text)
    {
        //Wait for 4 seconds
        text.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        LoadHomeMenu();
    }
}
