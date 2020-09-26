using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine()).Distinct().Count());
}
