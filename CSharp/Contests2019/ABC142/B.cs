using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = read();
		var h = read();
		Console.WriteLine(h.Count(x => x >= a[1]));
	}
}
