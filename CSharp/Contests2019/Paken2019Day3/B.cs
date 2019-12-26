using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(new int[n].Select(_ => Console.ReadLine()).Count(x => x == "black") > n / 2 ? "black" : "white");
	}
}
