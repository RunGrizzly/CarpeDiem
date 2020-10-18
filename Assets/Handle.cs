using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Handle : MonoBehaviour
{

    public List<GameObject> touchingJoints = new List<GameObject>();

    public HandController toucher;

    public bool palmTouching;

    public bool gripped;

    public float gripFactor;

    public GameObject parentObject;

    public TextMeshProUGUI helperText;

    private void OnTriggerEnter(Collider other)
    {

        GameObject go = other.gameObject;

        if (go.tag == "Finger")
        {

            if (go.name == "PalmCollider")
            {
                palmTouching = true;
                toucher = go.GetComponentInParent<HandController>();
                toucher.heldObject = parentObject;
                ShowHelper(false);
            }

            touchingJoints.Add(go);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.name == "PalmCollider")
        {
            palmTouching = false;
            toucher.heldObject = null;
            toucher = null;
            ShowHelper(true);
        }

        if (touchingJoints.Contains(go))
        {
            touchingJoints.Remove(go);
        }
    }

    public void ShowHelper(bool query)
    {

        if (query == true)
        {
            helperText.enabled = true;
        }
        else
        {
            helperText.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (toucher != null && toucher.GetGripFactor() >= 200)
        {
            GameObject palm = touchingJoints.Find(x => x.name == ("PalmCollider")).gameObject;
            if (parentObject.GetComponent<Rigidbody>())
            {
                parentObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            parentObject.transform.SetParent(palm.transform, true);

            //gripped = true;

        }

        if (toucher == null || toucher.GetGripFactor() < 300)
        {
            if (parentObject.GetComponent<Rigidbody>())
            {
                parentObject.GetComponent<Rigidbody>().isKinematic = false;
            }

            parentObject.transform.SetParent(null);
            //gripped = false;
        }
    }
}