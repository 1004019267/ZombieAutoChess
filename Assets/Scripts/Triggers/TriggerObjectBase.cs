
using UnityEngine;

namespace Triggers
{
    /// <summary>
    /// 可以触发的游戏对象.需要添加很多的触发条件.然后再添加需要自行的Action.需要执行调用onTrigger()函数.
    /// </summary>
    public class TriggerObjectBase : MonoBehaviour
    {
        public TriggerMatchType matchType;
        public string ActionName;
        /// <summary>
        /// 是不是触发后,不再触发.除非被其它代码打开.
        /// </summary>
        public bool disableOnTrggier;
        public enum TriggerMatchType
        {
            all,
            some
        }

        private ConditionBase[] _all;
        private GameObject _lastTriggerobj;

        public GameObject lastTrggerObject()
        {
            return _lastTriggerobj;
        }
        private void OnEnable()
        {
            if (ActionName == "")
            {
                ActionName = null;
            }
            _all = gameObject.GetComponents<ConditionBase>();

        }

        protected bool Ontrigger(GameObject obj)
        {
            if (!enabled)
            {
                return false;
            }

            foreach (ConditionBase condition in _all)
            {
                if (condition.ignore)
                {
                    continue;
                }

                if (condition.isMatch(obj))
                {
                    if (condition.conditioType == ConditionBase.ConditionType.triggerNow)
                    {
                        break;
                    }
                }
                else
                {
                    if (matchType == TriggerMatchType.all || condition.conditioType == ConditionBase.ConditionType.triggerLost)
                    {
                        return false;
                    }
                }
                
                
            }
            doTrigger(obj);
            return true;
        }


        public void doTrigger(GameObject obj)
        {
            _lastTriggerobj = obj;
            ActionUtils.RunAction(gameObject, ActionName);
            if (disableOnTrggier)
            {
                enabled = false;
            }
        }
        

    }
}