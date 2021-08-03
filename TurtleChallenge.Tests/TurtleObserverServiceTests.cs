using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions;
using TurtleChallenge.Models;
using TurtleChallenge.Services;

namespace TurtleChallenge.Tests
{
    [TestClass]
    public class TurtleObserverServiceTests
    {
        private const string settingsFile = "settings";
        private const string movesFile = "Moves";

        [TestMethod]
        public void GetNewTurtleState_IsOk_When_GameStarts()
        {
            FileSystem fileSystem = new FileSystem(); 
            //setup
            FileReaderService _fileReaderService = FileReaderService.GetInstance(fileSystem);
            Board _board = _fileReaderService.GetBoardsettings(settingsFile);
            Moves _moves = _fileReaderService.GetMovesSequences(movesFile);
            PrinterService _printerService = PrinterService.GetInstance();
            TurtleObservable _turtleObservable = new TurtleObservable(_board.turtle);
            TurtleObserverService _turtleOserverService = new TurtleObserverService(_board, _printerService);
            _turtleOserverService.Subscribe(_turtleObservable);
            //TODO
        }
    }
}
