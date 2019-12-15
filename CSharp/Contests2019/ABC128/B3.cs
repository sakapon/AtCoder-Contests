using System;
using System.Linq;

class B3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var query = Enumerable.Range(0, n)
			.Select(i => Console.ReadLine().Split())
			.Select((x, i) => new { Id = i + 1, City = x[0], Point = int.Parse(x[1]) })
			.OrderBy(r => r.City)
			.ThenByDescending(r => r.Point)
			.Select(r => r.Id);
		Console.WriteLine(string.Join("\n", query));
	}
}
