using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    public class GameService
    {
        private FileReaderService _fileReaderService;
        private PrinterService _printerService;
        //private ObserveService _observeService;
        private TurtleObserverService _turtleOserverService;
        private Board _board;
        private Cell _turtlePositionStart;
        private Dir _turtleDirectionStart;
        private Moves _moves;
        private int counter=0;
        private string _moveSequencesFileName = "moves";
        private string _boardSettingsFileName = "settings";

        public GameService(string boardSettingsFileName, string moveSequencesFileName)
        {
            _boardSettingsFileName = boardSettingsFileName ?? boardSettingsFileName;
            _moveSequencesFileName = moveSequencesFileName ?? _moveSequencesFileName;
            Initialize();
        }
        private void Initialize()
        {
            _fileReaderService = FileReaderService.GetInstance();
            _printerService = PrinterService.GetInstance();

            _moves = _fileReaderService.GetMovesSequences(_moveSequencesFileName);

            _board = _fileReaderService.GetBoardsettings(_boardSettingsFileName);

            _turtlePositionStart = _board.turtle.Position;
            _turtleDirectionStart = _board.turtle.Direction;

            _turtleOserverService = new TurtleObserverService(_board, _printerService);
            _board.turtle.Attach(_turtleOserverService);
        }
        public void ExecuteGame()
        {
            foreach(var movesSequence in _moves.GetSequences())
            {
                //restart turtle to initial position
                _board.turtle.Position = _turtlePositionStart;
                _board.turtle.Direction = _turtleDirectionStart;
                _board.turtle.ChangeTurtleState(new TurtleOkState());
                _turtleOserverService.ResetObserver(_turtlePositionStart);
                ExecuteSequence(movesSequence);
            }
        }

        private void ExecuteSequence(string[] movesSequence)
        {
            _printerService.PrintSequence(++counter);
            for (int i = 0; i < movesSequence.Length; i++)
            {
                if (movesSequence[i] == "r") _board.turtle.Rotate();
                else if (movesSequence[i] == "m") _board.turtle.Move();

                if (!_turtleOserverService.IsAlive())
                    return;
            }

            _turtleOserverService.NoWayOut();
        }
    }
}
