using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var r9 = Enumerable.Range(1, 9);
		Console.WriteLine(r9.SelectMany(i => r9.Select(j => i * j)).Any(x => x == n) ? "Yes" : "No");
	}
}
