using System;
using System.Linq;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var days = Array.ConvertAll(new bool[n], _ => { Console.ReadLine(); return Console.ReadLine().Split(); });
		Console.WriteLine(days.SelectMany(s => s).GroupBy(s => s).Count(g => g.Count() == n));
	}
}
