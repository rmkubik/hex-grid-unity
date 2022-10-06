using System;
using System.Collections.Generic;
using UnityEngine;

class Layout
{
  public readonly Orientation orientation;
  public readonly Point size;
  public readonly Point origin;

  static public Orientation pointy = new Orientation((float)Math.Sqrt(3.0f), (float)Math.Sqrt(3.0f) / 2.0f, 0.0f, 3.0f / 2.0f, (float)Math.Sqrt(3.0f) / 3.0f, -1.0f / 3.0f, 0.0f, 2.0f / 3.0f, 0.5f);
  static public Orientation flat = new Orientation(3.0f / 2.0f, 0.0f, (float)Math.Sqrt(3.0f) / 2.0f, (float)Math.Sqrt(3.0f), 2.0f / 3.0f, 0.0f, -1.0f / 3.0f, (float)Math.Sqrt(3.0f) / 3.0f, 0.0f);

  public int pixelsPerUnit;

  public Layout(Orientation orientation, Point size, Point origin, int pixelsPerUnit)
  {
    this.orientation = orientation;
    this.size = size;
    this.origin = origin;
    this.pixelsPerUnit = pixelsPerUnit;
  }

  public Point HexToPixel(Hex hex)
  {
    float x = (orientation.f0 * hex.q + orientation.f1 * hex.r) * size.x;
    float y = (orientation.f2 * hex.q + orientation.f3 * hex.r) * size.y;

    return new Point(x + origin.x, y + origin.y);
  }

  public FractionalHex PixelToHex(Point pixel)
  {
    Point pt = new Point((pixel.x - origin.x) / size.x, (pixel.y - origin.y) / size.y);

    double q = orientation.b0 * pt.x + orientation.b1 * pt.y;
    double r = orientation.b2 * pt.x + orientation.b3 * pt.y;

    return new FractionalHex(q, r, -q - r);
  }

  public Vector3 HexToUnit(Hex hex)
  {
    var pixel = HexToPixel(hex);
    var pixelVector = new Vector3(pixel.x, pixel.y, 0);

    return pixelVector / pixelsPerUnit;
  }

  public Point HexCornerOffset(int corner)
  {
    double angle = 2.0 * Math.PI * (orientation.start_angle - corner) / 6.0;
    return new Point(
      size.x * (float)Math.Cos(angle),
      size.y * (float)Math.Sin(angle)
    );
  }

  public List<Vector3> PolygonCorners(Hex hex)
  {
    List<Vector3> corners = new List<Vector3>();

    Point center = HexToPixel(hex);
    for (int i = 0; i < 6; i++)
    {
      Point offset = HexCornerOffset(i);
      corners.Add(new Vector3(
        (center.x + offset.x) / pixelsPerUnit,
        (center.y + offset.y) / pixelsPerUnit,
        0
      ));
    }

    return corners;
  }
}