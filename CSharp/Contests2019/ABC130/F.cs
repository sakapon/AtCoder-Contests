﻿using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	struct P
	{
		public double x, y;
		public string d;
	}

	struct V
	{
		public double x;
		public int d;
	}

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split()).Select(x => new P { x = double.Parse(x[0]), y = double.Parse(x[1]), d = x[2] }).ToArray();

		var rs = ps.Where(x => x.d == "R").ToArray();
		var ls = ps.Where(x => x.d == "L").ToArray();
		var us = ps.Where(x => x.d == "U").ToArray();
		var ds = ps.Where(x => x.d == "D").ToArray();

		var xs = new List<V>();
		Edge(xs, rs, p => p.x, 1);
		Edge(xs, us.Concat(ds).ToArray(), p => p.x, 0);
		Edge(xs, ls, p => p.x, -1);

		var ys = new List<V>();
		Edge(ys, us, p => p.y, 1);
		Edge(ys, rs.Concat(ls).ToArray(), p => p.y, 0);
		Edge(ys, ds, p => p.y, -1);

		var tx = GetTurnTimes(xs);
		var ty = GetTurnTimes(ys);

		Func<V, double, double> f = (v, t) => v.x + v.d * t;
		Func<double, double> rect = t => (xs.Max(v => f(v, t)) - xs.Min(v => f(v, t))) * (ys.Max(v => f(v, t)) - ys.Min(v => f(v, t)));
		Console.WriteLine(new[] { 0.0 }.Concat(tx).Concat(ty).Min(t => rect(t)));
	}

	static void Edge(List<V> vs, P[] ps, Func<P, double> toValue, int d)
	{
		if (ps.Length == 1)
		{
			vs.Add(new V { x = toValue(ps[0]), d = d });
		}
		else if (ps.Length > 1)
		{
			vs.Add(new V { x = ps.Max(toValue), d = d });
			vs.Add(new V { x = ps.Min(toValue), d = d });
		}
	}

	static double[] GetTurnTimes(List<V> vs) => vs.SelectMany(u => vs.Where(v => u.d != v.d).Select(v => (u.x - v.x) / (v.d - u.d))).Where(t => t >= 0).ToArray();
}
