
namespace Game.Model
{
    public class TurnModel : BaseModel
    {
        private TurnType _currentTurn;
        public TurnType CurrentTurn { get=>_currentTurn; set=>_currentTurn=value; }
        public override void InitModel()
        {
            _currentTurn = TurnType.None;
        }
    }
}

