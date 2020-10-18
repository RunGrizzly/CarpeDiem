using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    GameObject gameController;

    public GameObject pausePanel;

    public Color clockStart;
    public Color clockEnd;

    public Image clockImage;

    ClientManager clientManager;
    EventManager eventManager;
    GameManager gameManager;
    Sequencer sequencer;

    public Transform clientInfoPanel;

    public GameObject traitDispPrefab;

    public TextMeshProUGUI timeLeftValue;
    public TextMeshProUGUI clothesIronedValue;

    // public TextMeshProUGUI clientNameText;

    // Start is called before the first frame update
    void Start()
    {

        gameController = GameObject.Find("GameManager");

        clientManager = gameController.GetComponent<ClientManager>();
        eventManager = gameController.GetComponent<EventManager>();
        gameManager = gameController.GetComponent<GameManager>();
        sequencer = gameController.GetComponent<Sequencer>();

        eventManager.newClient.AddListener(UpdateClientInfo);

    }

    public void UpdateTime(float timeElapsed, float timeAllowed)
    {

        float currTime = timeAllowed - timeElapsed;

        timeLeftValue.text = currTime.ToString();
        clockImage.color = Color.Lerp(clockStart, clockEnd, timeElapsed / timeAllowed);
    }

    public void UpdateScore(int score)
    {
        clothesIronedValue.text = score.ToString();
    }

    public void ClientButtonClick()
    {
        clientManager.GetNewClient();
    }

    public void ExitButtonClick()
    {
        sequencer.QuitToDesktop();
    }

    public void RestartButtonClick()
    {
        sequencer.RestartGame();
    }

    void UpdateClientInfo(Client newClient)
    {

        foreach (Transform child in clientInfoPanel)
        {
            Destroy(child.gameObject);
        }

        TextMeshProUGUI nameDisplay = GameObject.Instantiate(traitDispPrefab).GetComponent<TextMeshProUGUI>();
        nameDisplay.transform.SetParent(clientInfoPanel, false);
        nameDisplay.text = newClient.clientName;

        //Add UI elements for each trait.
        foreach (Trait trait in newClient.clientTraits)
        {
            TextMeshProUGUI traitDisplay = GameObject.Instantiate(traitDispPrefab).GetComponent<TextMeshProUGUI>();
            traitDisplay.transform.SetParent(clientInfoPanel, false);
            traitDisplay.text = trait.traitName;
        }
    }

    void ShowEndScreen()
    {
        //Pause game.
        gameManager.SessionPause(true);

    }

    public void NewIronClick()
    {
        eventManager.newIron.Invoke();
    }

    void QuitGame()
    {
        sequencer.QuitToDesktop();
    }

}