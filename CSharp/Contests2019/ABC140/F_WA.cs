using System;

class F_WA
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Read();

		Array.Sort(s);
		Array.Reverse(s);

		for (int c = 1 << n - 1; c > 0; c >>= 1)
			for (int i = 0; i < c; i++)
				if (s[i] == s[i + c]) return false;
		return true;
	}
}
