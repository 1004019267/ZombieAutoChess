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
    class msgLoddylogin : baseMsg
    {
        static msgLoddylogin g_msglogin;
        public bool m_islogin = false;
        public bool m_islogining = false;
        public static msgLoddylogin getMe()
        {
            if (msgLoddylogin.g_msglogin == null)
            {
                msgLoddylogin.g_msglogin = new msgLoddylogin();
            }
            return msgLoddylogin.g_msglogin;
        }

        public msgLoddylogin()
        {
            m_e_type = e_baseMsg.e_basemsg_login;
        }

        public override void handle(JObject jo)
        {
            string errormsg =   (string)jo["errormsg"];
            if (errormsg != "" && errormsg != null)
            {
                tyInterface.getMe().onconnectLoddyError(errormsg);
                return;
            }
            m_islogin = true;
            tyInterface.getMe().onconnectLoddyOk("");
            logMgr.log("登陆大厅服务器成功！");
        }

        public virtual void login()
        {
            m_islogin = false;
            JObject staff = new JObject();
            staff.Add(new JProperty("type", m_e_type));                 // 
            staff.Add(new JProperty("gameid", tyConofig.g_ty_gameid));
            staff.Add(new JProperty("playid", tyConofig.g_ty_playid));
            logMgr.log(" login loddy" + staff.ToString());
            this.sendData(staff.ToString());
        }




    }
  }
 