using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;


namespace ty
{
    //发送给所有玩家
    class msgRoomSendToAll : baseRoomMsg
    {
        static msgRoomSendToAll g_msglogin;
        public static msgRoomSendToAll getMe()
        {
            if (msgRoomSendToAll.g_msglogin == null)
            {
                msgRoomSendToAll.g_msglogin = new msgRoomSendToAll();
            }
            return msgRoomSendToAll.g_msglogin;
        }

        public msgRoomSendToAll()
        {
            m_e_type = e_baseMsg_room.e_basemsg_sendtoall;
        }


        //
        public override void handle(JObject jo)
        {
            //
            // string data = (string)jo["data"];
            JObject joss = JObject.Parse(jo["data"].ToString());
            tyRoommsgMgr.getMe().userhandle(joss);

        }

        public virtual void sendto(JObject staff)
        {
            JObject staff_send = new JObject();
            staff_send.Add(new JProperty("type", m_e_type));                 // 
            staff_send.Add(new JProperty("data", staff.ToString()));
            this.sendData(staff_send.ToString());
        }

 

    }

}
