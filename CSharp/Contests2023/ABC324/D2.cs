using System;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().ToCharArray();

		Array.Sort(s);
		var cs = new char[n];

		var r = 0;
		for (long x = 0; x < 10_000_000; x++)
		{
			var xs = (x * x).ToString().ToCharArray();
			if (xs.Length > n) break;
			Array.Sort(xs);

			Array.Fill(cs, '0');
			xs.CopyTo(cs, n - xs.Length);
			if (ArrayEqual(s, cs)) r++;
		}
		return r;
	}

	static bool ArrayEqual<T>(T[] a1, T[] a2) where T : IEquatable<T>
	{
		if (a1.Length != a2.Length) return false;
		for (int i = 0; i < a1.Length; ++i)
			if (!a1[i].Equals(a2[i])) return false;
		return true;
	}
}
