using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class BoundaryTest : UnityPublisher<MessageTypes.Std.Float32>
    {
        private float _current_z_value;
        private MessageTypes.Std.Float32 _message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            _message = new MessageTypes.Std.Float32();
        }

        public void UpdateBottomConstraint()
        {
            _current_z_value = transform.localPosition.y;
            _message.data = _current_z_value;

            Publish(_message);
        }
    }
}
