using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class BoundaryTest : UnityPublisher<MessageTypes.Std.Float32>
    {
        private float _current_z_value;
        private MessageTypes.Std.Float32 _message;

        public GameObject GroundBoundary;

        private Rigidbody Rigidbody;
        protected override void Start()
        {
            Rigidbody = GroundBoundary.GetComponent<Rigidbody>();
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            _message = new MessageTypes.Std.Float32();
        }

        public void UpdateBottomConstraint()
        {
            _current_z_value = (float)GroundBoundary.transform.localPosition.y;
            _message.data = _current_z_value;
            SetPositionBackup();
            Publish(_message);
        }

        public void SetPositionBackup()
        {
            Rigidbody.velocity = Vector3.zero;
        }


    }
}
