using System;
using System.Linq;

class A2
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();
		Console.WriteLine(a.Take(3).Sum() == a[3] || a[0] + a[3] == a[1] + a[2] ? "Yes" : "No");
	}
}
