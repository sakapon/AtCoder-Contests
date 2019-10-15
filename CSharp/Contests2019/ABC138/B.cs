using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		Console.WriteLine(1 / Console.ReadLine().Split().Select(int.Parse).Sum(x => 1.0 / x));
	}
}
