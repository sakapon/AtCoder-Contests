using System;
using System.Collections.Generic;
using System.Text;

struct RInt
{
	public static RInt Zero { get; } = new RInt(new int[0]);

	const int D9 = 1000000000;
	static readonly int[] Digits = InitDigits();
	static int[] InitDigits()
	{
		var d = new int[10];
		d[0] = 1;
		for (int i = 0; i < 9; ++i) d[i + 1] = d[i] * 10;
		return d;
	}

	public int[] C;
	public bool IsNegative;
	public RInt(int[] c, bool neg = false) { C = c; IsNegative = neg; }

	public static implicit operator RInt(string v) => Parse(v);
	public static RInt Parse(string s)
	{
		if (s == "" || s == "0") return Zero;

		var neg = s[0] == '-';
		var length = (s.Length + (neg ? 7 : 8)) / 9;

		var c = new int[length];
		for (int i = neg ? 1 : 0, ri = s.Length - 1 - i; i < s.Length; ++i, --ri)
			c[ri / 9] += Digits[ri % 9] * (s[i] - '0');
		return new RInt(c, neg);
	}

	public override string ToString()
	{
		if (C.Length == 0) return "0";

		var sb = new StringBuilder();
		if (IsNegative) sb.Append('-');
		sb.Append(C[C.Length - 1]);
		for (int i = C.Length - 2; i >= 0; --i) sb.Append(C[i].ToString("D9"));
		return sb.ToString();
	}

	public static RInt operator -(RInt v) => v.C.Length == 0 ? Zero : new RInt(v.C, !v.IsNegative);
	public static RInt operator -(RInt v1, RInt v2) => v1 + (-v2);
	public static RInt operator +(RInt v1, RInt v2)
	{
		if (v1.IsNegative == v2.IsNegative)
		{
			var c = new int[Math.Max(v1.C.Length, v2.C.Length) + 1];
			for (int i = 0; i < v1.C.Length; ++i) c[i] = v1.C[i];
			for (int i = 0; i < v2.C.Length; ++i) c[i] += v2.C[i];

			for (int i = 0; i < c.Length; ++i)
			{
				if (c[i] < D9) continue;
				c[i] -= D9;
				c[i + 1]++;
			}

			if (c[c.Length - 1] == 0) Array.Resize(ref c, c.Length - 1);
			return new RInt(c, v1.IsNegative);
		}
		else
		{
			if (v1.C.Length < v2.C.Length) { var t = v1; v1 = v2; v2 = t; }
			var neg = v1.IsNegative;

			var c = new List<int>(v1.C);
			for (int i = 0; i < v2.C.Length; ++i) c[i] -= v2.C[i];

			while (c.Count > 0 && c[c.Count - 1] == 0) c.RemoveAt(c.Count - 1);
			if (c.Count == 0) return Zero;

			if (c[c.Count - 1] < 0)
			{
				neg = !neg;
				for (int i = 0; i < c.Count; i++) c[i] = -c[i];
			}

			for (int i = 0; i < c.Count; i++)
			{
				if (c[i] >= 0) continue;
				c[i] += D9;
				c[i + 1]--;
			}

			while (c.Count > 0 && c[c.Count - 1] == 0) c.RemoveAt(c.Count - 1);
			return new RInt(c.ToArray(), neg);
		}
	}
}
