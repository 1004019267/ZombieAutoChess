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
    class msgServerRoomSendKeyToOther : baseServerMsg
    {
        static msgServerRoomSendKeyToOther g_msglogin;
        public static msgServerRoomSendKeyToOther getMe()
        {
            if (msgServerRoomSendKeyToOther.g_msglogin == null)
            {
                msgServerRoomSendKeyToOther.g_msglogin = new msgServerRoomSendKeyToOther();
            }
            return msgServerRoomSendKeyToOther.g_msglogin;
        }

        public msgServerRoomSendKeyToOther()
        {
             m_e_type = e_baseMsg_user.e_basemsg_key;
        }


        //返回房间信息
        public override void handle(JObject jo)
        {

            //收到别人的信息
            int    srcid   =      Convert.ToInt32( (string)jo["srcid"] );
            int    key     =      Convert.ToInt32((string)jo["key"] )   ;
            logMgr.log(  "myid" + tyConofig.g_roomid +    "srcid" + srcid   +  "key" + key );
        }

  

    }

    }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    