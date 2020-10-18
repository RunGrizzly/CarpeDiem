using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DynamicJoint
{

    public Transform joint;

    public float minBend;
    public float maxBend;

    public float bend;

    public Vector3 origPos;
    public Vector3 origRot;
    public Vector3 origScale;

    public void CaptureTransforms()
    {
        origPos = joint.localPosition;
        origRot = joint.localEulerAngles;
        origScale = joint.localScale;
    }
}