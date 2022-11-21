using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveTargetToGoalAgent : Agent
{
    [SerializeField] private float _moveSpeed = 1f;


    public override void Initialize()
    {
        //_initialiPosition = transform.localPosition;
        //_initialRotation = transform.localRotation;
        //PlayerActions.Enable();
    }

    public override void OnEpisodeBegin()
    {
        //_floorMeshRenderer.material = _defaultMaterial;

        //transform.localPosition = new Vector3(Random.Range(0f, 0.5f), 0.14f, Random.Range(-0.127f, 0.125f));
        //transform.localRotation = _initialRotation;
        //KinovaTwistPublisher._publishMessageCheck = true;

        //_TargetGoalPose.localPosition = new Vector3(Random.Range(0.0f, 0.5f), Random.Range(0.02f, 0.3f), Random.Range(-0.127f, 0.125f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        //sensor.AddObservation(_TargetGoalPose.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveY = actions.ContinuousActions[2];

        //var moveVector = new Vector3(moveX, moveY, moveZ) * Time.deltaTime * _moveSpeed;
        var moveVector = new Vector3(moveX, moveY, moveZ) * _moveSpeed;


        // To move the Agent
        //transform.localPosition += moveVector;

        //Apply ros message publisher to move the end effector here
        //KinovaTwistPublisher.ActionReceiver(moveVector);


        //Apply incentive
        //AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        //continousActions[0] = PlayerActions.ReadValue<Vector3>().x;
        //continousActions[1] = PlayerActions.ReadValue<Vector3>().z;
        //continousActions[2] = PlayerActions.ReadValue<Vector3>().y;
    }

    void OnCollisionEnter(Collision c)
    {

    }
}