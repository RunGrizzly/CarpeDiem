using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trait
{

    public string traitName;

    [Range(0, 10)]
    public int traitFulfilment;

}