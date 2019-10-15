using System;
using System.Linq;

class A
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read();

		var M = a.Max();
		Console.WriteLine(a.Count(v => v + h[1] >= M));
	}
}
