using NUnit.Framework;
using battleships;

namespace battleshipsTest
{
  [TestFixture]
  public class GameTestFixture
  {
    [Test, Timeout(2000)]
    public void PlaceShipsOneByOneTest()
    {
      Ship[] ships = Board.GenerateShips();
      int[,] board = Board.PlaceShipsOneByOne(1, ships, ships);
    }
  }
}