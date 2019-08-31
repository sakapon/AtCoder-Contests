using System;
using System.Linq;

class A
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var p = a[1] - a[0];
		var m = (a[2] - a[0]) % p;
		Console.WriteLine(m == 0 ? a[2] : a[2] + p - m);
	}
}
