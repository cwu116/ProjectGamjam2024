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
            EventSystem.Register<OnPotionClick>(v => { _playerActionModel.CurrentPotion = v.potion; Cursor.instance.Show(); });
            EventSystem.Register<UsePotionEvent>(v => {_playerActionModel.CurrentPotion = null; GameBody.GetSystem<MapSystem>().ClearHighlightCells(); GameBody.GetSystem<MapSystem>().HighLightCells(Player.instance.MoveTimes-1 > 0 ? 1 : 0); }); 
        }

        private void Undo(OnMouseRightClick obj)
        {
            _playerActionModel.UndoOperation();
        }
    }
}
