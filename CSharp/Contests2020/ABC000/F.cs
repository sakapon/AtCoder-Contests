using System;
using System.Linq;

class F
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = Read();
		var ps = new int[h[0]].Select(_ => Read()).ToArray();

		Console.WriteLine(string.Join(" ", a));
	}
}
