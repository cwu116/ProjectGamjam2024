using UnityEngine;
using Game.Model;
using System.Collections.Generic;

namespace Game.System
{
    public partial class EnemyBehaviourSystem : BaseSystem
    {
        ////ͨ�������б���õ�����Ϊ
        List<Enemy> enemies = new List<Enemy>();
        public override void InitSystem()
        {
            RegisterEvents();
        }





        private void ChekoutExistPlayer(Enemy enemy, Vector2 playerPos)
        {
            //if (/*chekout is true*/)//chekout have player in area of vigilance//���䷶Χ��û�г��Ŀ��
            //{
            //    ChekoutEnoughEnergy(enemy, playerPos);
            //}
            //else//����ƶ�һ��(Ѳ��)
            //{
            //    GoOnPatrol(enemy);
            //}
        }

        private void GoOnPatrol(Enemy enemy)//Ѳ��
        {
            TriggerBuffs(enemy);
        }

        private void TriggerBuffs(Enemy enemy)//�����������ϵ�buff
        {
            //����buff
            if (enemy.isDead)
            {
                //���������¼�����buffϵͳ������(����������Ҫ--)
            }
            EventSystem.Send<EnemyActionComplete>();//�õ�λ�غϽ����¼�
        }

        private void ChekoutEnoughEnergy(Enemy enemy, Vector2 playerPos)//�����û���ж���
        {
            //if (/*chekout is true*/)
            //{
            //    CheckoutSkillExistPlayer(enemy, playerPos);
            //}
            //else
            //    TriggerBuffs(enemy);
        }

        private void CheckoutSkillExistPlayer(Enemy enemy, Vector2 playerPos)//�����Ŀ���Ƿ��ڼ��ܷ�Χ��
        {
            //if (/*chekout is true*/)
            //{
            //    //skillTrigger//maybe await animation
            //}
            //else
            //{
            //    //��Ŀ�귽���ƶ�
            //}
            //ChekoutEnoughEnergy(enemy, playerPos);
        }
    }
}

