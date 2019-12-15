using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		int i;
		Console.WriteLine(Enumerable.Range(0, 1000).Select(x => $"{x:D3}").Count(p => (i = s.IndexOf(p[0])) > -1 && (i = s.IndexOf(p[1], i + 1)) > -1 && s.IndexOf(p[2], i + 1) > -1));
	}
}
