using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectController : MonoBehaviour
{
    public GameObject area;
    public string TargetAreaTag;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(TargetAreaTag))
        {
            //envController.GoalTouched(Team.Blue);
        }
    }
}
