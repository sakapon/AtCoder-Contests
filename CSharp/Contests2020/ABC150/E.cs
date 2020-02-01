using System;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(((MInt)2).Pow(2 * int.Parse(Console.ReadLine()) - 2) * Console.ReadLine().Split().Select(int.Parse).OrderBy(x => -x).Select((c, i) => (MInt)(i + 2) * c).Aggregate((x, y) => x + y));
}

struct MInt
{
	const int M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;

	public MInt Pow(int i) => MPow(V, i);
	public override string ToString() => $"{V}";

	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
}
