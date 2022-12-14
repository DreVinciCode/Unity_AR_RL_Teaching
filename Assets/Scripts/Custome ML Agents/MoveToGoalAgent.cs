using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.InputSystem;
using System.IO;
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

    private string filename = "";


    private class AgentHistory
    {
        public float x_pos;
        public float y_pos;
        public float z_pos;
    }

    private List<AgentHistory> myUserActionList = new List<AgentHistory>();



    public override void Initialize()
    {
        _initialiPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
        PlayerActions.Enable();
        filename = Application.dataPath + "/record_success.csv";

    }

    public override void OnEpisodeBegin()
    {
        //_floorMeshRenderer.material = _defaultMaterial;

        transform.localPosition = new Vector3(Random.Range(0f, 0.5f), 0.14f, Random.Range(-0.127f, 0.125f));
        transform.localRotation = _initialRotation;

        _TargetGoalPose.localPosition = new Vector3(Random.Range(0.0f, 0.5f), Random.Range(0.02f, 0.3f), Random.Range(-0.127f, 0.125f));



    }

    private void RecordGoalPosition()
    {
        AgentHistory myUserAction = new AgentHistory();

        myUserAction.x_pos = _TargetGoalPose.localPosition.x;
        myUserAction.y_pos = _TargetGoalPose.localPosition.y;
        myUserAction.z_pos = _TargetGoalPose.localPosition.z;

        myUserActionList.Add(myUserAction);
        WriteCSV();
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
        float moveY = actions.ContinuousActions[2];

        var moveVector = new Vector3(moveX, moveY, moveZ) * Time.deltaTime * _moveSpeed;
        //var moveVector = new Vector3(moveX, moveY, moveZ) * _moveSpeed;


        // To move the Agent
        transform.localPosition += moveVector;

        //Debug.Log("speed: " + moveVector.magnitude);

        //Apply ros message publisher to move the end effector here

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
        if (other.tag == "wall" || other.tag == "ground" || other.tag == "robot")
        {
            _floorMeshRenderer.material = _loseMaterial;
            SetReward(-1f);
            EndEpisode();
        }

        if (other.tag == "ball")
        {
            _floorMeshRenderer.material = _winMaterial;
            SetReward(1f);
            RecordGoalPosition();
            EndEpisode();
        }    
    }

    public void WriteCSV()
    {
        if (myUserActionList.Count > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
           
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i < myUserActionList.Count; i++)
            {
                tw.WriteLine(myUserActionList[i].x_pos + ", " +
                             myUserActionList[i].y_pos + ", " +
                             myUserActionList[i].z_pos);
            }
            tw.Close();
        }
    }

}
