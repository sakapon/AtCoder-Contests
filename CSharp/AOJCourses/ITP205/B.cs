using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine()).Select(s => new { s, v = s.Split() }).OrderBy(_ => int.Parse(_.v[0])).ThenBy(_ => int.Parse(_.v[1])).ThenBy(_ => _.v[2][0]).ThenBy(_ => long.Parse(_.v[3])).ThenBy(_ => _.v[4]).Select(_ => _.s)));
}
