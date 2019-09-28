using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = read();
		Console.WriteLine(read().Count(x => x >= a[1]));
	}
}
