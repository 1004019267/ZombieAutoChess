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
    //



    class msgServerRoomCreateObject : baseServerMsg
    {
        static msgServerRoomCreateObject g_msglogin;
        public static msgServerRoomCreateObject getMe()
        {
            if (msgServerRoomCreateObject.g_msglogin == null)
            {
                msgServerRoomCreateObject.g_msglogin = new msgServerRoomCreateObject();
            }
            return msgServerRoomCreateObject.g_msglogin;
        }

        public msgServerRoomCreateObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_CreateObject;
        }

        //int g_objectCount = 10000;
        public long createObjectId()
        {
            byte[] bytes = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(bytes, 0);
        }

        //收到
        public override void handle(JObject jo)
        {
        }

        public virtual void sendto(int id,Vector3 pos , Vector3 scale, Quaternion qua, objType type)
        {
         

            JObject staff = new JObject();
            staff.Add( new JProperty(  "objid", id) );
            staff.Add( new JProperty(  "x"  , pos.x )   );
            staff.Add(new JProperty(   "y", pos.y));
            staff.Add(new JProperty(   "z", pos.z));

            staff.Add(new JProperty("sx", scale.x));
            staff.Add(new JProperty("sy", scale.y));
            staff.Add(new JProperty("sz", scale.z));

            staff.Add(new JProperty("qx", qua.x));
            staff.Add(new JProperty("qy", qua.y));
            staff.Add(new JProperty("qz", qua.z));
            staff.Add(new JProperty("qw", qua.w));
            staff.Add(new JProperty("objType", type));
            this.sendToALL(staff);
        }

    }

}

