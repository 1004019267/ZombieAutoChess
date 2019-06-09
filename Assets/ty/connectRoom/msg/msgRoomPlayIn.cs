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
    //玩家进入房间
    class msgRoomPlayIn : baseRoomMsg
    {
        static msgRoomPlayIn g_msglogin;
    
        public static msgRoomPlayIn getMe()
        {
            if (msgRoomPlayIn.g_msglogin == null)
            {
                msgRoomPlayIn.g_msglogin = new msgRoomPlayIn();
            }
            return msgRoomPlayIn.g_msglogin;
        }

        public msgRoomPlayIn()
        {
            m_e_type = e_baseMsg_room.e_basemsg_playin;
        }


        //返回房间信息
        public override void handle( JObject jo )
        {
            string playidstr = (string)jo["playid"];
            int playid = Convert.ToInt32(playidstr);
            roomPlayerMgr.getMe().createPlayer( playid );

            if (tyConofig.isRoomOwer() == true)
            {
                msgServerUserPlayIn.getMe().handle(jo);
            }
            else
            {
                msgUserPlayIn.getMe().handle(jo);
            }
        }

        public virtual void getPlayControlSync()
        {
            
            JObject staff = new JObject();
            staff.Add( new JProperty( "type"  , m_e_type ) );                 // 
            this.sendData(staff.ToString());
        }

    }

}
 