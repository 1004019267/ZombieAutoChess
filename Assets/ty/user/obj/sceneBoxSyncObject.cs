using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ty
{
  

    class sceneBoxSyncObject : baseSyncObject
    {

 
        public override  void   Start()
        {
            m_objType = objType.e_scenebox;
            base.Start();
        }


        

    }
}
