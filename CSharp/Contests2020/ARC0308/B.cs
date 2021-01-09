using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		int[] h = Read(), a = Read(), b = Read();
		Console.WriteLine(new int[h[2]].Select(_ => Read()).Select(x => a[x[0] - 1] + b[x[1] - 1] - x[2]).Append(a.Min() + b.Min()).Min());
	}
}
