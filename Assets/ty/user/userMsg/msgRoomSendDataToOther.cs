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
   //客户端发送 游戏数据给服务器
    class msgRoomSendDataToOther : baseUserMsg
    {
        static msgRoomSendDataToOther g_msglogin;
        public static msgRoomSendDataToOther getMe()
        {
            if (msgRoomSendDataToOther.g_msglogin == null)
            {
                msgRoomSendDataToOther.g_msglogin = new msgRoomSendDataToOther();
            }
            return msgRoomSendDataToOther.g_msglogin;
        }

        public msgRoomSendDataToOther()
        {
             m_e_type = e_baseMsg_user.e_basemsg_data;
        }


        //
        

        public virtual void sendto( string anydata)
        {
            JObject staff = new JObject();
            staff.Add(new JProperty(  "any"   ,  anydata));
            this.sendToServer( staff );
        }

    }

    }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    