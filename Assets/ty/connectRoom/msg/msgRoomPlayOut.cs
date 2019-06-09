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
    //玩家退出
    class msgRoomPlayOut : baseRoomMsg
    {
        static msgRoomPlayOut g_msglogin;
    
        public static msgRoomPlayOut getMe()
        {
            if (msgRoomPlayOut.g_msglogin == null)
            {
                msgRoomPlayOut.g_msglogin = new msgRoomPlayOut();
            }
            return msgRoomPlayOut.g_msglogin;
        }

        public msgRoomPlayOut()
        {
            m_e_type = e_baseMsg_room.e_basemsg_playout;
        }


        //返回房间信息
        public override void handle( JObject jo )
        {
            string playidstr = (string)jo["playid"];
            int playid = Convert.ToInt32(playidstr);
            //logMgr.log();
            roomPlayerMgr.getMe().deletePlayer(playid);

            if(tyConofig.isRoomOwer() == true)
            {
                msgServerUserPlayOut.getMe().handle(jo);
            }
            else
            {
                msgUserPlayOut.getMe().handle(jo);
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
 