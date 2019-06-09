using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace ty
{
    class tyConofig
    {

        public static string g_ty_loddyServerIp = "47.104.86.155";  //游戏服务器ip
        public static int g_ty_loddyServerPort = 55003; //游戏服务器端口
        public static string g_ty_gameid = "loddygongzuoxibao";
      
        public static int g_playsize = 2;    //没局人数

        //一下参数不需要填写
        public static Dictionary<int, string> g_targetplayidmap = new Dictionary<int, string>(); //对手playid
        public static string g_ty_roomServerIp = "127.0.0.1";
        public static int    g_ty_roomServerPort = 55000;
        public static int   g_roomid = 10000;  
        public static int   g_ty_playid = -1; //玩家id
        public static int   g_syncplayid = -2; //房主id

        //是否是服务器
        public static bool isRoomOwer()
        {
            return g_syncplayid == g_ty_playid;
        }
        public static void reset()
        {
            g_syncplayid = -2; //房主id

        }

    }

}