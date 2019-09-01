using System;
using System.Linq;

class B
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(double.Parse).ToArray();
		Console.WriteLine(Math.Ceiling((a[1] - 1) / (a[0] - 1)));
	}
}
