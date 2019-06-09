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
    class tyRoommsgMgr
    {
        static tyRoommsgMgr g_msgMgr;
        public static tyRoommsgMgr getMe()
        {
            if (tyRoommsgMgr.g_msgMgr == null)
            {
                tyRoommsgMgr.g_msgMgr = new tyRoommsgMgr();
                tyRoommsgMgr.g_msgMgr.init();
            }
            return tyRoommsgMgr.g_msgMgr;
        }

        static gameMap<e_baseMsg_room, baseRoomMsg> m_baseMsgMap = new gameMap<e_baseMsg_room, baseRoomMsg>();


        static gameMap<e_baseMsg_user, baseUserMsg> m_baseMsgUserMap = new gameMap< e_baseMsg_user ,  baseUserMsg>();

        static gameMap<e_baseMsg_user, baseServerMsg> m_baseMsgServerMap = new gameMap<e_baseMsg_user, baseServerMsg>();
        public void init()
        {

            this.addMsg( msgRoomhreat.getMe());
            this.addMsg( msgRoomlogin.getMe());
            this.addMsg( msgRoomGetPlayControlSync.getMe());
            this.addMsg( msgRoomSendToAll.getMe());
            this.addMsg( msgRoomSendToPlayerOne.getMe());
            this.addMsg( msgRoomPlayIn.getMe()  );
            this.addMsg( msgRoomPlayOut.getMe() );
            this.addMsg( msgRoomSendToOtherPlayer.getMe());
            this.addMsg(msgRoomPing.getMe());
        }

       public void shutdown()
        {
            for (int i = 0; i < m_baseMsgMap.Count; i++)
            {
                m_baseMsgMap.getDataByIndex(i).shutdown();
            }

            for (int i = 0; i < m_baseMsgUserMap.Count; i++)
            {
                m_baseMsgUserMap.getDataByIndex(i).shutdown();
            }

            for (int i = 0; i < m_baseMsgServerMap.Count; i++)
            {
                m_baseMsgServerMap.getDataByIndex(i).shutdown();
            }
        }

        public void addMsg(baseRoomMsg data)
        {
            m_baseMsgMap.AddOrReplace(data.m_e_type, data);
        }
        public void addUserMsg(baseUserMsg data)
        {
            m_baseMsgUserMap.AddOrReplace(data.m_e_type, data);
        }
        
     public void addServerMsg(baseServerMsg data)
        {
            m_baseMsgServerMap.AddOrReplace(data.m_e_type, data);
        }
        public void loop( )
        {
            for (int i = 0; i < m_baseMsgMap.Count; i++)
            {
                m_baseMsgMap.getDataByIndex(i).loop();
            }

            for (int i = 0; i < m_baseMsgUserMap.Count; i++)
            {
                m_baseMsgUserMap.getDataByIndex(i).loop();
            }

            for (int i = 0; i < m_baseMsgServerMap.Count; i++)
            {
                m_baseMsgServerMap.getDataByIndex(i).loop();
            }

            
        }

        public void handle(string jsonText)
        {
            JObject jo = JObject.Parse(jsonText);
            e_baseMsg_room type = (e_baseMsg_room)Convert.ToInt32(jo["type"].ToString());
            baseRoomMsg _baseMsg;
            if (m_baseMsgMap.TryGetValue(type, out _baseMsg))
            {
                _baseMsg.handle(jo);
            }
            else
            {
                logMgr.log("消息没有注册！：" + jsonText);
            }
        }


        public void userhandle(JObject jo)
        {
            //  JObject jo = JObject.Parse(jsonText);
            e_baseMsg_user type = (e_baseMsg_user)Convert.ToInt32(jo["tp"].ToString());
            if(tyConofig.isRoomOwer()==true)
            {
                baseServerMsg _baseMsg;
                if (m_baseMsgServerMap.TryGetValue(type, out _baseMsg))
                {
                    _baseMsg.handle(jo);
                }
                else
                {
                    logMgr.log(" userhandle server 消息没有注册！：");
                }
            }
            else
            {

                    baseUserMsg _baseMsg;
                    if (m_baseMsgUserMap.TryGetValue(type, out _baseMsg))
                    {
                        _baseMsg.handle(jo);
                    }
                    else
                    {
                        logMgr.log(" userhandle   消息没有注册！：");
                    }
            }





        }



    }


}