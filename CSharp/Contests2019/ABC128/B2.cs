using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var restaurants = Enumerable.Range(0, n)
			.Select(i => Console.ReadLine().Split())
			.Select((x, i) => new Restaurant { Id = i + 1, City = x[0], Point = int.Parse(x[1]) });

		var query =
			from r in restaurants
			orderby r.City, r.Point descending
			select r.Id;
		Console.WriteLine(string.Join("\n", query));
	}
}

class Restaurant
{
	public int Id { get; set; }
	public string City { get; set; }
	public int Point { get; set; }
}
