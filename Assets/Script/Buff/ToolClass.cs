using System;
using System.Collections.Generic;
using UnityEngine;

namespace Buff.Tool
{
    // 读表数据结构体
    [Serializable]
    public struct Potion
    {
        public string id;
        public string name;                    // 药水名
        public string description;             // 药水描述
        public List<string> containBuffs;      // 药剂buff集
    }
    public class PotionObj 
    {
        public Potion info { get; set; }
    }
    
    public class Param
    {
        private object _value;

        public Param(object value)
        {
            _value = value;
        }

        public static implicit operator string(Param param)
        {
            return param.ToString();
        }
        
        public static implicit operator int(Param param)
        {
            return int.Parse(param);
        }
    }

    public class ParamList
    {
        private List<Param> paramList;

        public ParamList(List<Param> outer = null)
        {
            if (outer == null)
            {
                paramList = new List<Param>();
            }
            else
            {
                paramList = outer;
            }
        }
        public ParamList(List<string> outer)
        {
            foreach (var item in outer)
            {
                paramList.Add(new Param(item));
            }
        }

        void Add(object obj)
        {
            
        }
        
        public static implicit operator Param[](ParamList param)
        {
            return param.paramList.ToArray();
        }
    }

    public class ValueInt
    {
        private int baseValue;
        private int changeValue;
        

        public ValueInt(int baseValue)
        {
            this.baseValue = baseValue;
        }
        
        public void AddValue(int Value, bool isChange = false)
        {
            if (!isChange)
            {
                changeValue += Value;
            }
            else
            {
                changeValue = Value;
            }
        }

        public void RemoveChange()
        {
            changeValue = 0;
        }

        public static implicit operator int(ValueInt unit)
        {
            return unit.baseValue + unit.changeValue;
        }

        public override string ToString()
        {
            return (baseValue + changeValue).ToString();
        }
        
    }
}
