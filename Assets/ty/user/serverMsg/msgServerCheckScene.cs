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
    //检查场景是否切换成功
    class msgServerCheckScene : baseServerMsg
    {
        static msgServerCheckScene g_msglogin;
    
        public static msgServerCheckScene getMe()
        {
            if (msgServerCheckScene.g_msglogin == null)
            {
                msgServerCheckScene.g_msglogin = new msgServerCheckScene();
            }
            return msgServerCheckScene.g_msglogin;
        }

        public msgServerCheckScene()
        {
            m_e_type = e_baseMsg_user.e_basemsg_checkScene;
        }


         Dictionary<int, bool> flagmap = new Dictionary<int, bool>();
        public delegate void Initevent();//切换场景结束后的游戏初始化
        public Initevent InitFun;
        public override void shutdown()
        {
              flagmap = new Dictionary<int, bool>();
        }

        //private bool isflag = false;

        public override void handle( JObject jo )
        {
            bool flag = (bool) jo["flag"];
            int playid = (int) jo["playid"];
            Debug.Log("playerid" + playid);
            Debug.Log("flag" + flag);
            //if (isflag)
            //{
            //    return;
            //}
            //isflag = flag;
            //if (getMe().InitFun != null)
            //{
            //    getMe().InitFun();
            //    getMe().InitFun = null;
            //}
            flagmap.Remove(playid);
            flagmap.Add(playid, flag);
        }

        
        public virtual void sendto(int playid,bool flag )
        {
            if (tyConofig.isRoomOwer() == false)
                return;
            JObject staff = new JObject();
            staff.Add(new JProperty("flag", flag));
            staff.Add(new JProperty("playid", playid));
            this.sendToALL(staff);
        }
  

    }

}
 