using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour
{

    //Hold a list of all game traits.

    public List<Trait> traitLibrary = new List<Trait>()
    {
        // new Trait("Likes Woody"),
        // new Trait("Prefers pristiness")
    };

    //Method for getting a new trait.

    public Trait GetTrait()
    {

        Trait newTrait = traitLibrary[Random.Range(0, traitLibrary.Count)];
        return newTrait;

    }

}