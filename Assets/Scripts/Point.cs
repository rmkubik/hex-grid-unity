public class Point
{
  public float x;
  public float y;

  public Point(float x, float y)
  {
    this.x = x;
    this.y = y;
  }

  public override string ToString()
  {
    return $"x:{x}, y:{y}";
  }
}