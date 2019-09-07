using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Split().Select(int.Parse).OrderByDescending(x => x).ToArray();

		var p2 = new int[n + 2];
		p2[0] = 0; p2[1] = 1;
		for (var i = 2; i < p2.Length; i++) p2[i] = 2 * p2[i - 1];

		for (var i = 1; i <= n; i++)
			for (int j = 0; j < i; j++)
			{
				var sj = p2[j];
				var ej = p2[j + 1] - 1;
				for (int k = sj; k <= ej; k++) if (s[k] <= s[p2[i] + k]) { Console.WriteLine("No"); return; }
			}
		Console.WriteLine("Yes");
	}
}
