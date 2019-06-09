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
    class msgRoomhreat : baseRoomMsg
    {
        static msgRoomhreat g_msglogin;
        public bool m_isGetServerID = false;
        public static msgRoomhreat getMe()
        {
            if (msgRoomhreat.g_msglogin == null)
            {
                msgRoomhreat.g_msglogin = new msgRoomhreat();
            }
            return msgRoomhreat.g_msglogin;
        }

        public msgRoomhreat()
        {
            m_e_type = e_baseMsg_room.e_basemsg_heart;
        }

        public override void handle( JObject jo )
        {
        }

        float m_timesend = 1;
        public override void loop()
        {
            m_timesend += Time.deltaTime;
            if (m_timesend > 1)
            {
                m_timesend = 0;
                send();
            }
        }
        public virtual void send()
        {
            JObject staff = new JObject();
            staff.Add(new JProperty("type", m_e_type));                    //等价于staff.Add("Name","Jack");  
                                                                           //  staff.Add(new JProperty("id", ServerMgr.getMe().m_ServerId));  //等价于staff.Add("Name","Jack");  
            this.sendData(staff.ToString());
        }



    }
}