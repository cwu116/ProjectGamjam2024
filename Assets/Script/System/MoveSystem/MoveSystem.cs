using Game.System;
using JetBrains.Annotations;
using RedBjorn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.System
{
    public class MoveSystem : BaseSystem
    {
        public void Move(GameObject gameObject, Vector3 target)//控制对象
        {
            int Move;//移动力
            int[,] a;//怪物当前坐标
            int[,] n;//目标坐标
            int[][,] c;//位移路径(位移路径)
            if (gameObject.tag.Equals("Player"))//玩家移动逻辑
            {

                return;
            }
            else if (gameObject.tag.Equals("Enemy"))//怪物移动AI
            {
                return;
            }
        }
        void Run_AI(GameObject gameObject)
        {
            MapSystem mapSystem = new MapSystem();
        }
        public void Run_Move(Vector2 Xy, int Move)//获取最短路径坐标表
        {

        }
        public override void InitSystem()
        {

        }
    }
}

