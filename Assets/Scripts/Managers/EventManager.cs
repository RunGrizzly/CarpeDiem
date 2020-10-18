using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NewClientEvent : UnityEvent<Client>
{

}

public class TaskCompleteEvent : UnityEvent
{

}

public class NewIronEvent : UnityEvent
{

}

public class NewClothesEvent : UnityEvent
{

}

public class TimeExpiredEvent : UnityEvent
{

}

public class AddScoreEvent : UnityEvent<int>
{

}

public class EventManager : MonoBehaviour
{

    //Game Events
    public NewClientEvent newClient;
    public TaskCompleteEvent taskComplete;
    public NewIronEvent newIron;
    public NewClothesEvent newClothes;

    public TimeExpiredEvent timeExpired;

    public AddScoreEvent addScore;

    void Awake()
    {

        if (newClient == null)
            newClient = new NewClientEvent();

        if (taskComplete == null)
            taskComplete = new TaskCompleteEvent();

        if (newIron == null)
            newIron = new NewIronEvent();

        if (newClothes == null)
            newClothes = new NewClothesEvent();

        if (timeExpired == null)
            timeExpired = new TimeExpiredEvent();

        if (addScore == null)
            addScore = new AddScoreEvent();

    }

}