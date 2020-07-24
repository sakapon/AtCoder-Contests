using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Console.ReadLine().Distinct().Count() > 1 ? "Yes" : "No");
}
