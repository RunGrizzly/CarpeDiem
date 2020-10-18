using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButton : MonoBehaviour
{
    public GameObject presser;
    public bool buttonPressed;

    protected EventManager eventManager;

    private void Start()
    {
        eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == presser)
        {
            buttonPressed = true;
            DoButtonAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            buttonPressed = false;
        }
    }

    public virtual void DoButtonAction()
    {

    }
}