using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ironable : MonoBehaviour
{

    EventManager eventManager;
    GameManager gameManager;

    public int scoreValue;

    public List<int> hitVertices = new List<int>();
    public Mesh mesh;

    public Vector3[] allVertices;

    public float ironCompletion;
    public float completionThreshold;

    private void Start()
    {
        eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        mesh = GetComponent<MeshFilter>().mesh;
        allVertices = mesh.vertices;
    }

    private void Update()
    {

        //Color hit vertices to be green.
        Color[] colors = new Color[mesh.vertices.Length];

        foreach (int vertex in hitVertices)
        {
            colors[vertex] = Color.green;
        }

        mesh.colors = colors;

        print(hitVertices.Count + " Have been hit");
        print(allVertices.Length + " Total vertices");
        print("Completion factor = " + ironCompletion);

        ironCompletion = ((float) hitVertices.Count / (float) allVertices.Length) * 100;

        if (ironCompletion >= completionThreshold)
        {

            eventManager.addScore.Invoke(scoreValue);
            eventManager.newClothes.Invoke();
        }

    }
}