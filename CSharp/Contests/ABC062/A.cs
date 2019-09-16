using System;
using System.Linq;

class A
{
	static void Main()
	{
		var g = "0131212112121";
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(g[a[0]] == g[a[1]] ? "Yes" : "No");
	}
}
