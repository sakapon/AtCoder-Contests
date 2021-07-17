using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = 0;
		for (int i = 0; i < n - 1; i++)
		{
			var j = Array.IndexOf(a, i + 1);
			var t = j - i + 1;
			r += t;
			Array.Reverse(a, i, t);
		}
		return r;
	}
}
