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
    //游戏结束
    class msgRoomGameOver : baseUserMsg
    {
        static msgRoomGameOver g_msglogin;
        public static msgRoomGameOver getMe()
        {
            if (msgRoomGameOver.g_msglogin == null)
            {
                msgRoomGameOver.g_msglogin = new msgRoomGameOver();
            }
            return msgRoomGameOver.g_msglogin;
        }

        public msgRoomGameOver()
        {
            m_e_type = e_baseMsg_user.e_basemsg_GameOver;
        }
        //
        public override void handle(JObject jo)
        {
            logMgr.log("游戏结束。");
            int playid = (int) jo["playid"];
            bool win = (bool) jo["iswin"];
           
        }

       

    }

}

