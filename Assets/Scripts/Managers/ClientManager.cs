using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{

    public GameObject clientPrefab;

    public GameObject clientGO;
    public Client currentClient;

    public int minTraits;
    public int maxTraits;

    Client client;

    private void Start()
    {
        //If our max traits exceeds the amount of available traits.
        if (maxTraits > gameObject.GetComponent<TraitManager>().traitLibrary.Count)
        {
            maxTraits = gameObject.GetComponent<TraitManager>().traitLibrary.Count;
        }
    }

    public void GetNewClient()
    {

        if (clientGO != null)
        {
            GameObject.Destroy(clientGO);
        }

        //Initialise our client.
        clientGO = GameObject.Instantiate(clientPrefab);
        currentClient = clientGO.GetComponent<Client>();

        currentClient.clientName = Random.Range(0, 200).ToString();
        currentClient.gameObject.name = currentClient.clientName;

        //Get random trait amount.
        int traitsAmount = Random.Range(minTraits, maxTraits);

        //While our client hasn't filled trait quota.
        while (currentClient.clientTraits.Count < traitsAmount)
        {
            //Find a new trait.
            Trait newTrait = gameObject.GetComponent<TraitManager>().GetTrait();

            //If we don't already have it.
            if (!currentClient.clientTraits.Contains(newTrait))
            {
                //Add it
                currentClient.clientTraits.Add(newTrait);
            }

        }

        //Invoke new client event.
        GetComponent<EventManager>().newClient.Invoke(currentClient);

    }
}