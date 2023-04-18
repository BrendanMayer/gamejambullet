using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTrail : MonoBehaviour
{
    public Vector3 hitpoint;
    public float timeNeededToReach;

    private void Start()
    {
        if (gameObject != null)
            LeanTween.move(gameObject, hitpoint, timeNeededToReach);
 
    }
}
