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
    class msgServerPlayerSyncObject : baseServerMsg
    {
        static msgServerPlayerSyncObject g_msglogin;
        public static msgServerPlayerSyncObject getMe()
        {
            if (msgServerPlayerSyncObject.g_msglogin == null)
            {
                msgServerPlayerSyncObject.g_msglogin = new msgServerPlayerSyncObject();
            }
            return msgServerPlayerSyncObject.g_msglogin;
        }

        public msgServerPlayerSyncObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_playerSyncObject;
        }

        
        // 
        public override void handle(JObject jo)
        {
            int playid = Convert.ToInt32((string)jo["playid"]);
            

            msgRoomCreateObject.getMe().handle(jo);
            int srcid = Convert.ToInt32((string)jo["srcid"]);
            int objid = Convert.ToInt32((string)jo["objid"]);
            double x = Convert.ToDouble((string)jo["x"]);
            double y = Convert.ToDouble((string)jo["y"]);
            double z = Convert.ToDouble((string)jo["z"]);
            Vector3 pos = new Vector3((float)x, (float)y, (float)z);
              logMgr.log("servermyid" + tyConofig.g_roomid + "srcid" + srcid + "objid" + objid);
         

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
            if (_baseSyncObject != null)
            {
                _baseSyncObject.setFrame( pos,rot,  scale, jiangetime);
            }
        }

        public virtual void sendto( float jiangetime,int id,Vector3 pos ,Quaternion qua, Vector3 scale, objType type ,int playid)
        {

            if (id == 0)
                return;
            JObject staff = new JObject();
            staff.Add(new JProperty("objid", id));
            staff.Add(new JProperty("x", pos.x));
            staff.Add(new JProperty("y", pos.y));
            staff.Add(new JProperty("z", pos.z));

            staff.Add(new JProperty("sx", scale.x));
            staff.Add(new JProperty("sy", scale.y));
            staff.Add(new JProperty("sz", scale.z));

            staff.Add(new JProperty("qx", qua.x));
            staff.Add(new JProperty("qy", qua.y));
            staff.Add(new JProperty("qz", qua.z));
            staff.Add(new JProperty("qw", qua.w));
            staff.Add(new JProperty("jg", jiangetime));
           // staff.Add(new JProperty("now", _sendttnowServer));


           staff.Add(new JProperty("objType", type));
           staff.Add(new JProperty("playid", playid));

            //logMgr.log("sync pos:" + staff.ToString());

            this.sentToOther(staff);
        }

    }

}

