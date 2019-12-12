using System;
using System.Diagnostics;

namespace battleships
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Start");

      Ship[] ships = Board.GenerateShips();

      int[,] board = Board.PlaceShipsOneByOne(1, ships, ships);
      Console.WriteLine(Board.BoardToString(board));
    }
  }
}
