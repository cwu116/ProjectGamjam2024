using UnityEngine;
using Game.Model;
using System.Collections.Generic;

namespace Game.System
{
    public partial class EnemyBehaviourSystem : BaseSystem
    {
        ////通过敌人列表调用敌人行为
        List<Enemy> enemies = new List<Enemy>();
        public override void InitSystem()
        {
            RegisterEvents();
        }





        private void ChekoutExistPlayer(Enemy enemy, Vector2 playerPos)
        {
            //if (/*chekout is true*/)//chekout have player in area of vigilance//警戒范围有没有仇恨目标
            //{
            //    ChekoutEnoughEnergy(enemy, playerPos);
            //}
            //else//随机移动一格(巡逻)
            //{
            //    GoOnPatrol(enemy);
            //}
        }

        private void GoOnPatrol(Enemy enemy)//巡逻
        {
            TriggerBuffs(enemy);
        }

        private void TriggerBuffs(Enemy enemy)//触发敌人身上的buff
        {
            //触发buff
            if (enemy.isDead)
            {
                //发送死亡事件或者buff系统本身处理(敌人数量需要--)
            }
            EventSystem.Send<EnemyActionComplete>();//该单位回合结束事件
        }

        private void ChekoutEnoughEnergy(Enemy enemy, Vector2 playerPos)//检测有没有行动点
        {
            //if (/*chekout is true*/)
            //{
            //    CheckoutSkillExistPlayer(enemy, playerPos);
            //}
            //else
            //    TriggerBuffs(enemy);
        }

        private void CheckoutSkillExistPlayer(Enemy enemy, Vector2 playerPos)//检测仇恨目标是否在技能范围内
        {
            //if (/*chekout is true*/)
            //{
            //    //skillTrigger//maybe await animation
            //}
            //else
            //{
            //    //向目标方向移动
            //}
            //ChekoutEnoughEnergy(enemy, playerPos);
        }
    }
}

