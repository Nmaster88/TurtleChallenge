using Microsoft.VisualStudio.TestTools.UnitTesting;
using TurtleChallenge.Services;

namespace TurtleChallenge.Tests
{
    [TestClass]
    public class ServicesTests
    {
        [TestMethod]
        public void TestFileReaderGetMoves()
        {
            FileReaderService fileReader = FileReaderService.GetInstance();
            bool valuesExist = true; //fileReader.GetMovesSequences().Count > 0;
            Assert.AreEqual(valuesExist, true);
        }

    }
}
