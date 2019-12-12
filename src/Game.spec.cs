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
      Ship[] ships = Game.GenerateShips();
      int[,] board = Game.PlaceShipsOneByOne(1, ships, ships);
    }
  }
}