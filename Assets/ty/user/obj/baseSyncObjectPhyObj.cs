using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ty
{

    public  class syncFrame_phy
    {
        public  Vector3 _EndPos;
        public  Quaternion _EndRot;
        public    Vector3 _EndScale;
        public float m_fNextGameTime = 0;
    }


  public  class baseSyncObjectPhyObj : baseSyncObject
    {
        private const float SendInterval = 0.1f; // 10 messages per second
        private const float MovementAcceleration = 2f;//移动速度
        [HideInInspector]
        public Vector2 _input = Vector2.zero;//移动的输入

        private Rigidbody _rigidbody;
        private SyncBuffer _syncBuffer;
        private Transform _transform;

        private float _timeSinceLastSync;
        private Vector3 _lastSentVelocity;
        private Vector3 _lastSentPosition;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _syncBuffer = GetComponent<SyncBuffer>();
            _transform = transform;
        }

        public override void Start()
        {
            base.Start();
        }

        public override bool init(int objectid)
        {
            
                 if (m_init == true)
                {
                    logMgr.log("已经加载");
                    return false;
                }

                if(objectid == 0)
                {
                    objectid = msgRoomCreateObject.getMe().createObjectId();
                }

                baseSyncObject _baseSyncObject= syncObjectMgr.getMe().getObject(objectid);
                if (_baseSyncObject == this)
                {
                    logMgr.log("已经加载");
                    return false;//已经加载
                }
 
                m_objectId = objectid;
                syncObjectMgr.getMe().addObject(this);
  
                msgServerRoomCreateObject.getMe().sendto(m_objectId, this.transform.position, this.transform.localScale, this.transform.rotation, m_objType);
                m_init = true;
                return true;
           
        }


        public  void setFrame(Vector3 _EndPos, Quaternion q, Vector3 s,Vector3 angle,Vector3 a,Vector3 a_angle ,float jiangetime)
        {
            _syncBuffer.AddKeyframe(jiangetime, _EndPos, q, s,angle,a,a_angle);
        }

        public override   void Update()
        {
            if (tyConofig.isRoomOwer() == false)
            {
                if (_syncBuffer.HasKeyframes)
                {
                    _syncBuffer.UpdatePlayback(Time.deltaTime);
                    _transform.position = _syncBuffer.Position;
                    _transform.rotation = _syncBuffer.Rotation;
                }
            }
//            else
//            {
//                // movement
//                _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
//
//                // teleportation
//                if (Input.GetKeyDown(KeyCode.Space))
//                {
//                    // send a keyframe to interpolate to before teleporting
//                    SendSyncMessage();
//
//                    var newPosition = new Vector3(UnityEngine.Random.Range(-6f, 6f), 2f, UnityEngine.Random.Range(-6f, 6f));
//                    _rigidbody.position = newPosition;
//                    _rigidbody.rotation = Quaternion.identity;
//                    _rigidbody.velocity = Vector3.zero;
//                    _rigidbody.angularVelocity = Vector3.zero;
//
//                    // and then immediately send a teleportation keyframe with 0 interpolation time
//                    SendSyncMessage();
//                }
//            }
        }


        public override void FixedUpdate()
        {
            if ( tyConofig.isRoomOwer() == true )
            {
                // control the rigidbody locally
               // _rigidbody.AddForce(new Vector3(_input.x, 0f, _input.y) * MovementAcceleration * Time.deltaTime, ForceMode.VelocityChange);
               //_rigidbody.MovePosition(_rigidbody.position + new Vector3(_input.x, 0f, _input.y) * MovementAcceleration * Time.deltaTime);
               _rigidbody.velocity = new Vector3(_input.x, 0f, _input.y) * MovementAcceleration * Time.deltaTime;
               _timeSinceLastSync += Time.deltaTime;

                if (_rigidbody.velocity != _lastSentVelocity || _rigidbody.position != _lastSentPosition)
                {
                    if (Mathf.Approximately(_timeSinceLastSync, SendInterval) || _timeSinceLastSync > SendInterval)
                    {
                        // send rigidbody data from client to server on change
                        SendSyncMessage();
                    }
                }
            }
        }


        //发送给服务器
        private void SendSyncMessage()
        {
            CmdSync(_timeSinceLastSync, _rigidbody.position, _rigidbody.rotation, _rigidbody.velocity);
            _lastSentVelocity = _rigidbody.velocity;
            _lastSentPosition = _rigidbody.position;
            _timeSinceLastSync = 0f;
        }

        //  [Command]发送给服务器
        private void CmdSync(float interpolationTime, Vector3 position, Quaternion rotation, Vector3 velocity)
        {

            msgServerPhysicSyncObj.getMe().sendto( interpolationTime , this.m_objectId ,  position , rotation , velocity , this.m_objType );

          //new WebSocketMessage.PlayerSync
          //  {
          //      Identity = Identity,
          //      InterpolationTime = _timeSinceLastSync,
          //      Position = _rigidbody.position,
          //      Rotation = _rigidbody.rotation,
          //      Velocity = _rigidbody.velocity
          //  });
          //  _lastSentVelocity = _rigidbody.velocity;
          //  _timeSinceLastSync = 0f;



            // add keyframe to buffer
            // _syncBuffer.AddKeyframe(interpolationTime, position, rotation, velocity);
            // send it to other clients
            //  RpcSync(interpolationTime, position, rotation, velocity);
        }

        //  [ClientRpc]  发送给服务器
        //private void RpcSync(float interpolationTime, Vector3 position, Quaternion rotation, Vector3 velocity)
        //{
        //    // prevent receiving keyframes on owner client and host
        //    if (tyConofig.isRoomOwer() == false)
        //        return;

 
        //    // add keyframe to buffer
        //    _syncBuffer.AddKeyframe(interpolationTime, position, rotation, velocity);
        //}



    }
}
