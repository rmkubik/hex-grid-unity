class Location
{
  public int row;
  public int col;

  public Location(int row, int col)
  {
    this.row = row;
    this.col = col;
  }

  public override string ToString()
  {
    return $"row:{row}, col:{col}";
  }
}