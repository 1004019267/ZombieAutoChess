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
    class msgServerUserPlayOut : baseServerMsg
    {
        static msgServerUserPlayOut g_msglogin;
    
        public static msgServerUserPlayOut getMe()
        {
            if (msgServerUserPlayOut.g_msglogin == null)
            {
                msgServerUserPlayOut.g_msglogin = new msgServerUserPlayOut();
            }
            return msgServerUserPlayOut.g_msglogin;
        }

        public msgServerUserPlayOut()
        {
            m_e_type = e_baseMsg_user.e_basemsg_playout;
        }


        //返回房间信息
        public override void handle( JObject jo )
        {
            string playidstr = (string)jo["playid"];
            int playid = Convert.ToInt32(playidstr);
            //logMgr.log();
         
        }

  

    }

}
 