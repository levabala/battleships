using System;
using System.Diagnostics;

namespace battleships
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Start");

      Ship[] ships = Game.GenerateShips();

      int[,] board = Game.PlaceShipsOneByOne(1, ships, ships);
      Console.WriteLine(Game.BoardToString(board));
    }
  }
}
