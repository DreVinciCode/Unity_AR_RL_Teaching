using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectBehavior : MonoBehaviour
{
    [HideInInspector]
    public PushTargetToGoal agent;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("wall"))
            agent.BallDropped();

        if (col.gameObject.CompareTag("agent"))
            Debug.Log("Agent");
    }
}
