using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var days = Array.ConvertAll(new bool[n], _ => { Console.ReadLine(); return Console.ReadLine().Split(); });

		var ps = days.SelectMany(v => v).Distinct().ToArray();
		Console.WriteLine(ps.Count(p => days.All(d => d.Contains(p))));
	}
}
