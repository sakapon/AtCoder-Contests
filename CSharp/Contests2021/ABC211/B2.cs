using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve() => new int[4].Select(_ => Console.ReadLine()).Distinct().Count() == 4;
}
