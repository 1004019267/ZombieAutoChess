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

  
    class tyInterface
    {
        static tyInterface g_msgMgr;
        public static tyInterface getMe()
        {
            if (tyInterface.g_msgMgr == null)
            {
                tyInterface.g_msgMgr = new tyInterface();
            }
            return tyInterface.g_msgMgr;
        }

        //推出房间
        public void shtdown()
        {
            tyConnectLoddy.getMe().shutdown();
            roomPlayerMgr.getMe().shutdown();
            tyConnectRoom.getMe().shutdown();
            tyRoommsgMgr.getMe().shutdown();
        }
        public void loop(float t)
        {
            tyConnectLoddy.getMe().loop(t);
            tyConnectRoom.getMe().loop();
            roomPlayerMgr.getMe().loop();
        }

        //连接游戏大厅  连接成功 自动给玩家id （tyConofig.g_ty_playid） 赋值
        public void connectLoddy( string gameid, string ip, int port )
        {
            tyConofig.g_ty_gameid = gameid;
            tyConofig.g_ty_loddyServerIp = ip;
            tyConofig.g_ty_loddyServerPort = port;
            tyConnectLoddy.getMe().connent(tyConofig.g_ty_loddyServerIp, tyConofig.g_ty_loddyServerPort);
        }

        public void onconnectLoddyOk(string json)
        {
            logMgr.log("我的玩家id:"+tyConofig.g_ty_playid );


            //在这个写连接房间成功逻辑





        }
        public void onconnectLoddyError(string errormsg)
        {
            logMgr.log(errormsg);

            //
            //在这个写连接房间失败逻辑


        }


        public bool isConnectLoddy( )
        {
            return tyConnectLoddy.getMe().m_connent;
        }


        //匹配玩家玩游戏
        public bool jointRoom()
        {
            if (tyConnectRoom.getMe().m_connent == true || tyConnectRoom.getMe().m_connenting == true)
            {
                logMgr.log("连接房间中，或已经在房间中！");

                return false;
            }

            if ( isConnectLoddy() == false ) 
            {
                logMgr.log("请先连接大厅！");
                return false;
            }
            msgLoddyjointRoom.getMe().send();
            return true;
        }

      
        //匹配玩家成功
        public void onjointRoomServerOk(JObject jo)
        {
            logMgr.log(  "连接房间成功 房间id：" + tyConofig.g_roomid );


            //在这个写连接房间成功逻辑






            //

        }
        public void onjointRoomServerError(string json)
        {
            logMgr.log("连接房间失败 房间id：" + tyConofig.g_roomid);
            outRoom();

            //在这个写连接房间失败逻辑






            //



        }

        //退出房间 退出游戏
        public void outRoom()
        {
            tyConnectRoom.getMe().shutdown();
            roomPlayerMgr.getMe().shutdown();
            syncObjectMgr.getMe().shutdown();
            tyConofig.reset();
        }

        //注册玩家协议
        public  void regmsg(baseUserMsg data)
        {
            tyRoommsgMgr.getMe().addUserMsg(data);
        }

        //注册玩家服务器协议
        public void regservermsg(baseServerMsg data)
        {
            tyRoommsgMgr.getMe().addServerMsg(data);
        }





    }

}