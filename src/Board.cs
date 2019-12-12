using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace battleships
{
  class Board
  {
    static int size = 10;
    static int attempsPerShip = 100;

    public static (Result, int[,]) PlaceShip(int[,] board, int player, Ship ship)
    {
      var originalBoard = board;
      board = (int[,])board.Clone();

      var restrictedBoard = (int[,])board.Clone(); ;
      for (int y = 0; y < restrictedBoard.GetLength(0); y++)
        for (int x = 0; x < restrictedBoard.GetLength(1); x++)
          if (board[y, x] == player)
            for (int yi = -1; yi <= 1; yi++)
              for (int xi = -1; xi <= 1; xi++)
              {
                var xr = Math.Min(Math.Max(x + xi, 0), size - 1);
                var yr = Math.Min(Math.Max(y + yi, 0), size - 1);
                restrictedBoard[yr, xr] = 1;
              }


      for (int i = 0; i < ship.length; i++)
      {
        int x = ship.x + (ship.direction == 0 ? i : 0);
        int y = ship.y + (ship.direction == 1 ? i : 0);

        if (x < 0 || x > size - 1 || y < 0 || y > size - 1 || board[y, x] == player || restrictedBoard[y, x] == 1)
          return (Result.Failure, originalBoard);

        board[y, x] = board[y, x] == 0 ? player : 3;
      }

      return (Result.Success, board);
    }

    public static Ship[] GenerateShips()
    {
      List<Ship> ships = new List<Ship>();

      int max = 4;
      for (int i = 0; i < max; i++)
        for (int j = max - i; j > 0; j--)
        {
          int length = i + 1;
          ships.Add(new Ship(length));
        }

      return ((IEnumerable<Ship>)ships).Reverse().ToArray();
    }

    public static int[,] PlaceShipsOneByOne(int player, Ship[] allShips, Ship[] shipsStack, int[,] originBoard = null, bool insideRecursion = false)
    {
      if (originBoard == null)
        originBoard = new int[size, size];

      Debug.Print("New deepness {0}/{1}", shipsStack.Length, allShips.Length);
      int c = 0;
      while (true)
      {
        var board = (int[,])originBoard.Clone();
        c++;

        if (shipsStack.Length == 0)
          return board;

        Ship ship = shipsStack.First();

        Func<int, int, int, (Result, int[,])> tryPlace = (x, y, direction) =>
          PlaceShip(board, player, new Ship(ship.length, x, y, direction));

        var rnd = new Random();
        Func<int, (Result, int[,])> tryPlaceLimited = null;
        tryPlaceLimited = (attemps) =>
        {
          if (attemps == 0)
            return (Result.Failure, board);

          (Result result1, int[,]) t = tryPlace(rnd.Next(size), rnd.Next(size), rnd.Next(2));
          if (t.result1 == Result.Success)
            return t;

          return tryPlaceLimited(attemps - 1);
        };

        Result result;
        (result, board) = tryPlaceLimited(attempsPerShip);

        if (result == Result.Success)
        {
          Debug.Print(string.Format("Attemps {0}", c));
          Debug.Print(BoardToString(board));
          int[,] maybeABoard = PlaceShipsOneByOne(player, allShips, shipsStack.Skip(1).ToArray(), board, true);
          if (maybeABoard != null)
            return maybeABoard;
        }

        if (insideRecursion)
          return null;


        shipsStack = allShips;
        originBoard = new int[size, size];
      }
    }

    public static string BoardToString(int[,] board)
    {
      int rowLength = board.GetLength(0);
      int colLength = board.GetLength(1);

      string s = "";
      for (int i = 0; i < rowLength; i++)
      {
        for (int j = 0; j < colLength; j++)
        {
          s += string.Format("{0} ", board[i, j]);
        }
        s += "\n";
      }

      return s;
    }
  }
}