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

    class tyLoddymsgMgr
    {
        static tyLoddymsgMgr g_msgMgr;
        public static tyLoddymsgMgr getMe()
        {
            if (tyLoddymsgMgr.g_msgMgr == null)
            {
                tyLoddymsgMgr.g_msgMgr = new tyLoddymsgMgr();
                tyLoddymsgMgr.g_msgMgr.init();
            }
            return tyLoddymsgMgr.g_msgMgr;
        }

        static gameMap<e_baseMsg, baseMsg> m_baseMsgMap = new gameMap<e_baseMsg, baseMsg>();

        public void init()
        {
            this.addMsg(msgLoddylogin.getMe());
            this.addMsg(msgLoddyjointRoom.getMe());
            this.addMsg(msgLoddyhreat.getMe());
            this.addMsg(msgCreatePlayId.getMe());
        }

        void shutdown()
        {

        }

        void addMsg(baseMsg data)
        {
            m_baseMsgMap.AddOrReplace(data.m_e_type, data);
        }


        public void loop()
        {
            for (int i = 0; i < m_baseMsgMap.Count; i++)
            {
                m_baseMsgMap.getDataByIndex( i ).loop();
            }
        }

        public void handle( string jsonText )
        {
            try
            {
                JObject jo = JObject.Parse(jsonText);
                e_baseMsg type = (e_baseMsg)Convert.ToInt32(jo["type"].ToString());
                baseMsg _baseMsg;
                if (m_baseMsgMap.TryGetValue(type, out _baseMsg))
                {
                    _baseMsg.handle(jo);
                }
                else
                {
                    logMgr.log( "消息没有注册！：" + jsonText );
                }
            }
            catch( Exception e )
            {
                logMgr.log("errormsg" + jsonText );
            }
  
        }

    }

}