// TODO: I copied this one straight from the implementation without
// reading it.
// Need to run through and translate and
// make sure I understand.
using System;
using System.Collections.Generic;

public class FractionalHex
{
  public readonly double q;
  public readonly double r;
  public readonly double s;

  public FractionalHex(double q, double r, double s)
  {
    this.q = q;
    this.r = r;
    this.s = s;
    if (Math.Round(q + r + s) != 0) throw new ArgumentException("q + r + s must be 0");
  }

  public Hex HexRound()
  {
    int qi = (int)(Math.Round(q));
    int ri = (int)(Math.Round(r));
    int si = (int)(Math.Round(s));
    double q_diff = Math.Abs(qi - q);
    double r_diff = Math.Abs(ri - r);
    double s_diff = Math.Abs(si - s);
    if (q_diff > r_diff && q_diff > s_diff)
    {
      qi = -ri - si;
    }
    else
        if (r_diff > s_diff)
    {
      ri = -qi - si;
    }
    else
    {
      si = -qi - ri;
    }
    return new Hex(qi, ri, si);
  }

  public FractionalHex HexLerp(FractionalHex b, double t)
  {
    return new FractionalHex(q * (1.0 - t) + b.q * t, r * (1.0 - t) + b.r * t, s * (1.0 - t) + b.s * t);
  }

  static public List<Hex> HexLinedraw(Hex a, Hex b)
  {
    int N = a.Distance(b);
    FractionalHex a_nudge = new FractionalHex(a.q + 1e-06, a.r + 1e-06, a.s - 2e-06);
    FractionalHex b_nudge = new FractionalHex(b.q + 1e-06, b.r + 1e-06, b.s - 2e-06);
    List<Hex> results = new List<Hex> { };
    double step = 1.0 / Math.Max(N, 1);
    for (int i = 0; i <= N; i++)
    {
      results.Add(a_nudge.HexLerp(b_nudge, step * i).HexRound());
    }
    return results;
  }

}