using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ty
{

    //游戏服务器逻辑
    class serverLogic
    {

        static serverLogic g_websocketMgr;
        public static serverLogic getMe()
        {
            if (serverLogic.g_websocketMgr == null)
            {
                serverLogic.g_websocketMgr = new serverLogic();
            }
            return serverLogic.g_websocketMgr;
        }


        //bool m_startgame = false;

        void init()
        {
        }
       public void loop()
       {
           if( tyConofig.isRoomOwer() == false)
           {
                return;
           }
        
       }
        //
        void startGame_loop()
        {
            //if (m_startgame == true)
            //{
            //    return;
            //}

            //if ( roomPlayerMgr.getMe().getPlayerCount() < tyConofig.g_playsize )
            //{
            //    return;
            //}

            //m_startgame = true; //开始游戏
            //msgRoomStartGame.getMe().sendto();
        }


        //
        void endGame()
        {

        }




    }
}


