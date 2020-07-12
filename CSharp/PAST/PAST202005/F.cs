using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var n2 = n / 2;
		var a = new int[n].Select(_ => Console.ReadLine()).ToArray();

		var r = new char[n];
		if (n % 2 == 1) r[n2] = a[n2][0];

		for (int i = 0; i < n2; i++)
		{
			var c = GetSame(a[i], a[n - 1 - i]);
			if (c == 0) { Console.WriteLine(-1); return; }
			r[i] = r[n - 1 - i] = c;
		}
		Console.WriteLine(new string(r));
	}

	static char GetSame(string s, string t)
	{
		var set = t.ToHashSet();
		return s.FirstOrDefault(set.Contains);
	}
}
