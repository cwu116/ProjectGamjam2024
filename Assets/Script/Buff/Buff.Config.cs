using UnityEngine;

namespace Buff.Config
{
    /// <summary>
    /// 代码枚举
    /// 对应代码名称，与Buff及Player属性名不同
    /// </summary>
    public enum CmdCode
    {
        Together = 0,
        
        // type = valueChange
        Damage,                 // 伤害               Damage:数值
        MoveRangeChange,        // 改变移动力          MoveRangeChange:数值
        MaxHpChange,            // 最大血量            MaxHpChange:数值
        MoveTimeChange,         // 改变行动点          MoveTimeChange:数值 
        SkillRangeChange,       // 改变技能范围        SkillRangeChange:数值
        DefenceChange,          // 改变防御力          DefenceChange:数值
        
        // type = state
        StatePush,              // 添加状态           StatePush:状态名,持续回合
        ClearState,             // 清除所有状态       ClearState
        
        // type = create
        CreateEntity,           // 生成实体           CreateEntity:实体名
        CreateScene             // 生成地块           CreateScene:地块名
        
        // ...
    }

}

