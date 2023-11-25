using System;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var rn = Enumerable.Range(0, n).ToArray();

		var yoko = s.Select(r => r.Count(c => c == 'o')).ToArray();
		var tate = rn.Select(j => rn.Count(i => s[i][j] == 'o')).ToArray();

		var r = 0L;
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (s[i][j] != 'o') continue;

				r += (yoko[i] - 1) * (tate[j] - 1);
			}
		}
		return r;
	}
}
