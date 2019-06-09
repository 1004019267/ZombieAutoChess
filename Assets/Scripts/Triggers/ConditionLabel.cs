using UnityEngine;

namespace Triggers
{
    /// <summary>
    /// 判断是不是拥有指定状态的对象.比如:名称,tag,碰撞层等信息.
    /// </summary>
    public class ConditionLabel : ConditionBase
    {

        public string label;
        public enum LabelType
        {
            name,
            tag,
            layerName,
            layerInt
        }

        public LabelType labelType;
        public override bool isMatch(GameObject col)
        {
            switch (labelType)
            {
                case LabelType.name:
                    return col.name == label;
                case LabelType.tag:
                    return col.CompareTag(label);
                case LabelType.layerName:
                    return col.layer == LayerMask.NameToLayer(label);
                case LabelType.layerInt:
                    return col.layer == int.Parse(label);
                default:
                    return false;
            }
        }
    }
}