using System.IO.Abstractions;
using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    /// <summary>
    /// Responsible for initializing needed services for the game.
    /// Starts the game and executes sequences.
    /// </summary>
    public class GameService
    {
        private FileReaderService _fileReaderService;
        private PrinterService _printerService;
        private TurtleObserverService _turtleOserverService;
        private TurtleObservable _turtleObservable;
        private Board _board;
        private Moves _moves;
        private int counter=0;
        private string _moveSequencesFileName = "moves";
        private string _boardSettingsFileName = "settings";
        private const string Sequence = "Sequence {number}";

        public GameService(string boardSettingsFileName, string moveSequencesFileName)
        {
            _boardSettingsFileName = boardSettingsFileName ?? boardSettingsFileName;
            _moveSequencesFileName = moveSequencesFileName ?? _moveSequencesFileName;
            InitializeSettings();
        }

        /// <summary>
        /// Initializes some services and the board of the game
        /// </summary>
        private void InitializeSettings()
        {
            FileSystem fileSytem = new FileSystem();

            _fileReaderService = FileReaderService.GetInstance(fileSytem);
            _printerService = PrinterService.GetInstance();

            _moves = _fileReaderService.GetMovesSequences(_moveSequencesFileName);

            _board = _fileReaderService.GetBoardsettings(_boardSettingsFileName);

            _turtleObservable = new TurtleObservable(_board.turtle);
            _turtleOserverService = new TurtleObserverService(_board, _printerService);
            _turtleOserverService.Subscribe(_turtleObservable);
        }

        /// <summary>
        /// Executes the game and does an iteration for each sequence
        /// </summary>
        public void ExecuteGame()
        {
            foreach(var movesSequence in _moves.GetSequences())
            {
                ResetToInitialValues();
                ExecuteSequence(movesSequence);
            }
        }

        /// <summary>
        /// Get sequence count text
        /// </summary>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        private string GetSequenceCountText(int sequenceNumber)
        {
            return Sequence.Replace("{number}", sequenceNumber.ToString());
        }

        /// <summary>
        /// Executes a sequence of the game
        /// </summary>
        /// <param name="movesSequence"></param>
        private void ExecuteSequence(string[] movesSequence)
        {
            _printerService.Print(GetSequenceCountText(++counter));

            for (int i = 0; i < movesSequence.Length; i++)
            {
                if (movesSequence[i] == "r") _board.turtle.Rotate();
                else if (movesSequence[i] == "m") _board.turtle.Move();

                _turtleObservable.TrackTurtle(_board.turtle);

                if (!_board.turtle.IsTurtleAlive())
                    return;
            }

            _board.turtle.DidNotFoundExit();
            _printerService.Print(_board.turtle.TurtleState.Text());
        }

        /// <summary>
        /// We want to reset the board to its initial state
        /// </summary>
        private void ResetToInitialValues()
        {
            //restart turtle to initial position
            _board.turtle.ResetTurtle();
        }
    }
}
