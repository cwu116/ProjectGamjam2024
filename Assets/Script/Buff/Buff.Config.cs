using System;
using UnityEngine;

namespace Buff.Config
{
    public enum BuffType
    {
        /// <summary>
        /// 数值改变类 : 仅仅改变数值操作
        /// </summary>
        ValueChange,            // 数值改变           ValueChange:变量名,数值

        /// <summary>
        /// 状态类 : 添加多样化状态
        /// <param name="状态名">string, 状态名引用</param>
        /// <param name="持续回合">int, 持续回合数</param>
        /// <param name="回合开始执行">bool, true:回合开始执行/false:回合结束执行</param>
        /// <param name="其他参数">字符串列表, 用于接受状态的其他参数</param>
        /// </summary>
        State,                  // 添加状态           StatePush:状态名,持续回合,起始位置,其他参数
        
        
        /// <summary>
        /// 生成类 : 生成地块或预制体
        /// </summary>
        Create,                 // 生成实体           CreateEntity:实体类型,实体名

        /// <summary>
        /// 操作类 : 让角色执行一些操作
        /// <param name="操作函数">string, 必须是角色的方法</param>
        /// <param name="参数">字符串列表, 传参</param>
        /// </summary>
        Action,                 // 执行动作           Action:操作函数,参数
    }
    

}

