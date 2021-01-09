using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var x = Console.ReadLine().Split().Select(int.Parse).ElementAt(1);
		Console.WriteLine(Console.ReadLine().Select(c => c == 'o' ? ++x : x > 0 ? --x : x).Last());
	}
}
