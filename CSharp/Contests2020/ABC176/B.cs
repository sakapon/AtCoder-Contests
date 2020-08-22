using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Console.ReadLine().Sum(c => c - '0') % 9 == 0 ? "Yes" : "No");
}
