using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).Take(2).Sum());
}
