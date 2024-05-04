using Game.Model;

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

        public HexCell currentGird;





        void RegisterEvents()
        {
            EventSystem.Register<OnMouseRightClick>(Undo);
            EventSystem.Register<UndoEvent>(v => { _playerActionModel.AddOperation(v.undoOperation); });
        }

        private void Undo(OnMouseRightClick obj)
        {
            _playerActionModel.UndoOperation();
        }
    }
}
