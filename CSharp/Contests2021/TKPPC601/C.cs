using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var m = a[0];
		a = Array.FindAll(a, x => x != -1);
		Array.Sort(a);

		var c0 = Array.IndexOf(a, m);
		if (c0 >= n) return false;

		Array.Reverse(a);
		var c1 = Array.IndexOf(a, m);
		if (c1 >= n) return false;

		return true;
	}
}
