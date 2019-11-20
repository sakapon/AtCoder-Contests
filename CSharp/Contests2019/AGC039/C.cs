using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var x = Console.ReadLine();

		var r = n;
		while (r % 2 == 0) r /= 2;
		var b = 2 * n / r;
		var cycles = Enumerable.Range(1, r).Where(i => r % i == 0).Select(i => b * i).ToArray();
		var counts = new MInt[cycles.Length];

		for (int i = 0; i < cycles.Length; i++)
		{
			var t = cycles[i] / 2;
			var sub = x.Substring(0, t);

			MInt p = 1;
			for (int j = t - 1; j >= 0; j--, p *= 2)
				counts[i] += p * (sub[j] - '0');

			var subs = new[] { sub, new string(sub.Select(c => c == '0' ? '1' : '0').ToArray()) };
			var x2 = string.Join("", Enumerable.Range(0, n / t).Select(j => subs[j % 2]));
			if (x2.CompareTo(x) <= 0) counts[i] += 1;
		}
		for (int i = 0; i < cycles.Length; i++)
			for (int j = i + 1; j < cycles.Length; j++)
				if (cycles[j] % cycles[i] == 0)
					counts[j] -= counts[i];

		Console.WriteLine(cycles.Zip(counts, (t, c) => t * c).Aggregate((u, v) => u + v).V);
	}
}

struct MInt
{
	const int M = 998244353;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
}
