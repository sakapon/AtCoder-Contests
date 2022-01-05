using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		var r = new int[6];

		for (int id = 1; id <= n; id++)
		{
			var p = ps[id - 1];
			if (p[1] != "AC") continue;

			var pid = p[0][0] - 'A';
			if (r[pid] != 0) continue;

			r[pid] = id;
		}

		return string.Join("\n", r);
	}
}
