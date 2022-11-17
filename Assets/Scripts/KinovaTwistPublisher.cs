using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class KinovaTwistPublisher : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        public bool _publishMessageCheck { get; set; }

        private MessageTypes.Geometry.Twist message;

        private float _maxVelocity = 0.02f;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
        }

        public void ActionReceiver(Vector3 action)
        {
            message.linear = GetGeometryVector3(action);
            message.angular = GetGeometryVector3(new Vector3(0f, 0f, 0f));
            Publish(message);
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();

            //geometryVector3.x = vector3.x;
            //geometryVector3.y = vector3.y;
            //geometryVector3.z = vector3.z;

            geometryVector3.x = -vector3.x;
            geometryVector3.y = -vector3.z;
            geometryVector3.z = vector3.y;


            return geometryVector3;
        }
    }
}
