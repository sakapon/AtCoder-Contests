using System;
using System.Linq;

class G2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n - 1].Select((_, i) => new int[i + 1].Concat(Console.ReadLine().Split().Select(int.Parse)).ToArray()).ToList();
		var p2 = Enumerable.Range(0, n + 1).Select(i => 1 << i).ToArray();

		var s = new int[p2[n]];
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				for (int f = 0; f < p2[n]; f++)
					if ((f & p2[i]) > 0 && (f & p2[j]) > 0)
						s[f] += a[i][j];

		var M = int.MinValue;
		for (int f = 0; f < p2[n]; f++)
			for (int g = f; g < p2[n]; g++)
				if ((f & g) == 0)
					M = Math.Max(M, s[f] + s[g] + s[p2[n] - 1 - f - g]);
		Console.WriteLine(M);
	}
}
