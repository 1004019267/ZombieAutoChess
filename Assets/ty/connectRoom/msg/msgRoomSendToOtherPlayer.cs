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
    //发送给除增加的其他玩家
    class msgRoomSendToOtherPlayer : baseRoomMsg
    {
        static msgRoomSendToOtherPlayer g_msglogin;
        public static msgRoomSendToOtherPlayer getMe()
        {
            if (msgRoomSendToOtherPlayer.g_msglogin == null)
            {
                msgRoomSendToOtherPlayer.g_msglogin = new msgRoomSendToOtherPlayer();
            }
            return msgRoomSendToOtherPlayer.g_msglogin;
        }

        public msgRoomSendToOtherPlayer()
        {
            m_e_type = e_baseMsg_room.e_basemsg_sendtoOtherPlayer;
        }

 
        //返回房间信息
        public override void handle(JObject jo)
        {
            //收到别人的信息
            JObject joss = JObject.Parse(jo["data"].ToString());
            tyRoommsgMgr.getMe().userhandle(joss);
        }

        public virtual bool sendto(JObject staff)
        {
          
          //  JObject staff = new JObject();
            //staff.Add(new JProperty( "type"   ,  m_e_type));                 // 
            //staff.Add(new JProperty( "srcid" ,  tyConofig.g_ty_playid ) );


            JObject staff_send = new JObject();
            staff_send.Add(new JProperty("type", m_e_type));
            staff_send.Add(new JProperty("srcid", tyConofig.g_ty_playid)); // 
            staff_send.Add(new JProperty("data", staff.ToString()));

            return this.sendData(staff_send.ToString());
        }

    }
}
 