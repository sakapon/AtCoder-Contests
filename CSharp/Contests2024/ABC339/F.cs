using System.Numerics;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var l0 = new List<BigInteger>();
		var l1 = new List<BigInteger>();

		foreach (var x in a)
		{
			if (x.Length <= 501)
			{
				l0.Add(BigInteger.Parse(x));
			}
			else
			{
				l1.Add(BigInteger.Parse(x));
			}
		}

		var r = 0L;
		var c0 = l0.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		var c1 = l1.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

		for (int i = 0; i < l0.Count; i++)
		{
			for (int j = 0; j < l0.Count; j++)
			{
				var key = l0[i] * l0[j];
				r += c0.GetValueOrDefault(key, 0);
				r += c1.GetValueOrDefault(key, 0);
			}
		}

		for (int i = 0; i < l0.Count; i++)
		{
			for (int j = 0; j < l1.Count; j++)
			{
				var key = l0[i] * l1[j];
				r += c1.GetValueOrDefault(key, 0) << 1;
			}
		}

		return r;
	}
}
