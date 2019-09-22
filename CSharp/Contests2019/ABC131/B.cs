using System;
using System.Linq;

class B
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = a[0], l = a[1];

		var m = l > 0 ? l : l + n - 1 >= 0 ? 0 : l + n - 1;
		Console.WriteLine(Enumerable.Range(l, n).Sum() - m);
	}
}
