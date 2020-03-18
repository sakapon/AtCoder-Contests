using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(int.Parse(Console.ReadLine()) == Console.ReadLine().Split().Distinct().Count() ? "YES" : "NO");
}
