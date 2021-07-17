using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var a = Read();
		Array.Sort(a);
		return a[2] + a[0] == 2 * a[1];
	}
}
