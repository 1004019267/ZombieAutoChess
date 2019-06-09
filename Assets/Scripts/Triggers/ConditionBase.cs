using UnityEngine;

namespace Triggers
{
    /// <summary>
    /// 触发器基类.提供触发功能.
    /// </summary>
    [RequireComponent(typeof(TriggerGameObject))]
    public abstract class ConditionBase : MonoBehaviour
    {
        public bool ignore;
        public ConditionType conditioType;
        public enum ConditionType
        {
            /// <summary>
            /// 当前条件符合,继续执行后面的条件.
            /// </summary>
            match,
            /// <summary>
            /// 立即触发成功.不再做后续的判断.
            /// </summary>
            triggerNow,
            /// <summary>
            /// T触发失败,不再做后续的判断.
            /// </summary>
            triggerLost,
//		/// <summary>
//		/// 触发成功.跳过指定数量的判断.继续判断.
//		/// </summary>
//		skipNum,
//		/// <summary>
//		/// 直接跳到指定索引的
//		/// </summary>
//		gotoTo
        }
        
        
        public abstract bool isMatch(GameObject col);
    }
}