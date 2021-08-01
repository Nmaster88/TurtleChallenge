using System;
using System.Collections.Generic;
using System.IO;
using TurtleChallenge.Models;

namespace TurtleChallenge.Services
{
    /// <summary>
    /// Responsible for reading  the information on both moves and settings file
    /// assigns both information to correspondent data structures
    /// </summary>
    public class FileReaderService
    {
        private static FileReaderService _fileReader;
        private FileReaderService() { }
        public static FileReaderService GetInstance()
        {
            return _fileReader ?? (_fileReader = new FileReaderService());
        }

        private const string movesPath = "../../../../TurtleChallenge.Console/Files/{fileName}.csv";
        private const string boardSettingsPath = "../../../../TurtleChallenge.Console/Files/{fileName}.csv";

        public Moves GetMovesSequences(string fileName)
        {
            Moves MovesSequences = Moves.GetInstance();
            var sequencesLines = File.ReadAllLines(movesPath.Replace("{fileName}",fileName));
            for (int i = 0; i < sequencesLines.Length; i++)
            {
                var moveSequence = sequencesLines[i].Split(",");
                MovesSequences.AddSequence(moveSequence);
            }

            return MovesSequences;
        }

        public Board GetBoardsettings(string fileName)
        {
            Board board = new Board();
            board.grid = new Grid();

            string[] boardSettings = File.ReadAllLines(boardSettingsPath.Replace("{fileName}", fileName));
            var board_grid_settings = boardSettings[0].Split(",");

            if(!int.TryParse(board_grid_settings[1], out int boardWidth))
            {
                throw new Exception();
            }
            if (!int.TryParse(board_grid_settings[2], out int boardHeight))
            {
                throw new Exception();
            }
            board.grid.width = boardWidth;
            board.grid.height = boardHeight;

            var board_turtle_settings = boardSettings[1].Split(",");
            Cell position = new Cell();
            if (!int.TryParse(board_turtle_settings[2], out int turtleXPos))
            {
                throw new Exception();
            }
            if (!int.TryParse(board_turtle_settings[4], out int turtleYPos))
            {
                throw new Exception();
            }
            position.x = turtleXPos;
            position.y = turtleYPos;

            board.turtle = Turtle.GetInstance(position, (Dir)Enum.Parse(typeof(Dir), board_turtle_settings[6], true));
            //board.turtle.Direction = (Dir)Enum.Parse(typeof(Dir), board_turtle_settings[6],true);

            var board_turtle_exit = boardSettings[2].Split(",");
            Cell exitPosition = new Cell();
            if (!int.TryParse(board_turtle_exit[2], out int exitXPos))
            {
                throw new Exception();
            }
            if (!int.TryParse(board_turtle_exit[4], out int exitYPos))
            {
                throw new Exception();
            }
            exitPosition.x = exitXPos;
            exitPosition.y = exitYPos;
            board.Exit = new Element();
            board.Exit.Position = exitPosition;

            board.Mines = new List<Element>();
            for(int i=3; i<boardSettings.Length; i++)
            {
                var board_mine = boardSettings[i].Split(",");
                Cell minePosition = new Cell();
                if (!int.TryParse(board_mine[2], out int mineXPosition))
                {
                    throw new Exception();
                }
                if (!int.TryParse(board_mine[4], out int mineYPosition))
                {
                    throw new Exception();
                }
                minePosition.x = mineXPosition;
                minePosition.y = mineYPosition;
                Element mine = new Element();
                mine.Position = minePosition;
                board.Mines.Add(mine);
            }

            return board;
        }
    }
}
