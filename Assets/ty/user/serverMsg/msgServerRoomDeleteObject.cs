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



    class msgServerRoomDeleteObject : baseServerMsg
    {
        static msgServerRoomDeleteObject g_msglogin;
        public static msgServerRoomDeleteObject getMe()
        {
            if (msgServerRoomDeleteObject.g_msglogin == null)
            {
                msgServerRoomDeleteObject.g_msglogin = new msgServerRoomDeleteObject();
            }
            return msgServerRoomDeleteObject.g_msglogin;
        }

        public msgServerRoomDeleteObject()
        {
            m_e_type = e_baseMsg_user.e_basemsg_deleteObj;
        }




        public virtual void sendto(int id   )
        {
            if (tyConofig.isRoomOwer() == false)
                return;
            JObject staff = new JObject();
            staff.Add(new JProperty("objid", id));
            this.sentToOther(staff);
        }



    }

}

