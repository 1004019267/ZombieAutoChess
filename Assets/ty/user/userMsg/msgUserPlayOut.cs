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
    class msgUserPlayOut : baseUserMsg
    {
        static msgUserPlayOut g_msglogin;
    
        public static msgUserPlayOut getMe()
        {
            if (msgUserPlayOut.g_msglogin == null)
            {
                msgUserPlayOut.g_msglogin = new msgUserPlayOut();
            }
            return msgUserPlayOut.g_msglogin;
        }

        public msgUserPlayOut()
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
 