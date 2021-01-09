using System;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		int n = h[0], qc = h[1];
		var a = Read();

		var d = Enumerable.Range(0, n - 1).Select(i => (long)a[i + 1] - a[i]).ToArray();

		// 奇数番目と偶数番目。
		var d1 = d.Where((x, i) => i % 2 == 0).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		// 偶数番目と奇数番目。
		var d2 = d.Where((x, i) => i % 2 == 1).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

		// 変化量。
		var delta = 0L;

		for (int k = 0; k < qc; k++)
		{
			var q = Read();

			if (q[0] == 1)
			{
				delta += q[1];
			}
			else if (q[0] == 2)
			{
				delta -= q[1];
			}
			else
			{
				var ai = q[1] - 1;
				if (ai % 2 == 0)
				{
					// odd
					if (ai > 0)
					{
						var d1_1 = a[ai] - a[ai - 1];
						var d1_2 = d1_1 + q[2];

						d2[d1_1]--;
						if (d2.ContainsKey(d1_2)) d2[d1_2]++;
						else d2[d1_2] = 1;
					}

					if (ai + 1 < n)
					{
						var d1_1 = a[ai + 1] - a[ai];
						var d1_2 = d1_1 - q[2];

						d1[d1_1]--;
						if (d1.ContainsKey(d1_2)) d1[d1_2]++;
						else d1[d1_2] = 1;
					}
				}
				else
				{
					// even
					{
						var d1_1 = a[ai] - a[ai - 1];
						var d1_2 = d1_1 + q[2];

						d1[d1_1]--;
						if (d1.ContainsKey(d1_2)) d1[d1_2]++;
						else d1[d1_2] = 1;
					}

					if (ai + 1 < n)
					{
						var d1_1 = a[ai + 1] - a[ai];
						var d1_2 = d1_1 - q[2];

						d2[d1_1]--;
						if (d2.ContainsKey(d1_2)) d2[d1_2]++;
						else d2[d1_2] = 1;
					}
				}
				a[ai] += q[2];
			}

			var c1 = d1.ContainsKey(delta) ? d1[delta] : 0;
			var c2 = d2.ContainsKey(-delta) ? d2[-delta] : 0;
			Console.WriteLine(c1 + c2);
		}

		Console.Out.Flush();
	}
}
