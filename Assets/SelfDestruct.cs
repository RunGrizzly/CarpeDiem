using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public int selfDestructTime;

    private void Start()
    {
        StartCoroutine(SelfDestructStart());
    }

    IEnumerator SelfDestructStart()
    {

        yield return new WaitForSeconds(selfDestructTime);

        Destroy(gameObject);

    }
}