using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ty
{
 
    // 玩家同步类  server  to client
  public  class PlayerSyncObjectObjPosRotScale : baseSyncObject
    {
        public  float SendInterval = 0.02f; // 10 messages per second

        //private const float MovementAcceleration = 13f;
        //private Vector2 _input;

        protected SyncBuffer_transform _syncBuffer;

        protected Transform  _transform;

        protected float _timeSinceLastSync;

        protected Quaternion _lastSentqua;

        protected Vector3 _lastSentPosition;

        private void Awake()
        {
           // _rigidbody = transform.gameObject;// GetComponent<Rigidbody>();
            _syncBuffer = new SyncBuffer_transform();
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


        public override void setFrame( Vector3 _EndPos, Quaternion q, Vector3 s, float jiangetime )
        {
            _syncBuffer.AddKeyframe( jiangetime , s , _EndPos , q );
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
                    _transform.localScale = _syncBuffer.Scale;
               
                }
            }
            else
            {
                // movement
                //_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                // teleportation
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // send a keyframe to interpolate to before teleporting
                    SendSyncMessage();

                    var newPosition = new Vector3(UnityEngine.Random.Range(-6f, 6f), 2f, UnityEngine.Random.Range(-6f, 6f));
                    _transform.position = newPosition;
                    _transform.rotation = Quaternion.identity;

                    SendSyncMessage();
                }
            }
        }


        public override void FixedUpdate()
        {
            if ( tyConofig.isRoomOwer() == false )
            {
                return;
            }

            _timeSinceLastSync += Time.deltaTime;
            if (Mathf.Approximately(_timeSinceLastSync, SendInterval) || _timeSinceLastSync > SendInterval)
            {
                SendSyncMessage();
            }
        }

 
        //发送给服务器
        private void SendSyncMessage()
        {
            
            msgServerRoomSyncObject.getMe().sendto(_timeSinceLastSync, this.m_objectId, _transform.position, _transform.rotation, _transform.lossyScale, this.m_objType);
            _lastSentqua       = _transform.rotation;
            _lastSentPosition  = _transform.position;
            _timeSinceLastSync = 0f;
        }

        //  [Command]发送给服务器
 
    }
}
