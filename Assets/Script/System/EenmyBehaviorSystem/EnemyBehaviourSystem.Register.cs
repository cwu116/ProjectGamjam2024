
using System;
using UnityEngine;

namespace Game.System
{
    public partial class EnemyBehaviourSystem : BaseSystem
    {
        private void RegisterEvents()
        {
            EventSystem.Register<AfterEnemyTurnBeginEvent>(TriggerEnemiesAction);
            EventSystem.Register<EnemyActionComplete>(OnEnemyAcionComplete);
        }

        int completeConter = 0;


        private void OnEnemyAcionComplete(EnemyActionComplete obj)
        {
            if (!obj.enemy.isDead)
            {
                completeConter++;
            }
            if (completeConter >= enemies.Count)
            {
                EventSystem.Send<EnemyTurnEndTrigger>();
                completeConter = 0;
            }
        }

        /// <summary>
        /// 触发敌人逻辑
        /// </summary>
        /// <param name="obj">玩家位置信息</param>
        private void TriggerEnemiesAction(AfterEnemyTurnBeginEvent obj)
        {
            enemies.Clear();
            foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemies.Add(enemy.GetComponent<Enemy>());
            }
            foreach (var enemy in enemies)
            {
                //ChekoutExitsPlayer(enemy, obj.playerPos);
                GameBody.GetSystem<MoveSystem>().EnemyMoveTo(enemy.gameObject);
            }
        }
    }
}
