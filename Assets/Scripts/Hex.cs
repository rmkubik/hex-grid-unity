using System;
using System.Collections.Generic;
using UnityEngine;

public class Hex : IEquatable<Hex>
{
  public int q;
  public int r;
  public int s
  {
    get => -q - r;
  }

  // s param is derived from q and r
  // so we can ignore it.
  public Hex(int q, int r, int s = 0)
  {
    this.q = q;
    this.r = r;

    Debug.Assert(this.q + this.r + this.s == 0, $"q:{this.q}, r:{this.r}, s:{this.s} do not make a valid Hex. They should total to 0.");
  }

  public override bool Equals(System.Object obj)
  {
    Hex hex = (Hex)obj;
    return this.Equals(hex);
  }

  public bool Equals(Hex hex)
  {
    return (q == hex.q) && (r == hex.r);
  }

  public override int GetHashCode()
  {
    // This is a "pairing function" used to encode the values of 
    // q and r into a single integer. 23, 31, and 37 are prime.
    // This ensures that every value of q and r will produce
    // the same integer.
    return 23 + 31 * q + 37 * r;
  }

  public override string ToString()
  {
    return "(" + q + ";" + r + ")";
  }

  public Hex Add(Hex other)
  {
    return new Hex(q + other.q, r + other.r);
  }

  public Hex Subtract(Hex other)
  {
    return new Hex(q - other.q, r - other.r);
  }

  public Hex Multiply(int k)
  {
    return new Hex(q * k, r * k);
  }

  public int Length()
  {
    return (Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2;
  }

  public int Distance(Hex other)
  {
    return Subtract(other).Length();
  }

  static public List<Hex> directions = new List<Hex> { new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1) };

  static public Hex Direction(int direction)
  {
    return Hex.directions[direction];
  }

  public Hex Neighbor(int direction)
  {
    return Add(Hex.Direction(direction));
  }

  public List<Hex> Neighbors()
  {
    var neighbors = new List<Hex>();

    for (int i = 0; i < directions.Count; i += 1)
    {
      neighbors.Add(Neighbor(i));
    }

    return neighbors;
  }
}