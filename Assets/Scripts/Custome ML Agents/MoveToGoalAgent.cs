using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class MoveToGoalAgent : Agent
{
    public override void OnActionReceived(ActionBuffers actions)
    {
        //base.OnActionReceived(actions); 
        //Debug.Log(actions.DiscreteActions[0]);
        Debug.Log(actions.ContinuousActions[0] + ": " + actions.ContinuousActions[1]);

    }
}