using System;
using System.Linq;

class C2
{
	static void Main()
	{
		var b = new bool[101];
		foreach (var s in Console.ReadLine().Split())
			b[int.Parse(s)] = true;
		Console.WriteLine(Enumerable.Range(1, 100).Where(x => b[x]).ElementAt(3));
	}
}
