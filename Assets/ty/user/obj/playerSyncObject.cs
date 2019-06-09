using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ty
{
    class playerSyncObject : PlayerSyncObjectObjPosRotScale
    {
       
        public override void Update()
        {
             if (tyConofig.isRoomOwer() == false)
            {
                if (this.m_objType == objType.e_player)
                {
                    if (_syncBuffer.HasKeyframes)
                    {
                        _syncBuffer.UpdatePlayback(Time.deltaTime);
                        _transform.position = _syncBuffer.Position;
                        _transform.rotation = _syncBuffer.Rotation;
                        _transform.localScale = _syncBuffer.Scale;
               
                    }
                }
            }
             else
             {
                 if (this.m_objType == objType.e_otherplayer)
                 {
                     if (_syncBuffer.HasKeyframes)
                     {
                         _syncBuffer.UpdatePlayback(Time.deltaTime);
                         _transform.position = _syncBuffer.Position;
                         _transform.rotation = _syncBuffer.Rotation;
                         _transform.localScale = _syncBuffer.Scale;
               
                     }
                 }
             }
          


        }
        
        private void SendSyncMessage()
        {
            
            msgServerPlayerSyncObject.getMe().sendto(_timeSinceLastSync, this.m_objectId, _transform.position, 
                _transform.rotation, _transform.lossyScale, this.m_objType,tyConofig.g_ty_playid);
            _lastSentqua       = _transform.rotation;
            _lastSentPosition  = _transform.position;
            _timeSinceLastSync = 0f;
        }


        public override void FixedUpdate()
        {
            if ( tyConofig.isRoomOwer() == false )
            {
                if (this.m_objType == objType.e_otherplayer)
                {
                    if (Mathf.Approximately(_timeSinceLastSync, SendInterval) || _timeSinceLastSync > SendInterval)
                    {
                        SendSyncMessage();
                    }
                    _timeSinceLastSync += Time.deltaTime;
                }
            }
            else
            {
                if (this.m_objType == objType.e_player)
                {
                    if (Mathf.Approximately(_timeSinceLastSync, SendInterval) || _timeSinceLastSync > SendInterval)
                    {
                        SendSyncMessage();
                    }
                    _timeSinceLastSync += Time.deltaTime;
                }
                
            }

            
        }
    }



}
