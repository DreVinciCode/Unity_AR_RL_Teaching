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
    [SerializeField] private Material _winMaterial;
    [SerializeField] private Material _loseMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private MeshRenderer _floorMeshRenderer;

    private Vector3 _initialiPosition;
    private Quaternion _initialRotation;

    public InputAction PlayerActions;
  
    public override void Initialize()
    {
        _initialiPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
        PlayerActions.Enable();

    }

    public override void OnEpisodeBegin()
    {
        //_floorMeshRenderer.material = _defaultMaterial;

        transform.localPosition = new Vector3(Random.Range(-0.1f, 0.4f), 0.024f, Random.Range(0.045f, 0.5f));
        transform.localRotation = _initialRotation;

        _TargetGoalPose.localPosition = new Vector3(Random.Range(0.52f, 1f), 0.0214f, Random.Range(0.06f, 0.56f));
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
