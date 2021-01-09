using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).OrderBy(v => v[0]).ThenBy(v => v[1]).Select(v => $"{v[0]} {v[1]}")));
}
