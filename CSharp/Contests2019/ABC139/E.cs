using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	class Game
	{
		public int Id;
		public List<Game> ForeGames = new List<Game>();
		public List<Game> PostGames = new List<Game>();

		public void ClearPost()
		{
			foreach (var g in PostGames) g.ForeGames.Remove(this);
			PostGames.Clear();
		}
	}

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = Comb2(n).Select(p => new Game { Id = 10000 * p[0] + p[1] }).ToDictionary(g => g.Id);
		for (var i = 1; i <= n; i++)
		{
			var a = Console.ReadLine().Split().Select(int.Parse).Select(j => i < j ? 10000 * i + j : 10000 * j + i).ToArray();
			for (var j = 0; j < n - 2; j++)
			{
				d[a[j]].PostGames.Add(d[a[j + 1]]);
				d[a[j + 1]].ForeGames.Add(d[a[j]]);
			}
		}

		var q = d.Values.Where(g => g.ForeGames.Count == 0).ToArray();
		var c = 0;
		for (; q.Length > 0; c++)
		{
			var set = new HashSet<Game>(q.SelectMany(g => g.PostGames));
			foreach (var g in q) g.ClearPost();
			q = set.Where(g => g.ForeGames.Count == 0).ToArray();
		}
		Console.WriteLine(d.All(p => p.Value.ForeGames.Count == 0) ? c : -1);
	}

	static IEnumerable<int[]> Comb2(int n)
	{
		for (var i = 1; i <= n; i++)
			for (var j = i + 1; j <= n; j++)
				yield return new[] { i, j };
	}
}
