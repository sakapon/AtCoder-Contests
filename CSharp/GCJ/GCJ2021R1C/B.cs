using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		//var n = long.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		if (s.Length == 1) return 12;

		var comp = Comparer<string>.Create((x, y) =>
		{
			if (x.Length != y.Length) return x.Length.CompareTo(y.Length);
			return x.CompareTo(y);
		});

		var kmax = s.Length / 2 + 1;
		var l = new List<string>();

		for (int k = 1; k <= kmax; k++)
		{
			var x = long.Parse(s.Substring(0, k));

			for (int i = 0; i < 10; i++)
			{
				l.Add(GetRoaring(x).SkipWhile(t => comp.Compare(t, s) <= 0).First());
				x++;
				if (x.ToString().Length > k)
					x /= 10;
			}
		}
		l.Sort(comp);

		return l[0];
	}

	static IEnumerable<string> GetRoaring(long x)
	{
		var s = x.ToString();
		for (var i = x + 1; ; i++)
		{
			s += i;
			yield return s;
		}
	}

	static IEnumerable<string> GetRoaring(string s)
	{
		for (var i = long.Parse(s) + 1; ; i++)
		{
			s += i;
			yield return s;
		}
	}
}
