using UnityEngine;

//Œﬁ”√
public class GameObjectAgent : MonoBehaviour
    {
        public GameObject agnent;

        public virtual GameObject getAgent( object requester)
        {
            return agnent;
        }


        public static GameObject getAgentGameObject(object requester, GameObject obj,bool returnself)
        {
            if (obj == null)
            {
                return null;
            }
            GameObjectAgent a = obj.GetComponent<GameObjectAgent>();
            if (a == null)
            {
                return returnself ? obj : null;
            }
            else
            {
                GameObject Agent = a.getAgent(requester);
                if (Agent == null && returnself)
                {
                    return obj;
                }
                return Agent;
            }
        }

        public static GameObject getAgentGameObject(object requester, GameObject obj)
        {
            return getAgentGameObject(requester, obj, true);
        }

        public static Transform getAgentTransform(object requester, GameObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return getAgentGameObject(requester, obj).transform;
        }
        
    }
