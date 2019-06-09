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
    //发送给特定玩家
    class msgRoomSendToPlayerOne : baseRoomMsg
    {
        static msgRoomSendToPlayerOne g_msglogin;
        public static msgRoomSendToPlayerOne getMe()
        {
            if (msgRoomSendToPlayerOne.g_msglogin == null)
            {
                msgRoomSendToPlayerOne.g_msglogin = new msgRoomSendToPlayerOne();
            }
            return msgRoomSendToPlayerOne.g_msglogin;
        }

        public msgRoomSendToPlayerOne()
        {
            m_e_type = e_baseMsg_room.e_basemsg_sendtoplayerone;
        }

        //返回房间信息
        public override void handle(JObject jo)
        {
            //收到别人的信息
            JObject joss = JObject.Parse(jo["data"].ToString());
            tyRoommsgMgr.getMe().userhandle(joss);
        }

        public virtual void sendto(int  tarid , JObject staff)
        {
           // 、、JObject staff = new JObject();
            //staff.Add(new JProperty( "type"   ,  m_e_type));                 // 
            //staff.Add(new JProperty( "srcid" ,  tyConofig.g_ty_playid ) );
            //staff.Add(new JProperty( "tarid"  , tarid )  );



            JObject staff_send = new JObject();
            staff_send.Add(new JProperty("type", m_e_type));
            staff_send.Add(new JProperty("srcid", tyConofig.g_ty_playid)); // 
            staff_send.Add(new JProperty("tarid", tarid)); // 
            staff_send.Add(new JProperty("data", staff.ToString()));
            //staff.Add(new JProperty( "data"   , data));
            this.sendData(staff_send.ToString());
        }
    }

}
 