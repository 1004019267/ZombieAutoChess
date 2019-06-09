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
    class msgLoddyhreat : baseMsg
    {
        static msgLoddyhreat g_msglogin;
        public bool m_isGetServerID = false;
        public static msgLoddyhreat getMe()
        {
            if (msgLoddyhreat.g_msglogin == null)
            {
                msgLoddyhreat.g_msglogin = new msgLoddyhreat();
            }
            return msgLoddyhreat.g_msglogin;
        }

        public msgLoddyhreat()
        {
            m_e_type = e_baseMsg.e_basemsg_heart;
        }

        public override void handle(JObject jo)
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
            staff.Add(new JProperty("type", m_e_type));         //等价于staff.Add("Name","Jack");     //  staff.Add(new JProperty("id", ServerMgr.getMe().m_ServerId));  //等价于staff.Add("Name","Jack");  
            this.sendData(staff.ToString());
        }
    }

}
 