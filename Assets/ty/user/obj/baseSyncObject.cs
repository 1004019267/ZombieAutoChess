using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ty
{
    public enum objType
    {
        e_none,
        e_sceneobj,
        e_scenebox,
        e_gun,
        e_player,
        e_otherplayer


    }


    public  class syncFrame
    {
      public  Vector3    _EndPos;
      public  Quaternion _EndRot;
      public    Vector3  _EndScale;
        public float     m_fNextGameTime = 0;
    }





  public  class baseSyncObject : MonoBehaviour
  {
        public      int          m_objectId     = 0; // 
        public      objType      m_objType      = objType.e_none;
      

        syncFrame m_cursyncFrame = new syncFrame();
        syncFrame m_startsyncFrame = new syncFrame();
        private Queue<syncFrame> m_freamqueue = new Queue<syncFrame>();
        Vector3 m_oldPos = new Vector3(0, 0, 0);
        Vector3 m_oldDir  = new Vector3 (0,0,0);
        protected bool m_init = false;

        public float chushizhi =100f;
        float m_speedtime = 100f;


        long _sendttnowServer = 0;
        long _sendttoldServer = 0;


        long _sendttnowClient = 0;
        long _sendttoldClient = 0;


        public virtual void Start()
        {

            _sendttnowServer = _sendttoldServer = DateTime.Now.Ticks;

            _sendttnowClient = _sendttoldClient = DateTime.Now.Ticks;

            m_speedtime = chushizhi;
            init(m_objectId);
        }

        public virtual bool init(int objectid)
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
                m_cursyncFrame._EndPos = base.transform.position;
                m_cursyncFrame._EndRot = base.transform.rotation;
                m_cursyncFrame._EndScale = base.transform.localScale;



               m_startsyncFrame._EndPos   = base.transform.position;
                  m_startsyncFrame._EndRot   = base.transform.rotation;
                  m_startsyncFrame._EndScale = base.transform.localScale;

                m_oldPos = base.transform.position;
                msgServerRoomCreateObject.getMe().sendto(m_objectId, this.transform.position, this.transform.localScale, this.transform.rotation, m_objType);

                m_init = true;
                return true;
           
        }

        //删除
        private void OnDestroy()
        {
            msgServerRoomDeleteObject.getMe().sendto(m_objectId);
            syncObjectMgr.getMe().removeObject(m_objectId);
        }




        public virtual void setFrame( Vector3 _EndPos , Quaternion q , Vector3 s,float jiangetime)
        {
            syncFrame _syncFrame = new syncFrame();
            _syncFrame._EndPos = _EndPos;
            _syncFrame._EndRot = q;
            _syncFrame._EndScale = s;
            _syncFrame.m_fNextGameTime = jiangetime;
            
            m_freamqueue.Enqueue( _syncFrame );
            logMgr.log("当前包数量：" + m_freamqueue.Count );

            FixedUpdate();
        }

        bool m_curfreamisOver = false;
        void updateFream()
        {
            if ( this.m_freamqueue.Count <= 0 )
                return;

            m_fAccumilatedTime -= m_cursyncFrame.m_fNextGameTime;
            m_cursyncFrame   =  m_freamqueue.Dequeue();

            m_startsyncFrame._EndPos = base.transform.position;
            m_startsyncFrame._EndRot = base.transform.rotation;
            m_startsyncFrame._EndScale = base.transform.localScale;


        }

        float m_floatsendPosRot = 0;
        protected virtual void _Update_server()
        {

           // updateFream();
            if (m_init == false)
                return;
           
            if (tyConofig.isRoomOwer() == false)
            {
                return;
            }

            _sendttnowServer = DateTime.Now.Ticks;
            float dd = (_sendttnowServer - _sendttoldServer) / 1000.0f / 1000.0f;
            _sendttoldServer = _sendttnowServer;

            m_floatsendPosRot += dd;
            if( this.m_floatsendPosRot>= m_fFrameLen  )
            {
                this.sendPosRot(m_floatsendPosRot);
                m_floatsendPosRot = 0;
            }

        }


        float m_fAccumilatedTime = 0;
     
        float m_fInterpolation = 0;
     
        float m_fFrameLen = 0.03f;//每一帧长度

        public virtual void Update()
        {

        }


        public virtual void FixedUpdate()
        {
            _Update_server();

            if (m_init == false)
                return;
            if (tyConofig.isRoomOwer() == true)
            {
                return;
            }

         
            if (m_fAccumilatedTime >= m_cursyncFrame.m_fNextGameTime && this.m_freamqueue.Count <= 0)
            {
                m_fAccumilatedTime = m_cursyncFrame.m_fNextGameTime;
                return;
            }

            if (this.m_freamqueue.Count <=3 )
            {
                m_speedtime -= 0.08f;
                if (m_speedtime < chushizhi)
                    m_speedtime = chushizhi;
            }
            else if(this.m_freamqueue.Count > 3)
            {
                m_speedtime += (0.08f* this.m_freamqueue.Count);
                if (m_speedtime > 100f)
                    m_speedtime = 100f;
            }



            _sendttnowClient = DateTime.Now.Ticks;
            float dd = (_sendttnowClient - _sendttoldClient) / 1000.0f / 1000.0f;
            _sendttoldClient = _sendttnowClient;

            m_fAccumilatedTime = m_fAccumilatedTime + dd*(m_speedtime);
            //如果真实累计的时间超过游戏帧逻辑原本应有的时间,则循环执行逻辑,确保整个逻辑的运算不会因为帧间隔时间的波动而计算出不同的结果


       

            ////计算两帧的时间差,用于运行补间动画
             m_fInterpolation = m_fAccumilatedTime / m_cursyncFrame.m_fNextGameTime;// 


            //logMgr.log("dd " + dd);
            //logMgr.log("m_fNextGameTime " + m_cursyncFrame.m_fNextGameTime);


            //logMgr.log("m_fInterpolation"+ m_fInterpolation);
            //logMgr.log("m_cursyncFrame.m_fNextGameTime" + m_cursyncFrame.m_fNextGameTime);

            //更新渲染位置
            //m_callUnit.updateRenderPosition(m_fInterpolation);

            //位置旋转的线性插值。
            base.transform.position = Vector3.Lerp(base.transform.position,   m_cursyncFrame._EndPos,    dd * (m_speedtime));
            base.transform.rotation = Quaternion.Lerp(base.transform.rotation,   m_cursyncFrame._EndRot, dd * (m_speedtime));
            base.transform.localScale = Vector3.Lerp(base.transform.localScale, m_cursyncFrame._EndScale, dd * (m_speedtime));

            float dis = Vector3.Distance(base.transform.position, m_cursyncFrame._EndPos);

            while (  this.m_freamqueue.Count > 0)
            {
                updateFream();
            }
            
 
        }


         Vector3     m_buffscale = new Vector3(-100000, -1000000, -1000000);
         Vector3     m_buffpos   = new Vector3(-100000, -1000000, -1000000);
         Quaternion  m_buffrot   = new Quaternion(-100000, -1000000, -1000000, -1000000);

        float m_sendtime_sync = 0;
        //发送位置旋转
        void sendPosRot(float jiangetime  )
        {

            float dis = Vector3.Distance(base.transform.position, m_buffpos);
            float dissca = Vector3.Distance(base.transform.localScale, m_buffscale);
            float ss = Quaternion.Angle(base.transform.rotation, m_buffrot);

     


            if (m_init == false)
                return;
            if ( tyConofig.isRoomOwer() ==false )
            {
                return;
            }

            m_sendtime_sync += Time.fixedDeltaTime;
   
      

            if (dis != 0.0f || ss != 0.0f || dissca != 0.0f  || m_sendtime_sync>1)
            {
                m_buffpos   = base.transform.position;
                m_buffrot   = base.transform.rotation;
                m_buffscale = base.transform.localScale;
                m_sendtime_sync = 0;
                msgServerRoomSyncObject.getMe().sendto( jiangetime, m_objectId, base.transform.position, base.transform.rotation, base.transform.localScale, this.m_objType);
            }



        }
  


    }
}
