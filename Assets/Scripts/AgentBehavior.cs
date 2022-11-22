using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Demonstrations;

namespace Unity.MLAgents.Policies
{
    public class AgentBehavior : MonoBehaviour
    {
        public BehaviorParameters BehaviorParameters;
        public DemonstrationRecorder DemonstrationRecorder;

        private bool _state = false;
        private bool _record = false;

        public void ToggleBehaviorType()
        {
            _state = !_state;
            if (_state)
                SelectHeuristic();
            else
                SelectInference();
        }

        public void SelectHeuristic()
        {
            BehaviorParameters.BehaviorType = BehaviorType.HeuristicOnly;
        }

        public void SelectInference()
        {
            BehaviorParameters.BehaviorType = BehaviorType.InferenceOnly;
        }

        public void ToggleHeuristicRecording()
        {
            _record = !_record;
            DemonstrationRecorder.Record = _record;
        }
    }
}
