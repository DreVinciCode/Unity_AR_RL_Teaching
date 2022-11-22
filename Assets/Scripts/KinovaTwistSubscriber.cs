using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class KinovaTwistSubscriber : UnitySubscriber<MessageTypes.Geometry.Twist>
    {
        public MoveTargetToGoalAgent moveTargetToGoalAgent;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.Twist message)
        {
            moveTargetToGoalAgent.Write(message);
        }
    }
}
