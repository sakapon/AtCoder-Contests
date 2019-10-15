using System;
using System.Linq;

class A
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(a[0] >= 13 ? a[1] : a[0] >= 6 ? a[1] / 2 : 0);
	}
}
