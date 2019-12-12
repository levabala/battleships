namespace battleships
{
  struct Ship
  {
    public int length, x, y, direction;
    public Ship(int length, int x = -1, int y = -1, int direction = -1)
    {
      this.length = length;
      this.x = x;
      this.y = y;
      this.direction = direction;
    }
  }
}