using Game.Model;
using System;

namespace Game.System
{
    interface ICanUndo
    {
        void Undo();
    }
    public class PlayerActionSystem : BaseSystem
    {
        private PlayerActionModel _playerActionModel;
        public override void InitSystem()
        {
            _playerActionModel = GameBody.GetModel<PlayerActionModel>();
            RegisterEvents();
        }





        void RegisterEvents()
        {
            EventSystem.Register<OnMouseRightClick>(Undo);
            EventSystem.Register<UndoEvent>(v => { _playerActionModel.AddOperation(v.undoOperation); });
            EventSystem.Register<OnPotionClick>(v => _playerActionModel.currentPotion = v.potion);
            EventSystem.Register<UsePotionEvent>(v => _playerActionModel.currentPotion = null);
        }

        private void Undo(OnMouseRightClick obj)
        {
            _playerActionModel.UndoOperation();
        }
    }
}
