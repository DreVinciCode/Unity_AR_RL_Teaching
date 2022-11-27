using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.InputSystem;
using UnityEngine;

public class PushTargetToGoal : Agent
{
    [HideInInspector]
    public GoalDetectModified goalDetect;

    [SerializeField] private Transform _TargetGoalPose;
    [SerializeField] private GameObject _GoalLocation;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Material _winMaterial;
    [SerializeField] private Material _loseMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private MeshRenderer _floorMeshRenderer;

    private Vector3 _initialiPosition;
    private Quaternion _initialRotation;
    public InputAction PlayerActions;

    public override void Initialize()
    {
        goalDetect = _GoalLocation.GetComponent<GoalDetectModified>();
        goalDetect.agent = this;

        _initialiPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
        PlayerActions.Enable();
    }

    public override void OnEpisodeBegin()
    {
        //_floorMeshRenderer.material = _defaultMaterial;

        transform.localPosition = new Vector3(Random.Range(0f, 0.5f), 0.14f, Random.Range(-0.127f, 0.125f));
        transform.localRotation = _initialRotation;

        _TargetGoalPose.localPosition = new Vector3(Random.Range(0.0f, 0.5f), Random.Range(0.02f, 0.3f), Random.Range(-0.127f, 0.125f));
        _GoalLocation.transform.localPosition = new Vector3(Random.Range(0.0f, 0.5f), 0.01f, Random.Range(-0.127f, 0.125f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(_TargetGoalPose.localPosition);
        //sensor.AddObservation(_GoalLocation.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveY = actions.ContinuousActions[2];

        var moveVector = new Vector3(moveX, moveY, moveZ) * Time.deltaTime * _moveSpeed;
        //var moveVector = new Vector3(moveX, moveY, moveZ) * _moveSpeed;

        // To move the Agent
        transform.localPosition += moveVector;

        //Apply incentive
        AddReward(-1f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = PlayerActions.ReadValue<Vector3>().x;
        continousActions[1] = PlayerActions.ReadValue<Vector3>().z;
        continousActions[2] = PlayerActions.ReadValue<Vector3>().y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall" || other.tag == "ground")
        {
            _floorMeshRenderer.material = _loseMaterial;
            SetReward(-1f);
            EndEpisode();
        }

        if (other.tag == "ball")
        {
            _floorMeshRenderer.material = _winMaterial;
            SetReward(1f);
            EndEpisode();
        }
    }
}
