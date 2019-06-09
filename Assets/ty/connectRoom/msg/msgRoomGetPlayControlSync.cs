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
    //主要玩家同步者id
    class msgRoomGetPlayControlSync : baseRoomMsg
    {
        static msgRoomGetPlayControlSync g_msglogin;
        public static msgRoomGetPlayControlSync getMe()
        {
            if (msgRoomGetPlayControlSync.g_msglogin == null)
            {
                msgRoomGetPlayControlSync.g_msglogin = new msgRoomGetPlayControlSync();
            }
            return msgRoomGetPlayControlSync.g_msglogin;
        }
        public msgRoomGetPlayControlSync()
        {
            m_e_type = e_baseMsg_room.e_basemsg_getPlayControlSync;
        }
        //返回房间信息
        public override void handle( JObject jo )
        {

            string errormsg = (string)jo["errormsg"];
            if (errormsg != "" && errormsg != null)
            {
                logMgr.log(errormsg);
                return;
            }

            string  syncplayid_str = (string)jo["syncplayid"];

            tyConofig.g_syncplayid  = Convert.ToInt32( syncplayid_str );
            //获得房主id
            logMgr.log( "获得房主id：" + tyConofig.g_syncplayid );
            
        }
        public virtual void getPlayControlSync()
        {
            JObject staff = new JObject();
            staff.Add( new JProperty( "type"  , m_e_type ) );                 // 
            this.sendData(staff.ToString());
        }
    }

}
 