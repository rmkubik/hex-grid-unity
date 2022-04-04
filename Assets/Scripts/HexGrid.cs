using UnityEngine;

class HexGrid<T>
{
  private int width;
  private int height;

  public delegate void GridCellCallback(GridCell<T> cell, GridCell<T>[] cells);
  public delegate T InitCallback(Location location);

  private GridCell<T>[] cells;

  public HexGrid(int width, int height, InitCallback initCallback)
  {
    this.width = width;
    this.height = height;

    this.cells = new GridCell<T>[this.width * this.height];

    Init(initCallback);
  }

  public void ForEach(GridCellCallback callback)
  {
    foreach (var cell in cells)
    {
      callback(cell, cells);
    }
  }

  private void Init(InitCallback callback)
  {
    for (int row = 0; row < height; row += 1)
    {
      for (int col = 0; col < width; col += 1)
      {
        Location location = new Location(row, col);
        int index = row * width + col;

        T value = callback(location);
        GridCell<T> cell = new GridCell<T>(location, value);

        this.cells[index] = cell;
      }
    }
  }

  void Fill(T value)
  {
    for (int row = 0; row < height; row += 1)
    {
      for (int col = 0; col < width; col += 1)
      {
        Location location = new Location(row, col);
        int index = row * width + col;

        if (this.cells[index] == null)
        {
          this.cells[index] = new GridCell<T>(location, value);
        }

        this.cells[index].value = value;
      }
    }
  }
}