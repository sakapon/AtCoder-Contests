using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		Console.WriteLine(Console.ReadLine().Split().Select(int.Parse).All(x => x % 2 != 0 || x % 3 == 0 || x % 5 == 0) ? "APPROVED" : "DENIED");
	}
}
