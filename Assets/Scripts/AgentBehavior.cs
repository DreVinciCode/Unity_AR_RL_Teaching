using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

namespace Unity.MLAgents.Policies
{
    public class AgentBehavior : MonoBehaviour
    {
        public BehaviorParameters BehaviorParameters;

        private bool _state = false;

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
    }
}
