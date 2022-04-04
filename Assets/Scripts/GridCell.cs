class GridCell<T>
{
  public Location location;

  public T value;

  public GridCell(Location location, T value)
  {
    this.location = location;
    this.value = value;
  }
}