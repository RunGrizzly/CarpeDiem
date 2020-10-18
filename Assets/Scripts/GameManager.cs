using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    AudioManager audioManager;

    public Task currentTask;

    public int clothingIroned;

    public bool sessionPaused;

    ScreenUI screenUI;

    //Timer

    bool sessionRunning;

    public float timeElapsed;
    public float timeAllowed;

    void Start()
    {

        timeElapsed = 0;
        timeAllowed = 90;

        //Turn cursor off.
        Cursor.visible = false;

        currentTask = new Task();

        currentTask.fulfilments.Add(new Fulfilment(gameObject.GetComponent<TraitManager>().traitLibrary[0], 2));

        GetComponent<EventManager>().addScore.AddListener(AddScore);

        //Get UI
        screenUI = GameObject.Find("ScreenUI").GetComponent<ScreenUI>();

        StartCoroutine(StartTimer());

        audioManager = GetComponent<AudioManager>();

        SessionPause(false);

    }

    IEnumerator StartTimer()
    {

        timeElapsed = 0;
        screenUI.UpdateTime(timeElapsed, timeAllowed);

        while (timeElapsed < timeAllowed)
        {
            sessionRunning = true;
            yield return new WaitForSecondsRealtime(1.0f);
            timeElapsed += (1 * Time.timeScale);
            screenUI.UpdateTime(timeElapsed, timeAllowed);
        }

        GetComponent<EventManager>().timeExpired.Invoke();
        EndSession();
        sessionRunning = false;

    }

    private void Update()
    {
        if (Input.GetButtonDown("KeyTab") && screenUI != null)
        {
            SessionPause();
        }
    }

    public void AddScore(int value)
    {
        clothingIroned += value;
        screenUI.UpdateScore(clothingIroned);
    }

    public void EndSession()
    {
        //Session ended.
        SessionPause(true);
        sessionRunning = false;
    }

    public void SessionPause(bool state)
    {
        if (sessionRunning == true)
        {
            //Switch state.
            sessionPaused = state;

            if (sessionPaused == true)
            {
                //Pause game.
                print("Game was paused");

                //Turn cursor on.
                Cursor.visible = true;

                Time.timeScale = 0;
                screenUI.pausePanel.SetActive(true);
                audioManager.sessionSource.Pause();
                audioManager.menuSource.UnPause();
            }

            else
            {
                //Unpause game.
                print("Game was unpaused");
                //Turn cursor off.
                Cursor.visible = false;

                Time.timeScale = 1;
                screenUI.pausePanel.SetActive(false);
                audioManager.sessionSource.UnPause();
                audioManager.menuSource.Pause();
            }
        }
    }

    public void SessionPause()
    {
        if (sessionRunning == true)
        {

            //Switch state.
            sessionPaused = !sessionPaused;

            if (sessionPaused == true)
            {
                //Pause game.
                print("Game was paused");

                //Turn cursor on.
                Cursor.visible = true;

                Time.timeScale = 0;
                screenUI.pausePanel.SetActive(true);
                audioManager.sessionSource.Pause();
                audioManager.menuSource.UnPause();
            }

            else
            {
                //Unpause game.
                print("Game was unpaused");
                //Turn cursor off.
                Cursor.visible = false;

                Time.timeScale = 1;
                screenUI.pausePanel.SetActive(false);
                audioManager.sessionSource.UnPause();
                audioManager.menuSource.Pause();
            }
        }
    }

}