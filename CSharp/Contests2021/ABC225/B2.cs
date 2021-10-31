using System;
using System.Linq;

class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		return es.All(e => e[0] == es[0][0] || e[1] == es[0][0]) || es.All(e => e[0] == es[0][1] || e[1] == es[0][1]);
	}
}
