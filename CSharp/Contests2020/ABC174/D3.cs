using System;
using System.Linq;

class D3
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var rn = Enumerable.Range(0, n).ToArray();
		var wis = Array.FindAll(rn, i => s[i] == 'W');
		Array.Reverse(rn);
		var ris = Array.FindAll(rn, i => s[i] == 'R');
		return wis.Zip(ris).Count(p => p.First < p.Second);
	}
}
