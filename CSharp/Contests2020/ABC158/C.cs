using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var r = Enumerable.Range(1, 1009).FirstOrDefault(x => x * 8 / 100 == h[0] && x / 10 == h[1]);
		Console.WriteLine(r > 0 ? r : -1);
	}
}
