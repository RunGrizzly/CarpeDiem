using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Iron : MonoBehaviour
{
    public List<Transform> rayCastPoints = new List<Transform>();

    public LayerMask clothesMask;

    public float ironDistance;

    private void Update()
    {
        foreach (Transform point in rayCastPoints)
        {

            RaycastHit hit;

            if (Physics.SphereCast(point.position, 0.5f, -transform.up, out hit, ironDistance, clothesMask))
            {

                //Store the mesh that was hit.
                Mesh mesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                //Get the ironable componnent.
                Ironable ironable = hit.collider.gameObject.GetComponent<Ironable>();
                //Get the int of the vertex that was hit.
                int hitVertex = GetClosestVertex(hit, mesh.GetTriangles(0));

                if (!ironable.hitVertices.Contains(hitVertex))
                {

                    ironable.hitVertices.Add(hitVertex);

                }

            }
        }

    }

    public int GetClosestVertex(RaycastHit aHit, int[] aTriangles)
    {
        var b = aHit.barycentricCoordinate;
        int index = aHit.triangleIndex * 3;
        if (aTriangles == null || index < 0 || index + 2 >= aTriangles.Length)
            return -1;
        if (b.x > b.y)
        {
            if (b.x > b.z)
                return aTriangles[index]; // x
            else
                return aTriangles[index + 2]; // z
        }
        else if (b.y > b.z)
            return aTriangles[index + 1]; // y
        else
            return aTriangles[index + 2]; // z
    }
}