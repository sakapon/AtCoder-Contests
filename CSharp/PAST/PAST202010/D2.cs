using System;
using System.Linq;

class D2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var x = s.IndexOf('#');
		var y = n - 1 - s.LastIndexOf('#');

		var all = Math.Max(x + y, s.Split('#').Max(p => p.Length));
		Console.WriteLine($"{x} {all - x}");
	}
}
