using System;
using System.Collections.Generic;
using System.Linq;

class Q076M
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var sum = a.Sum();
		if (sum % 10 != 0) return false;
		sum /= 10;

		a = a.Concat(a).ToArray();
		var rsq = new StaticRSQ1(a);
		var s = rsq.Raw;

		var map = new MultiMap<long, int>();
		for (int i = 0; i < s.Length; i++)
		{
			map.Add(s[i] % sum, i);
		}

		foreach (var l in map.Values)
		{
			for (int i = 1; i < l.Count; i++)
			{
				if (rsq.GetSum(l[i - 1], l[i]) == sum)
				{
					return true;
				}
			}
		}
		return false;
	}
}

class MultiMap<TK, TV> : Dictionary<TK, List<TV>>
{
	static List<TV> empty = new List<TV>();

	public void Add(TK key, TV v)
	{
		if (ContainsKey(key)) this[key].Add(v);
		else this[key] = new List<TV> { v };
	}

	public List<TV> ReadValues(TK key) => ContainsKey(key) ? this[key] : empty;
}
