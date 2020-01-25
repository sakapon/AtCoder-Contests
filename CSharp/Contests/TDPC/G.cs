using System;
using System.Linq;

class G
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var k = long.Parse(Console.ReadLine());
		var d = s.Select((c, i) => new { c, i }).GroupBy(_ => _.c, _ => _.i).OrderBy(g => g.Key).ToDictionary(g => g.Key, g => g.ToArray());

		var dp = Enumerable.Repeat(long.MaxValue / 2, n).ToArray();
		for (int i = n - 1; i >= 0; i--)
		{
			var sum = 1L;
			foreach (var p in d)
			{
				var j = Array.BinarySearch(p.Value, i);
				j = j >= 0 ? j + 1 : ~j;
				if (j < p.Value.Length) sum += dp[p.Value[j]];
			}
			if (sum > k) break;
			dp[i] = sum;
		}

		var r = "";
		var ti = 0;
		var tk = 0L;
		for (int i = 0; i < n; i++)
		{
			foreach (var p in d)
			{
				var j = Array.BinarySearch(p.Value, ti);
				j = j >= 0 ? j : ~j;
				if (j == p.Value.Length) continue;

				var tk2 = tk + dp[p.Value[j]];
				if (tk2 < k)
				{
					tk = tk2;
				}
				else
				{
					r += p.Key;
					ti = p.Value[j] + 1;
					++tk;
					break;
				}
			}
			if (tk == k) break;
			if (r.Length == i) { r = "Eel"; break; }
		}
		Console.WriteLine(r);
	}
}
