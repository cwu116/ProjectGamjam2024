
using System;
using UnityEngine;
using System.Threading.Tasks;

namespace Game.System
{
    public partial class EnemyBehaviourSystem : BaseSystem
    {
        private void RegisterEvents()
        {
            EventSystem.Register<AfterEnemyTurnBeginEvent>(TriggerEnemiesAction);
            EventSystem.Register<EnemyActionComplete>(OnEnemyAcionComplete);
            EventSystem.Register<EnemyDieEvent>(OnEnemyDie);
        }

        ~EnemyBehaviourSystem()
        {
            EventSystem.UnRegister<AfterEnemyTurnBeginEvent>(TriggerEnemiesAction);
            EventSystem.UnRegister<EnemyActionComplete>(OnEnemyAcionComplete);
            EventSystem.UnRegister<EnemyDieEvent>(OnEnemyDie);
        }

        private void OnEnemyDie(EnemyDieEvent obj)
        {
            enemies.Remove(obj.enemy as Enemy);
            GameObject.Destroy(obj.enemy.gameObject,0.5f);
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
        private async void TriggerEnemiesAction(AfterEnemyTurnBeginEvent obj)
        {
            enemies.Clear();
            foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemies.Add(enemy.GetComponent<Enemy>());
            }
            if(enemies.Count<=0)
                EventSystem.Send<EnemyTurnEndTrigger>();
            for (int i=0;i<enemies.Count;i++)
            {
                Enemy enemy = enemies[i];
                enemy.GetComponent<Enemy>().MoveTimes = new Buff.Tool.ValueInt(enemy.GetComponent<Enemy>().MaxMoveTimes);
                for (int j = 0; j < enemy.GetComponent<Enemy>().StepLength; j++)
                {
                    GameBody.GetSystem<MoveSystem>().EnemyMoveTo(enemy.gameObject);
                }
                EventSystem.Send<EnemyActionComplete>(new EnemyActionComplete() { enemy = enemy.GetComponent<Enemy>() });
            }
        }
    }
}
