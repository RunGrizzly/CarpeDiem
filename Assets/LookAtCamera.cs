using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(2 * transform.position - camera.transform.position);
    }
}