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
    class msgLoddyjointRoom : baseMsg
    {
        static msgLoddyjointRoom g_msglogin;
        public bool m_islogin = false;
        public bool m_islogining = false;

        public static msgLoddyjointRoom getMe()
        {
            if (msgLoddyjointRoom.g_msglogin == null)
            {
                msgLoddyjointRoom.g_msglogin = new msgLoddyjointRoom();
            }
            return msgLoddyjointRoom.g_msglogin;
        }

        public msgLoddyjointRoom()
        {
            m_e_type = e_baseMsg.e_basemsg_jointRoom;
        }

        public override void handle(JObject jo)
        {

            string errormsg = (string)jo["errormsg"];

            m_islogin    = true;

            m_islogining = false;

            if (errormsg != "" && errormsg != null)
            {
                tyInterface.getMe().onjointRoomServerError(errormsg);
                logMgr.log( "匹配失败" + errormsg );
            }
            else
            {
               // tyInterface.getMe().onjointRoomOk(  jo  );

                string roomid = (string)jo["roomid"];
                string Ip = (string)jo["Ip"];
                string Port = (string)jo["Port"];
                string createRoom = (string)jo["createRoom"];

                tyConofig.g_ty_roomServerIp = Ip;
                tyConofig.g_ty_roomServerPort = Convert.ToInt32(Port);
                tyConofig.g_roomid = Convert.ToInt32(roomid);

                tyConnectRoom.getMe().connent(tyConofig.g_ty_roomServerIp, tyConofig.g_ty_roomServerPort);

                logMgr.log("匹配成功 roomid:" + tyConofig.g_roomid);
            }
            //ok
      

        }

        public virtual void send()
        {

            if (m_islogining == true)
                return;

            m_islogining = true;

            JObject staff = new JObject();

            staff.Add(new JProperty("type", m_e_type));                 // 
            staff.Add(new JProperty("playid", tyConofig.g_ty_playid));
            staff.Add(new JProperty("roomdata", ""));// 房间数据
   
            staff.Add(new JProperty("playsize", tyConofig.g_playsize));

            logMgr.log("开始匹配" + staff.ToString());

            this.sendData(staff.ToString());
        }








    }
}