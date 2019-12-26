using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine()).GroupBy(x => x).OrderBy(g => g.Count()).Last().Key);
}
