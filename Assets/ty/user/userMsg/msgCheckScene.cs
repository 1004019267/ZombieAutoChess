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
    //玩家退出
    class msgCheckScene : baseUserMsg
    {
        static msgCheckScene g_msglogin;
    
        public static msgCheckScene getMe()
        {
            if (msgCheckScene.g_msglogin == null)
            {
                msgCheckScene.g_msglogin = new msgCheckScene();
            }
            return msgCheckScene.g_msglogin;
        }

        public msgCheckScene()
        {
            m_e_type = e_baseMsg_user.e_basemsg_checkScene;
        }

        private bool isflag = false;
        public override void handle( JObject jo )
        {
            bool flag = (bool) jo["flag"];
            int playid = (int) jo["playid"];
            Debug.Log("playerid" + playid);
            Debug.Log("flag" + flag);
            if (isflag)
            {
                return;
            }
            isflag = flag;
            if (msgServerCheckScene.getMe().InitFun != null)
            {
                msgServerCheckScene.getMe().InitFun();
                msgServerCheckScene.getMe().InitFun = null;
            }
         
        }

  

    }

}
 