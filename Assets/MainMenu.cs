using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject mainGroup;
    public GameObject loadingGroup;

    public Image loadingBar;

    GameObject gameController;

    ClientManager clientManager;
    EventManager eventManager;
    Sequencer sequencer;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //Game controller gameobject.
        gameController = GameObject.Find("GameManager");

        //Sub managers
        clientManager = gameController.GetComponent<ClientManager>();
        eventManager = gameController.GetComponent<EventManager>();
        sequencer = gameController.GetComponent<Sequencer>();
        audioManager = gameController.GetComponent<AudioManager>();

        mainGroup.SetActive(true);
        loadingGroup.SetActive(false);
    }

    public void StartGameButton()
    {
        StartCoroutine(DoLoadScreen());
    }

    public IEnumerator DoLoadScreen()
    {

        mainGroup.SetActive(false);
        loadingGroup.SetActive(true);

        float waitTime = 6;
        float elapsed = 0;

        loadingBar.transform.localScale = new Vector3(0, 1, 1);

        audioManager.SetPlayClip(audioManager.sessionSource, audioManager.sessionMusic);

        while (elapsed < waitTime)
        {

            print("Loading");

            loadingBar.transform.localScale = new Vector3(elapsed / waitTime, 1, 1);
            elapsed += 1;
            yield return new WaitForSecondsRealtime(1.0f);
        }

        StartCoroutine(gameController.GetComponent<Sequencer>().BeginSession());

    }
}