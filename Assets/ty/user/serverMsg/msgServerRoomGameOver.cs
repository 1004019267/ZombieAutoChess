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
    class msgServerRoomGameOver : baseServerMsg
    {
        static msgServerRoomGameOver g_msglogin;

        public static msgServerRoomGameOver getMe()
        {
            if (msgServerRoomGameOver.g_msglogin == null)
            {
                msgServerRoomGameOver.g_msglogin = new msgServerRoomGameOver();
            }

            return msgServerRoomGameOver.g_msglogin;
        }

        public msgServerRoomGameOver()
        {
            m_e_type = e_baseMsg_user.e_basemsg_GameOver;
        }

        //
        public override void handle(JObject jo)
        {
            logMgr.log("zz游戏结束。");
           
        }

        public virtual void sendto(int id, bool iswin)
        {
            
            JObject staff = new JObject();
            staff.Add(new JProperty("playid", id));
            staff.Add(new JProperty("iswin", iswin));
            this.sendToALL(staff);
        }
    }
}