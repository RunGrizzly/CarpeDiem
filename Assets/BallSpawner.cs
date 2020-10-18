using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;

    public List<Transform> spawnPositions = new List<Transform>();

    private void Start()
    {
        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls()
    {

        int ballCount = 0;

        while (ballCount < 100)
        {

            GameObject ballClone = GameObject.Instantiate(ballPrefab);
            ballClone.transform.position = spawnPositions[Random.Range(0, spawnPositions.Count)].position;

            ballCount += 1;

            yield return new WaitForSeconds(2.0f);
        }

    }
}