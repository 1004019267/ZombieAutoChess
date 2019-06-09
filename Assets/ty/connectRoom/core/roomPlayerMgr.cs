using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ty
{
    class roomPlayerMgr
    {
        static roomPlayerMgr g_msgMgr;
        public static roomPlayerMgr getMe()
        {
            if (roomPlayerMgr.g_msgMgr == null)
            {
                roomPlayerMgr.g_msgMgr = new roomPlayerMgr();
                roomPlayerMgr.g_msgMgr.init();
            }
            return roomPlayerMgr.g_msgMgr;
        }
        /// <summary>
        /// 房间服务器 玩家列表
        /// </summary>
        Dictionary< int , roomPlayer > m_roomObjMap = new Dictionary< int , roomPlayer >();

        public void init()
        {
        }

        public void shutdown()
        {
            m_roomObjMap = new Dictionary< int , roomPlayer >( );
        }

        public void loop()
        {
        }

        public void createPlayer( int playid )
        {
            if ( this.getPlayer( playid ) != null )
            {
                return;
            }

            roomPlayer _roomPlayer = new roomPlayer();

            _roomPlayer.m_playid   = playid;
            //
          
            m_roomObjMap.Add(_roomPlayer.m_playid, _roomPlayer);

            logMgr.log("玩家加入房间" + playid);
            logMgr.log("当前玩家数" + m_roomObjMap.Count);

            if (roomPlayerMgr.getMe().getPlayerCount() >= tyConofig.g_playsize)
            {
                //开始游戏
                msgRoomStartGame.getMe().handle(null);
                msgServerRoomStartGame.getMe().handle(null);
            }


            //m_roomObjMap.Add();
            int playidindex = 1;
            foreach(var dd in m_roomObjMap)
            {
                dd.Value.PlayerIndexId = playidindex;
                ++playidindex;

                logMgr.log("playidindex" + playidindex);
            }


        }




        public void deletePlayer(int playid)
        {
           
            m_roomObjMap.Remove(playid);

            logMgr.log("玩家退出房间" + playid  );
            logMgr.log("当前玩家数" + m_roomObjMap.Count);
        }
        public int getPlayerCount( )
        {

        return    m_roomObjMap.Count;
            
        }
        public roomPlayer getPlayer( int  _playid )
        {
            roomPlayer _roomPlayer = null;
            if ( m_roomObjMap.TryGetValue(_playid, out _roomPlayer) == true )
            {
                return _roomPlayer;
            }
            return null;
        }




    }
}
