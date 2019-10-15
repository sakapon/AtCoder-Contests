using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		for (var i = a[1] - a[0] + 1; i < a[1] + a[0];) Console.Write($"{i++} ");
	}
}
