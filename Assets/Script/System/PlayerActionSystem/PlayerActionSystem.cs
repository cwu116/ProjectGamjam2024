using Game.Model;

namespace Game.System
{
    interface ICanUndo
    {
        void Undo();
    }
    public class PlayerActionSystem : BaseSystem
    {
        //private PlayerActionModel _playerActionModel;
        public override void InitSystem()
        {
            //_playerActionModel = GameBody.GetModel<PlayerActionModel>();
            RegisterEvents();
        }




        void RegisterEvents()
        {
            EventSystem.Register<OnMouseRightClick>(Undo);
            EventSystem.Register<UndoEvent>(v => { /*_PlayerActionModel.AddOperation(v.undo);*/});
        }

        private void Undo(OnMouseRightClick obj)
        {
            //_playerActionModel.UndoOperation();
        }
    }
}
