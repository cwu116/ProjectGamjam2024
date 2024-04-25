
using System;

namespace Game.System
{
    //public partial class EnemyBehaviourSystem:BaseSystem
    //{
    //    private void RegisterEvents()
    //    {
    //        EventSystem.Register<AfterEnemyTurnBeginEvent>(TriggerEnemiesAction);
    //        EventSystem.Register<EnemyActionComplete>(OnEnemyAcionComplete);
    //    }

    //    int completeConter = 0;

    //    public EnemyBehaviourSystem()
    //    {
    //    }

    //    private void OnEnemyAcionComplete(EnemyActionComplete obj)
    //    {
    //       if(!obj.enemy.isDead)
    //        {
    //            completeConter++;
    //        }
    //        if(completeConter>=enemies.Count)
    //        {
    //            EventSystem.Send<EnemyTurnEndTrigger>();
    //            completeConter = 0;
    //        }
    //    }

    //    /// <summary>
    //    /// 触发敌人逻辑
    //    /// </summary>
    //    /// <param name="obj">玩家位置信息</param>
    //    private void TriggerEnemiesAction(AfterEnemyTurnBeginEvent obj)
    //    {
    //       foreach(var enemy in enemies)
    //        {
    //            ChekoutExitsPlayer(enemy, obj.playerPos);
    //        }
    //    }
    //}
}
