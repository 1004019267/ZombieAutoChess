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
   //客户端发送按键消息给其他玩家
    class msgRoomSendKeyToOther : baseUserMsg
    {
        static msgRoomSendKeyToOther g_msglogin;
        public static msgRoomSendKeyToOther getMe()
        {
            if (msgRoomSendKeyToOther.g_msglogin == null)
            {
                msgRoomSendKeyToOther.g_msglogin = new msgRoomSendKeyToOther();
            }
            return msgRoomSendKeyToOther.g_msglogin;
        }

        public msgRoomSendKeyToOther()
        {
             m_e_type = e_baseMsg_user.e_basemsg_key;
        }


        // 
        

        public virtual void sendto( int key)
        {
            JObject staff = new JObject();
            staff.Add( new JProperty(  "key" ,  key));
            this.sentToOther( staff );
        }

    }

    }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    