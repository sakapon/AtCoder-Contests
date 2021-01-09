using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine()).GroupBy(x => x).GroupBy(g => g.Count(), g => g.Key).OrderBy(g => -g.Key).First().OrderBy(x => x)));
}
