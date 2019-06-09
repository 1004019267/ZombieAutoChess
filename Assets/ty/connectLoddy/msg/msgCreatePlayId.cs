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
    class msgCreatePlayId : baseMsg
    {
        static msgCreatePlayId g_msglogin;
        public static msgCreatePlayId getMe()
        {
            if (msgCreatePlayId.g_msglogin == null)
            {
                msgCreatePlayId.g_msglogin = new msgCreatePlayId();
            }
            return msgCreatePlayId.g_msglogin;
        }

        public msgCreatePlayId()
        {
            m_e_type = e_baseMsg.e_createPlayId;
        }

        public override void handle( JObject jo )
        {
            string playid =   (string)jo["playid"];
            tyConofig.g_ty_playid = Convert.ToInt32( playid );
            logMgr.log("msgCreatePlayId ok！");
            msgLoddylogin.getMe().login();
        }

        public virtual void getPlayID()
        {
            JObject staff = new JObject();
            staff.Add(new JProperty("type", m_e_type));                 // 
            this.sendData( staff.ToString() );
        }
 

    }
  }
 