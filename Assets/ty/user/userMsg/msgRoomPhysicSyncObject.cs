using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ty
{
    //同步 位置 和 旋转 朝向 没有同步
    class msgRoomPhysicSyncObject : baseUserMsg
    {
        static msgRoomPhysicSyncObject g_msglogin;
        public static msgRoomPhysicSyncObject getMe()
        {
            if (msgRoomPhysicSyncObject.g_msglogin == null)
            {
                msgRoomPhysicSyncObject.g_msglogin = new msgRoomPhysicSyncObject();
            }
            return msgRoomPhysicSyncObject.g_msglogin;
        }

        public msgRoomPhysicSyncObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_physicobj;
        }
      


        // 
        public override void handle(JObject jo)
        {
            if (tyConofig.isRoomOwer() == true)
            {
                return;
            }

            msgRoomCreateObject.getMe().handle(jo);

            int srcid = Convert.ToInt32((string)jo["srcid"]);
            int objid = Convert.ToInt32((string)jo["objid"]);
            double x = Convert.ToDouble((string)jo["x"]);
            double y = Convert.ToDouble((string)jo["y"]);
            double z = Convert.ToDouble((string)jo["z"]);
            Vector3 pos = new Vector3((float)x, (float)y, (float)z);
          //  logMgr.log("myid" + tyConofig.g_roomid + "srcid" + srcid + "objid" + objid);
            baseSyncObject _baseSyncObject = syncObjectMgr.getMe().getObject(objid);

            double rx = Convert.ToDouble((string)jo["qx"]);
            double ry = Convert.ToDouble((string)jo["qy"]);
            double rz = Convert.ToDouble((string)jo["qz"]);
            double rw = Convert.ToDouble((string)jo["qw"]);

            Quaternion rot = new Quaternion((float)rx, (float)ry, (float)rz, (float)rw);

            double sx = Convert.ToDouble((string)jo["sx"]);
            double sy = Convert.ToDouble((string)jo["sy"]);
            double sz = Convert.ToDouble((string)jo["sz"]);

            float   jiangetime       = (float)Convert.ToDouble((string)jo["jg"]);
            //long    _sendttnowServer = Convert.ToInt64((string)jo["now"]);
           // ulong _tttnow = GetCurrentTimeByMiliSec();
           // logMgr.log("间隔时间：" + (_tttnow - _sendttnowServer)/1000f/1000f);

            Vector3 scale = new Vector3((float)sx, (float)sy, (float)sz);
            
            double anglespeedx = Convert.ToDouble((string)jo["anglespeedx"]);
            double anglespeedy = Convert.ToDouble((string)jo["anglespeedy"]);
            double anglespeedz = Convert.ToDouble((string)jo["anglespeedz"]);
            Vector3 anglespeed = new Vector3((float)anglespeedx, (float)anglespeedy, (float)anglespeedz);
            
            double velocityx = Convert.ToDouble((string)jo["velocityx"]);
            double velocityy = Convert.ToDouble((string)jo["velocityy"]);
            double velocityz = Convert.ToDouble((string)jo["velocityz"]);
            Vector3 velocity = new Vector3((float)velocityx, (float)velocityy, (float)velocityz);
            
            double aspeedx = Convert.ToDouble((string)jo["velocityx"]);
            double aspeedy = Convert.ToDouble((string)jo["velocityy"]);
            double aspeedz = Convert.ToDouble((string)jo["velocityz"]);
            Vector3 aspeed = new Vector3((float)aspeedx, (float)aspeedy, (float)aspeedz);
      
            double Aanglespeedx = Convert.ToDouble((string)jo["Aanglespeedx"]);
            double Aanglespeedy = Convert.ToDouble((string)jo["Aanglespeedy"]);
            double Aanglespeedz = Convert.ToDouble((string)jo["Aanglespeedz"]);
            Vector3 Aanglespeed = new Vector3((float)Aanglespeedx, (float)Aanglespeedy, (float)Aanglespeedz);
           
            
            if (_baseSyncObject != null)
            {
                baseSyncObjectPhyObj _obj = _baseSyncObject as baseSyncObjectPhyObj;
                _obj.setFrame( pos,rot,  velocity,anglespeed,aspeed,anglespeed, jiangetime);
            }


        }



    }

}

