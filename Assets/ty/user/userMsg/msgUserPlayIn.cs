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
    class msgUserPlayIn : baseUserMsg
    {
        static msgUserPlayIn g_msglogin;
    
        public static msgUserPlayIn getMe()
        {
            if (msgUserPlayIn.g_msglogin == null)
            {
                msgUserPlayIn.g_msglogin = new msgUserPlayIn();
            }
            return msgUserPlayIn.g_msglogin;
        }

        public msgUserPlayIn()
        {
            m_e_type = e_baseMsg_user.e_basemsg_playin;
        }


        //返回房间信息
        public override void handle( JObject jo )
        {
            string playidstr = (string)jo["playid"];
            int playid = Convert.ToInt32(playidstr);

        }

   

    }

}
 