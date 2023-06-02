using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		Array.Sort(a);
		var n2 = n >> 1;

		for (int i = 0; i < n2; i++)
		{
			if (a[i] >= a[n2 + 1 + i]) return false;
			if (a[i + 1] >= a[n2 + 1 + i]) return false;
		}
		return true;
	}
}
