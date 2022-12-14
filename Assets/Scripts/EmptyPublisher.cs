using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class EmptyPublisher : UnityPublisher<MessageTypes.Std.Empty>
    {
        private MessageTypes.Std.Empty _message;
        public bool _publishMessageCheck { get; set; }


        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            _message = new MessageTypes.Std.Empty();
        }

        public void RL_HomePosition()
        {
            ProcessMessage();
        }

        private void ProcessMessage()
        {
            if (_publishMessageCheck)
                Publish(_message);
        }
    }
}
