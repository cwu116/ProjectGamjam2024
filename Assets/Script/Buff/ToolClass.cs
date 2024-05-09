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

        public override string ToString()
        {
            return Convert.ToString(_value);
        }

        public int ToInt()
        {
            try
            {
                return int.Parse(_value.ToString());
            }
            catch
            {

            }
            return 0;
        }

        // 转类布尔
        public static implicit operator bool(Param param)
        {
            return param.ToInt() != 0;
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
            paramList = new List<Param>();
            foreach (var item in outer)
            {
                paramList.Add(new Param(item));
            }
        }

        void Add(Param obj)
        {
            paramList.Add(obj);
        }
        
        public static implicit operator Param[](ParamList param)
        {
            return param.paramList.ToArray();
        }
    }

    [Serializable]
    public class ValueInt
    {
        public string name = null;
        
        private int baseValue;
        private int changeValue;
        

        public int Base
        {
            get => baseValue;
        }

        public int Change
        {
            get => changeValue;
        }
        public ValueInt(int baseValue)
        {
            this.baseValue = baseValue;
        }
        
        public ValueInt(int baseValue, int outValue)
        {
            this.baseValue = baseValue;
            changeValue = outValue - this.baseValue;
        }
        
        public static ValueInt operator +(ValueInt left, int newValue)
        {
            left.AddValue(newValue);
            return left;
        }
        
        public static ValueInt operator &(ValueInt left, int right)
        {
            left = new ValueInt(left.baseValue, right);
            return left;
        }

        public void AddValue(int Value, bool isChange = false)
        {
            if (!isChange)
            {
                if (changeValue + baseValue + Value <= 0)
                {
                    changeValue = -baseValue;
                    return;
                }

                
                changeValue += Value;
            }
            else
            {
                changeValue = Value - baseValue;
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
        
        public static implicit operator bool(ValueInt unit)
        {
            return unit != 0;
        }
        
    }
}
