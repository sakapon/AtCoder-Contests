using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		if (n % 2 == 0) { Console.WriteLine("No"); return; }
		Console.WriteLine("Yes");
		var m = Enumerable.Range(0, n).Select(i => Enumerable.Range(1, n).Select(j => n * i + j).ToArray()).ToArray();
		for (var i = 0; i < n / 2; i++)
		{
			var j = n - 2 - i;
			var t = m[i][i];
			m[i][i] = m[j][j];
			m[j][j] = t;
		}
		foreach (var r in m) Console.WriteLine(string.Join(" ", r));
	}
}
