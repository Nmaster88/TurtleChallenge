using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO.Abstractions;
using TurtleChallenge.Models;
using TurtleChallenge.Services;

namespace TurtleChallenge.Tests
{
    [TestClass]
    public class FileReaderServiceTests
    {
        private const string settingsFile = "settings";
        private const string movesFile = "Moves";
        private Mock<IFileSystem> moqFileSystem = null;

        [TestMethod]
        public void GetMovesSequences_Reading()
        {
            moqFileSystem = new Mock<IFileSystem>();
            //setup
            FileReaderService _fileReaderService = FileReaderService.GetInstance(moqFileSystem.Object);
            setupFileMoves();

            Moves moves = Moves.GetInstance();
            moves.AddSequence(new string[] { "r", "r", "m", "m", "m", "r", "r", "r", "m", "m" });
            moves.AddSequence(new string[] { "r","m","m","r","m","m","r","r","r","m","m" });
            moves.AddSequence(new string[] { "r","r","m","m","m" });

            Assert.AreEqual(moves.GetSequences(), _fileReaderService.GetMovesSequences(movesFile).GetSequences());
        }

        [TestMethod]
        public void GetSettings_Reading()
        {
            moqFileSystem = new Mock<IFileSystem>();
            //setup
            FileReaderService _fileReaderService = FileReaderService.GetInstance(moqFileSystem.Object);
            setupFileSettings();

            Board board = new Board();

            Grid grid = new Grid();
            grid.width = 4;
            grid.height = 5;
            board.grid = grid;

            Element turtleElement = new Element();
            Cell turtleCell = new Cell();
            turtleCell.x = 1;
            turtleCell.y = 1;
            turtleElement.Position = turtleCell;
            Turtle turtle = Turtle.GetInstance(turtleCell, Dir.NORTH);

            board.turtle = turtle;

            List<Element> Mines = new List<Element>();

            Element mine1 = new Element();
            Cell mineCell1 = new Cell();
            mineCell1.x = 2;
            mineCell1.y = 2;
            mine1.Position = mineCell1;
            Mines.Add(mine1);
            Element mine2 = new Element();
            Cell mineCell2 = new Cell();
            mineCell2.x = 2;
            mineCell2.y = 2;
            mine2.Position = mineCell2;
            Mines.Add(mine2);
            Element mine3 = new Element();
            Cell mineCell3 = new Cell();
            mineCell3.x = 2;
            mineCell3.y = 2;
            mine2.Position = mineCell3;
            Mines.Add(mine3);

            board.Mines = Mines;

            Element Exit = new Element();
            Cell exitCell = new Cell();
            exitCell.x = 3;
            exitCell.y = 4;
            Exit.Position = exitCell;

            board.Exit = Exit;

            Assert.AreEqual(board.Exit.Position.x, _fileReaderService.GetBoardsettings(settingsFile).Exit.Position.x);
        }

        private void setupFileMoves()
        {
            moqFileSystem.Setup(f => f.File.ReadAllLines(movesFile))
                .Returns(new string[]
                { "r,r,m,m,m,r,r,r,m,m",
                  "r,m,m,r,m,m,r,r,r,m,m",
                  "r,r,m,m,m"
                });
        }

        private void setupFileSettings()
        {
            moqFileSystem.Setup(f => f.File.ReadAllLines("settingsFile"))
                .Returns(new string[] 
                { "Size,4,5,,,,",
                  "Starting position, x,1, y,1, dir, North",
                  "Exit point, x,3, y,4,,",
                  "Mine, x,2, y,2,,",
                  "Mine, x,2, y,3,,",
                  "Mine, x,3, y,3,,"
                });
        }
    }
}
