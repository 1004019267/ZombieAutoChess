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

 


    class msgServerRoomStartGame : baseServerMsg
    {
        static msgServerRoomStartGame g_msglogin;
        public static msgServerRoomStartGame getMe()
        {
            if (msgServerRoomStartGame.g_msglogin == null)
            {
                msgServerRoomStartGame.g_msglogin = new msgServerRoomStartGame();
            }
            return msgServerRoomStartGame.g_msglogin;
        }

        public msgServerRoomStartGame()
        {
            m_e_type = e_baseMsg_user.e_basemsg_startGame; //
 
        }


        //返回房间信息
        public override void handle(JObject jo)
        {
            //开始创建场景。
            logMgr.log("开始游戏");
            if( tyConofig.isRoomOwer() == true )
            {
                 msgRoomCreatescene.getMe().sendto( "shaungren" );
            }

        }

 

    }

}

