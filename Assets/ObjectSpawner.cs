using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerType { Iron, Clothes }

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectPrefabs = new List<GameObject>();
    GameObject objectClone;
    public SpawnerType spawnerType;

    public List<Transform> spawnPositions = new List<Transform>();

    private void Start()
    {

        switch (spawnerType)
        {
            case (SpawnerType.Iron):
                GameObject.Find("GameManager").GetComponent<EventManager>().newIron.AddListener(SpawnObject);
                break;

            case (SpawnerType.Clothes):
                GameObject.Find("GameManager").GetComponent<EventManager>().newClothes.AddListener(SpawnObject);
                break;

        }

        SpawnObject();

    }

    void SpawnObject()
    {

        if (objectClone != null)
        {
            Destroy(objectClone);
        }

        objectClone = GameObject.Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Count)]);
        objectClone.transform.position = spawnPositions[Random.Range(0, spawnPositions.Count)].position;
    }
}