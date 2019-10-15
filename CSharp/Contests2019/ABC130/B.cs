using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var l = read();

		var d = new int[h[0] + 1];
		for (int i = 0; i < h[0]; i++) d[i + 1] = d[i] + l[i];
		Console.WriteLine(d.TakeWhile(v => v <= h[1]).Count());
	}
}
