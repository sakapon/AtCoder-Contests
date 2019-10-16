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
		var ins = new int[h[0] + 1];
		foreach (var r in rs) ins[r[1]]++;

		var q = Enumerable.Range(1, h[0]).Where(i => ins[i] == 0).ToList();
		var c = -1;
		for (; q.Any(); c++)
		{
			var ps = q.ToArray();
			q.Clear();

			foreach (var p in ps)
				foreach (var np in map[p])
				{
					ins[np]--;
					if (ins[np] == 0) q.Add(np);
				}
		}
		Console.WriteLine(c);
	}
}
