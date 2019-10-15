using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Console.ReadLine().Count(c => c == 'x') < 8 ? "YES" : "NO");
}
