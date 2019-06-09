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


    class msgServerRoomSnycAllObj : baseServerMsg
    {
        static msgServerRoomSnycAllObj g_msglogin;
        public static msgServerRoomSnycAllObj getMe()
        {
            if (msgServerRoomSnycAllObj.g_msglogin == null)
            {
                msgServerRoomSnycAllObj.g_msglogin = new msgServerRoomSnycAllObj();
            }
            return msgServerRoomSnycAllObj.g_msglogin;
        }

        public msgServerRoomSnycAllObj()
        {
            m_e_type = e_baseMsg_user.e_basemsg_SnycAllObj; //
 
        }

        float m_sendtime = 0;
        public override void loop()
        {
            m_sendtime += Time.deltaTime;
            if(m_sendtime >0.3)
            {
                sendto();
                m_sendtime = 0;
            }

        }


        public virtual void sendto( )
        {
            //JObject staff = new JObject();
            //int i = 0;

            //staff.Add(new JProperty("size" , syncObjectMgr.getMe().m_roomObjMap.Count));
            //foreach (var item in syncObjectMgr.getMe().m_roomObjMap)
            //{
            //    staff.Add(new JProperty(""+ i++ , item.Value.m_objectId));
            //}

            //this.sentToOther(staff);
        }

    }

}

