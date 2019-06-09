using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Internal;

namespace ty
{
     class msgServerPhysicSyncObj:baseServerMsg
    {
        static msgServerPhysicSyncObj g_msglogin;
    
        public static msgServerPhysicSyncObj getMe()
        {
            if (msgServerPhysicSyncObj.g_msglogin == null)
            {
                msgServerPhysicSyncObj.g_msglogin = new msgServerPhysicSyncObj();
            }
            return msgServerPhysicSyncObj.g_msglogin;
        }

        public msgServerPhysicSyncObj()
        {
            m_e_type = e_baseMsg_user.e_basemsg_physicobj;
        }



            
        public override void handle( JObject jo )
        {
           
        }

        
        public virtual void sendto(float jiangetime,int id,Vector3 pos ,Quaternion qua, Vector3 velocity,objType type,Vector3 scale = default(Vector3),
            Vector3 anglespeed = default(Vector3),
            Vector3 aspeed = default(Vector3),Vector3 aanglespeed = default(Vector3))
        {
            if (id == 0)
                return;
            if (tyConofig.isRoomOwer() == false)
            {
                return;
            }
            JObject staff = new JObject();
            staff.Add(new JProperty("objid", id));
            staff.Add(new JProperty("x", pos.x));
            staff.Add(new JProperty("y", pos.y));
            staff.Add(new JProperty("z", pos.z));

            if (scale == Vector3.zero)
            {
                scale = Vector3.one;
            }
            staff.Add(new JProperty("sx", scale.x));
            staff.Add(new JProperty("sy", scale.y));
            staff.Add(new JProperty("sz", scale.z));
            
           

            staff.Add(new JProperty("qx", qua.x));
            staff.Add(new JProperty("qy", qua.y));
            staff.Add(new JProperty("qz", qua.z));
            staff.Add(new JProperty("qw", qua.w));
            
            staff.Add(new JProperty("anglespeedx", anglespeed.x));
            staff.Add(new JProperty("anglespeedy", anglespeed.y));
            staff.Add(new JProperty("anglespeedz", anglespeed.z));
            
            staff.Add(new JProperty("velocityx", velocity.x));
            staff.Add(new JProperty("velocityy", velocity.y));
            staff.Add(new JProperty("velocityz", velocity.z));
            
            
            staff.Add(new JProperty("aspeedx", aspeed.x));
            staff.Add(new JProperty("aspeedy", aspeed.y));
            staff.Add(new JProperty("aspeedz", aspeed.z));
            
            
            staff.Add(new JProperty("Aanglespeedx", aanglespeed.x));
            staff.Add(new JProperty("Aanglespeedy", aanglespeed.y));
            staff.Add(new JProperty("Aanglespeedz", aanglespeed.z));
            
            
            staff.Add(new JProperty("jg", jiangetime));


            staff.Add(new JProperty("objType", type));

            //logMgr.log("sync pos:" + staff.ToString());

            this.sentToOther(staff);
        }
    }
}