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
   //发送按键消息给其他玩家
    class msgServerRoomSendDataToOther : baseServerMsg
    {
        static msgServerRoomSendDataToOther g_msglogin;
        public static msgServerRoomSendDataToOther getMe()
        {
            if (msgServerRoomSendDataToOther.g_msglogin == null)
            {
                msgServerRoomSendDataToOther.g_msglogin = new msgServerRoomSendDataToOther();
            }
            return msgServerRoomSendDataToOther.g_msglogin;
        }

        public msgServerRoomSendDataToOther()
        {
             m_e_type = e_baseMsg_user.e_basemsg_data;
        }


        //返回房间信息
        public override void handle(JObject jo)
        {

            //收到别人的信息
            string    d     =      (string)jo["d"];
           // logMgr.log(  "myid" + tyConofig.g_roomid +    "srcid" + srcid   +  "key" + key );
        }

  

    }

    }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    