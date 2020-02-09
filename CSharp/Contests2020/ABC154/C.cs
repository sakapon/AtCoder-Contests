using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var m = Console.ReadLine().Split().Select(int.Parse).Distinct().Count();
		Console.WriteLine(n == m ? "YES" : "NO");
	}
}
