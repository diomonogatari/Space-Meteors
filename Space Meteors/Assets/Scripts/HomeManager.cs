using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class HomeManager : MonoBehaviour
{
    private Scene activeScene;

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetButton(Constants.InputAxis.cancel))
            if (activeScene.name.Equals(Constants.Scenes.mainMenu))
                QuitGame();

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(Constants.Scenes.game);
    }
    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
