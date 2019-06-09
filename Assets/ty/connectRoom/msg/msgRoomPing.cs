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
    class msgRoomPing : baseRoomMsg
    {
        static msgRoomPing g_msglogin;
        public static msgRoomPing getMe()
        {
            if (msgRoomPing.g_msglogin == null)
            {
                msgRoomPing.g_msglogin = new msgRoomPing();
            }
            return msgRoomPing.g_msglogin;
        }

        public msgRoomPing()
        {
            m_e_type = e_baseMsg_room.e_basemsg_ping;
        }


        float m_start;
        //返回房间信息
        public override void handle(JObject jo)
        {
            logMgr.log("ping time:" + (m_time_all - m_start) * 1000);
        }

        float m_time_all = 0;
        //

        float m_sendtime = 0;
        public override void loop()
        {
            m_time_all += Time.fixedDeltaTime;

            //m_sendtime += Time.fixedDeltaTime;
            //if(m_sendtime > 3)
            //{
            //    ping();
            //    m_sendtime = 0;
            //}
        }

        public virtual void ping()
        {
            return;
            m_start = m_time_all;
            JObject staff = new JObject();
            staff.Add(new JProperty("type", m_e_type));                 // 
            staff.Add(new JProperty("playid", tyConofig.g_ty_playid));
            this.sendData(staff.ToString());
        }






    }

}
