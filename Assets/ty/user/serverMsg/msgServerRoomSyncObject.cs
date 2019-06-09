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
    class msgServerRoomSyncObject : baseServerMsg
    {
        static msgServerRoomSyncObject g_msglogin;
        public static msgServerRoomSyncObject getMe()
        {
            if (msgServerRoomSyncObject.g_msglogin == null)
            {
                msgServerRoomSyncObject.g_msglogin = new msgServerRoomSyncObject();
            }
            return msgServerRoomSyncObject.g_msglogin;
        }

        public msgServerRoomSyncObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_SyncObject;
        }

        // 
        public override void handle(JObject jo)
        {
        }

        public virtual void sendto( float jiangetime,int id,Vector3 pos ,Quaternion qua, Vector3 scale, objType type )
        {

            if (id == 0)
                return;
            if (tyConofig.isRoomOwer() == false)
            {
                return;
            }
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

            //logMgr.log("sync pos:" + staff.ToString());

            this.sentToOther(staff);
        }

    }

}

