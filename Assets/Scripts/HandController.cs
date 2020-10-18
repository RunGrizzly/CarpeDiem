using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    public Animator animator;

    public GameObject heldObject;

    Camera camera;

    //public float gripFactor;

    [Range(1, 10)]
    public int sensitivity = 1;

    [Range(1, 10)]
    public int handSpeed = 1;

    [Range(-10, 10)]
    public float handDistance = 1.2f;

    public Transform root;

    // public Transform wrist;

    List<List<DynamicJoint>> affectedChains = new List<List<DynamicJoint>>();

    public List<DynamicJoint> palmBones = new List<DynamicJoint>();

    public List<DynamicJoint> thumbBones = new List<DynamicJoint>();

    public List<DynamicJoint> indexBones = new List<DynamicJoint>();

    public List<DynamicJoint> middleBones = new List<DynamicJoint>();

    public List<DynamicJoint> ringBones = new List<DynamicJoint>();

    public List<DynamicJoint> pinkyBones = new List<DynamicJoint>();

    public List<DynamicJoint> wristBones = new List<DynamicJoint>();

    public bool controllingThumb;
    public bool controllingIndex;
    public bool controllingMiddle;
    public bool controllingRing;
    public bool controllingPinky;
    public bool controllingWrist;

    public bool positionLock = false;

    Vector3 mousePos;

    //Coroutines

    Coroutine rotateArm;
    Coroutine spinArm;

    void Start()
    {

        //Find camera.
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //Store orig transform for each of our joints.

        foreach (DynamicJoint dj in wristBones)
        {
            dj.CaptureTransforms();
            // dj.minBend = -120;
            // dj.maxBend = 80;
        }

        foreach (DynamicJoint dj in palmBones)
        {
            dj.CaptureTransforms();

        }

        foreach (DynamicJoint dj in thumbBones)
        {
            dj.CaptureTransforms();
            // dj.minBend = -4;
            // dj.maxBend = 150;
        }

        foreach (DynamicJoint dj in indexBones)
        {
            dj.CaptureTransforms();
            dj.minBend = -10;
            dj.maxBend = 65;
        }

        foreach (DynamicJoint dj in middleBones)
        {
            dj.CaptureTransforms();
            dj.minBend = -10;
            dj.maxBend = 65;
        }

        foreach (DynamicJoint dj in ringBones)
        {
            dj.CaptureTransforms();
            dj.minBend = -10;
            dj.maxBend = 65;
        }

        foreach (DynamicJoint dj in pinkyBones)
        {
            dj.CaptureTransforms();
            dj.minBend = -10;
            dj.maxBend = 65;
        }

    }

    private void Update()
    {

        //Find out which fingers we are affecting.

        //Detect inputs.
        //Q - Pinky
        if (Input.GetButtonDown("KeyQ"))
        {
            print("controlling pinky");
            controllingPinky = true;
            affectedChains.Add(pinkyBones);
        }

        if (Input.GetButtonUp("KeyQ"))
        {
            controllingPinky = false;
            affectedChains.Remove(pinkyBones);
        }

        //W - Ring
        if (Input.GetButtonDown("KeyW"))
        {
            print("controlling ring");
            controllingRing = true;
            affectedChains.Add(ringBones);
        }

        if (Input.GetButtonUp("KeyW"))
        {
            controllingRing = false;
            affectedChains.Remove(ringBones);
        }

        //E - Middle
        if (Input.GetButtonDown("KeyE"))
        {
            print("controlling middle");
            controllingMiddle = true;
            affectedChains.Add(middleBones);
        }

        if (Input.GetButtonUp("KeyE"))
        {

            controllingMiddle = false;
            affectedChains.Remove(middleBones);
        }

        //R - Index
        if (Input.GetButtonDown("KeyR"))
        {
            print("controlling index");
            controllingIndex = true;
            affectedChains.Add(indexBones);
        }

        if (Input.GetButtonUp("KeyR"))
        {
            controllingIndex = false;
            affectedChains.Remove(indexBones);
        }

        //Space - Thumb
        if (Input.GetButtonDown("KeySpace"))
        {
            print("controlling thumb");
            controllingThumb = true;
            affectedChains.Add(thumbBones);
        }

        if (Input.GetButtonUp("KeySpace"))
        {
            controllingThumb = false;
            affectedChains.Remove(thumbBones);
        }

        //Alt - Wrist
        if (Input.GetButtonDown("KeyAlt"))
        {
            controllingWrist = true;
            affectedChains.Add(wristBones);
        }

        if (Input.GetButtonUp("KeyAlt"))
        {
            controllingWrist = false;
            affectedChains.Remove(wristBones);
        }

        if (Input.GetMouseButtonDown(2))
        {
            animator.SetTrigger("Punch");
        }

        // gripFactor = GetGripFactor();

        if (affectedChains.Count > 0)
        {
            //Affect each list currently being affected.
            foreach (List<DynamicJoint> chain in affectedChains)
            {
                foreach (DynamicJoint dj in chain)
                {

                    //gripFactor += dj.bend;

                    //Adjust bend factor of joint.
                    dj.bend += Input.mouseScrollDelta.y * sensitivity;
                    //Clamp it.
                    dj.bend = Mathf.Clamp(dj.bend, dj.minBend, dj.maxBend);

                    //If we are holding something, prevent close
                    if (heldObject != null)
                    {
                        dj.bend = Mathf.Clamp(dj.bend, dj.minBend, dj.bend);
                    }

                    if (chain != thumbBones)
                    {
                        //Adjust rotation based on bend.
                        dj.joint.localEulerAngles = new Vector3(dj.origRot.x, dj.origRot.y, dj.origRot.z - dj.bend);
                    }
                    else
                    {
                        //Adjust rotation based on bend.
                        dj.joint.localEulerAngles = new Vector3(dj.origRot.x, dj.origRot.y, dj.origRot.z + dj.bend);
                    }
                }
            }
        }
        else
        {
            handDistance += Input.GetAxis("Mouse ScrollWheel");
        }

        if (Input.GetMouseButtonDown(0))
        {
            rotateArm = StartCoroutine(DoRotateArm());
        }

        if (Input.GetMouseButtonDown(1))
        {
            spinArm = StartCoroutine(DoSpinArm());
        }

        //If we are not rotating.

        if (positionLock == false)
        {

            // float mousePosX = Mathf.Clamp(Input.mousePosition.x, 0, Screen.width);
            // float mousePosY = Mathf.Clamp(Input.mousePosition.y, 0, Screen.height);
            // float mousePosZ = Mathf.Clamp(handDistance, 0, 15);

            float mousePosX = Mathf.Lerp(-10, 10, Input.mousePosition.x / Screen.width);
            float mousePosY = Mathf.Lerp(-10, 10, Input.mousePosition.y / Screen.height);
            float mousePosZ = Mathf.Lerp(-2, 15, handDistance / 10);

            Vector3 newPos = new Vector3(mousePosX, mousePosZ, mousePosY);

            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * handSpeed);

            // var v3 = Input.mousePosition;
            // v3.z = handDistance;
            // v3 = camera.ScreenToWorldPoint(v3);

            // transform.position = Vector3.Lerp(transform.position, v3, Time.deltaTime * handSpeed);
        }

    }

    public float GetGripFactor()
    {

        float gripFactor = 0;

        foreach (DynamicJoint dj in thumbBones)
        {

            gripFactor += dj.bend;
        }

        foreach (DynamicJoint dj in indexBones)
        {

            gripFactor += dj.bend;
        }

        foreach (DynamicJoint dj in middleBones)
        {

            gripFactor += dj.bend;
        }

        foreach (DynamicJoint dj in ringBones)
        {

            gripFactor += dj.bend;
        }

        foreach (DynamicJoint dj in pinkyBones)
        {

            gripFactor += dj.bend;
        }

        return gripFactor;
    }

    IEnumerator DoSpinArm()
    {
        positionLock = true;

        Vector3 mouseIndex = Input.mousePosition;

        while (Input.GetMouseButton(1))
        {
            print("holding right mouse");

            float mouseXDelta = Input.mousePosition.x - mouseIndex.x;
            mouseXDelta = Mathf.Clamp(mouseXDelta, -100, 100);
            mouseXDelta *= -1;

            print(mouseXDelta);

            Quaternion rotAmount = Quaternion.Euler(0, mouseXDelta / 100, 0);
            transform.rotation *= rotAmount;

            // transform.localEulerAngles = new Vector3(yawIndex - mouseXDelta, transform.eulerAngles.y, pitchIndex - mouseYDelta);

            yield return null;
        }

        positionLock = false;
        print("let go of right mouse");

    }

    IEnumerator DoRotateArm()
    {

        positionLock = true;

        Vector3 mouseIndex = Input.mousePosition;

        while (Input.GetMouseButton(0))
        {
            print("holding middle mouse");

            float mouseXDelta = Input.mousePosition.x - mouseIndex.x;
            mouseXDelta = Mathf.Clamp(mouseXDelta, -100, 100);
            mouseXDelta *= -1;
            float mouseYDelta = Input.mousePosition.y - mouseIndex.y;
            mouseYDelta = Mathf.Clamp(mouseYDelta, -100, 100);
            mouseYDelta *= -1;

            print(mouseXDelta);
            print(mouseYDelta);

            Quaternion rotAmount = Quaternion.Euler(mouseXDelta / 100, 0, mouseYDelta / 100);
            transform.rotation *= rotAmount;

            // transform.localEulerAngles = new Vector3(yawIndex - mouseXDelta, transform.eulerAngles.y, pitchIndex - mouseYDelta);

            yield return null;
        }

        positionLock = false;
        print("let go of middle mouse");

    }

}