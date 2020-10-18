using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sequencer : MonoBehaviour
{

    public string mainMenuScene;

    public List<string> mainSceneLoad = new List<string>();

    GameManager gameManager;
    AudioManager audioManager;

    //First thing the sequencer does.
    private void Start()
    {

        audioManager = GetComponent<AudioManager>();

        //Add our scene loader callback.
        SceneManager.sceneLoaded += OnSceneLoaded;
        //Do initialise.
        InitialiseSession();
    }

    void InitialiseSession()
    {
        SceneUnloader(new List<string>() { "MainMenu", "GameScene", "Viewport" });

        audioManager.sessionSource.Pause();
        //When initialised to the main menu - set our main menu music from the audio manager.
        audioManager.SetPlayClip(audioManager.menuSource, audioManager.sessionMusic);
        //Load our main menu scene.
        SceneLoader("MainMenu");
    }

    public IEnumerator BeginSession()
    {

        //Load our main scene load package.
        SceneLoader(mainSceneLoad);
        // Wait until last scene is loaded.
        while (!SceneManager.GetSceneByName("Viewport").isLoaded)
        {
            yield return null;
        }

        //Unload the main menu.
        SceneManager.UnloadSceneAsync("MainMenu");

        //Add a game manager.
        gameManager = gameObject.AddComponent<GameManager>();

    }

    void PauseGame()
    {
        gameManager.SessionPause();
    }

    public void RestartGame()
    {
        InitialiseSession();
    }

    public void QuitToDesktop()
    {
        print("Quit the application");
        // #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        //         Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        // #endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
        Application.Quit();
#elif (UNITY_WEBGL)
        Application.OpenURL("https://joshooahh.itch.io/good-morning");
#endif

    }

    void SceneLoader(List<string> sceneList)
    {

        foreach (string sceneName in sceneList)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

    }

    void SceneUnloader(List<string> sceneList)
    {
        foreach (string sceneName in sceneList)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {

                //Unload the scene
                SceneManager.UnloadSceneAsync(sceneName);
            }

        }
    }

    void SceneLoader(string sceneName)
    {

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "GameScene":
                //Set active scene to be our game scene.
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
                break;

        }
    }
}