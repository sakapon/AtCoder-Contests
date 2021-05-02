using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split();

		var r = 0;

		for (int i = 1; i < n; i++)
		{
			var x0 = a[i - 1];
			var x = a[i];

			var (nx, d) = Next(x0, x);
			a[i] = nx;
			r += d;
		}
		return r;
	}

	static (string ns, int d) Next(string s0, string s)
	{
		if (Compare(s0, s) < 0) return (s, 0);
		if (s0.Length == s.Length) return (s + '0', 1);

		var d = s0.Length - s.Length;
		if (s0.StartsWith(s))
		{
			var cs = s0.ToCharArray();
			Increment(ref cs, cs.Length - 1);
			var ns = new string(cs);
			if (ns.StartsWith(s)) return (ns, d);
		}

		s = s.PadRight(s0.Length, '0');
		if (Compare(s0, s) < 0) return (s, d);
		return (s + '0', d + 1);
	}

	static void Increment(ref char[] s, int i)
	{
		if (i == -1)
		{
			s = ("1" + new string(s)).ToCharArray();
			return;
		}

		s[i]++;
		if (s[i] > '9')
		{
			s[i] = '0';
			Increment(ref s, i - 1);
		}
	}

	static int Compare(string x, string y)
	{
		if (x.Length != y.Length) return x.Length.CompareTo(y.Length);
		return x.CompareTo(y);
	}
}
