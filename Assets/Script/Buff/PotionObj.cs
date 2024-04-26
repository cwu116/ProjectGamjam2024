using System;
using System.Collections.Generic;
using UnityEngine;

namespace Potion
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
}
