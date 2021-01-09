using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<string>();
		var h = Read();
		var n = h[0];

		var v = new List<int>();
		var next = new List<int>();
		var f = CreateArray(n, -1);
		var l = CreateArray(n, -1);

		for (int k = 0; k < h[1]; k++)
		{
			var q = Read();
			if (q[0] == 0)
			{
				var t = q[1];
				var i = v.Count;
				v.Add(q[2]);
				next.Add(-1);
				if (l[t] != -1) next[l[t]] = i;
				if (f[t] == -1) f[t] = i;
				l[t] = i;
			}
			else if (q[0] == 1)
			{
				var c = new List<int>();
				for (var i = f[q[1]]; i != -1; i = next[i])
					c.Add(v[i]);
				r.Add(string.Join(" ", c));
			}
			else
			{
				int s = q[1], t = q[2];
				if (l[t] != -1) next[l[t]] = f[s];
				if (f[t] == -1) f[t] = f[s];
				l[t] = l[s];
				f[s] = l[s] = -1;
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}

	static T[] CreateArray<T>(int l, T v)
	{
		var a = new T[l];
		if (!Equals(v, default(T)))
			for (int i = 0; i < l; ++i)
				a[i] = v;
		return a;
	}
}
