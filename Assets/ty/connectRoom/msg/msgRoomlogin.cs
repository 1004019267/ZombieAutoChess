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
    class msgRoomlogin : baseRoomMsg
    {
        static msgRoomlogin g_msglogin;
        public bool m_islogin = false;

        public static msgRoomlogin getMe()
        {
            if (msgRoomlogin.g_msglogin == null)
            {
                msgRoomlogin.g_msglogin = new msgRoomlogin();
            }
            return msgRoomlogin.g_msglogin;
        }

        public msgRoomlogin()
        {
            m_e_type = e_baseMsg_room.e_basemsg_login;
        }


        //返回房间信息
        public override void handle(JObject jo)
        {
            string errormsg = (string)jo["errormsg"];
            if( errormsg != "" && errormsg != null)
            {
                logMgr.log(errormsg);
                tyInterface.getMe().onjointRoomServerError(errormsg);
             
                return ;
            }
            msgRoomGetPlayControlSync.getMe().getPlayControlSync();
            m_islogin = true;
            logMgr.log("登陆room服务器成功！");
            tyInterface.getMe().onjointRoomServerOk(jo);
        }

        public virtual void login()
        {
            m_islogin = false;
            JObject staff = new JObject();
            staff.Add(new JProperty( "type"   ,  m_e_type));                 // 
            staff.Add(new JProperty( "roomid" ,  tyConofig.g_roomid) );
            staff.Add(new JProperty( "playid" ,  tyConofig.g_ty_playid ) );
            staff.Add(new JProperty("playsize", tyConofig.g_playsize));
            
            logMgr.log("登录房间：" + staff.ToString());

         
            this.sendData(staff.ToString());

            msgRoomPing.getMe().ping();
        }






    }

    }
 