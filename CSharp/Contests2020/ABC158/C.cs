using System;
using System.Linq;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var r = Enumerable.Range(1, 1009).FirstOrDefault(x => (int)(0.08 * x) == h[0] && (int)(0.1 * x) == h[1]);
		Console.WriteLine(r > 0 ? r : -1);
	}
}
