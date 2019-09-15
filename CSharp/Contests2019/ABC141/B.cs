using System;
using System.Linq;

class B
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(s.Where((c, i) => i % 2 == 0).All(c => c != 'L') && s.Where((c, i) => i % 2 == 1).All(c => c != 'R') ? "Yes" : "No");
	}
}
