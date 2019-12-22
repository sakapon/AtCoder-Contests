using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		var h = Console.ReadLine().Split();
		Console.WriteLine(new string(Enumerable.Range(0, h[0].Length).SelectMany(i => h.Select(s => s[i])).ToArray()));
	}
}
