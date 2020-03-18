using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var M = new int[n + 1];
		for (int i = 0; i < n; i++)
			M[i + 1] = Math.Max(M[i], a[i]);
		Console.WriteLine(Enumerable.Range(0, n).Count(i => M[i] < a[i]));
	}
}
