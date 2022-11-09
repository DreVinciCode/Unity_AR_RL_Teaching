using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.InputSystem;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform _TargetGoalPose;
    [SerializeField] private float _moveSpeed = 1f;

    private Vector3 _initialiPosition;
    private Quaternion _initialRotation;

    public InputAction PlayerActions;
  
    public override void Initialize()
    {
        _initialiPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
        PlayerActions.Enable();

    }
    /*
    protected override void OnEnable()
    {
        //PlayerActions.Enable();
    }

    protected override void OnDisable()
    {
        //PlayerActions.Disable();
    }
    */
    public override void OnEpisodeBegin()
    {
        //transform.position = Vector3.zero;
        transform.localPosition = _initialiPosition;
        transform.localRotation = _initialRotation;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(_TargetGoalPose.position);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        // To move the Unity Agent
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * _moveSpeed;


        //Apply ros message publisher to move the end effector here
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = PlayerActions.ReadValue<Vector2>().x;
        continousActions[1] = PlayerActions.ReadValue<Vector2>().y;

    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "wall")
        {
            SetReward(-1f);
            EndEpisode();
        }

        if (other.tag == "ball")
        {
            SetReward(1f);
            EndEpisode();
        }    
    }   
}
