using System;
using System.Linq;

class C2
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var r = Enumerable.Range(10 * h[1], 10).FirstOrDefault(x => x * 2 / 25 == h[0]);
		Console.WriteLine(r > 0 ? r : -1);
	}
}
