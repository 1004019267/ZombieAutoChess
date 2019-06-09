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
    //

 


    class msgServerRoomCreatescene : baseServerMsg
    {
        static msgServerRoomCreatescene g_msglogin;
        public static msgServerRoomCreatescene getMe()
        {
            if (msgServerRoomCreatescene.g_msglogin == null)
            {
                msgServerRoomCreatescene.g_msglogin = new msgServerRoomCreatescene();
            }
            return msgServerRoomCreatescene.g_msglogin;
        }

        public msgServerRoomCreatescene()
        {
            m_e_type = e_baseMsg_user.e_basemsg_createscene; //
 
        }


        //返回房间信息
        public override void handle(JObject jo)
        {
            //开始创建场景。
            logMgr.log("开始创建场景");
            string scenename = (string)jo["scenename"] ;
            if( tyConofig.isRoomOwer() == true ) //
            {
                //create scene
            }
            else
            {
                //create scene 创建一个空的创建。


            }

        }

        public virtual void sendto(string scenename)
        {
            JObject staff = new JObject();
            staff.Add(new JProperty("scenename", scenename));
            this.sendToALL(staff);
        }

    }

}

