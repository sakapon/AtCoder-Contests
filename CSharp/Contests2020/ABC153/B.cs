using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		Console.WriteLine(read().Sum() >= h[0] ? "Yes" : "No");
	}
}
