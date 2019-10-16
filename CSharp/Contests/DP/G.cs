using System;
using System.Linq;

class G
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var rs = new int[h[1]].Select(_ => read()).ToArray();
		var map = rs.ToLookup(r => r[0], r => r[1]);
		var map_r = rs.ToLookup(r => r[1], r => r[0]);

		var q = Enumerable.Range(1, h[0]).Where(i => !map_r.Contains(i)).ToList();
		var c = -1;
		for (; q.Any(); c++)
		{
			var ps = q.ToArray();
			q.Clear();

			foreach (var p in ps)
				foreach (var np in map[p])
					if (!q.Contains(np)) q.Add(np);
		}
		Console.WriteLine(c);
	}
}
