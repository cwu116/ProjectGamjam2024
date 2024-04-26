using System.Collections.Generic;

namespace Script.Buff
{
    public class BuffComponent
    {
        
        // Dictionary<>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">怪物、玩家基础数值类</param>
        public BuffComponent(StateUnit entity)
        {
            
        }

        public void UseBuffGameStart()
        {
            
        }

        public void AddBuff()
        {
            
        }

        public void RemoveBuff()
        {
            
        }
    }

    public class StateUnit
    {
        private int baseValue;

        private int changeValue;

        public StateUnit(int baseValue)
        {
            this.baseValue = baseValue;
        }
        
        public void AddValue(int change)
        {
            
        }

        public void RemoveChange()
        {
            
        }

        public static implicit operator int(StateUnit unit)
        {
            return unit.baseValue + unit.changeValue;
        }

        public override string ToString()
        {
            return (baseValue + changeValue).ToString();
        }
        
    }
}