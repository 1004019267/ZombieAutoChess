using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ty
{
    enum e_baseMsg_room
    {
        e_basemsg_none = -1,
        e_basemsg_login = 0,
        e_basemsg_heart = 13, //
        e_basemsg_sendtoall = 14, //
        e_basemsg_sendtoplayerone= 15, //
        e_basemsg_getPlayControlSync = 16, //

        e_basemsg_playin  = 17, //
        e_basemsg_playout = 18, //

        e_basemsg_sendtoOtherPlayer = 19, //
        e_basemsg_ping = 20, //
    }


    class baseRoomMsg
    {
        public virtual void shutdown()
        {

        }
        public e_baseMsg_room m_e_type = e_baseMsg_room.e_basemsg_none;
        public baseRoomMsg()
        {

        }

        public virtual void handle(JObject jo)
        {

        }
        public virtual void loop()
        {

        }

        public virtual bool sendData(string data)
        {
           
           return tyConnectRoom.getMe().send( data );
        }



        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter(); formatter.Serialize(ms, obj); return ms.GetBuffer();
            }
        }

        /// <summary> 
        /// 将一个序列化后的byte[]数组还原         
        /// </summary>
        /// <param name="Bytes"></param>         
        /// <returns></returns> 
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter(); return formatter.Deserialize(ms);
            }
        }


    }
}