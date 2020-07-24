using System;
using System.Linq;

class C2
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var r = Enumerable.Range(10 * h[1], 10).FirstOrDefault(x => (int)(0.08 * x) == h[0]);
		Console.WriteLine(r > 0 ? r : -1);
	}
}
