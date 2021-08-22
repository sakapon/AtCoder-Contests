using System;

class E
{
	static uint[] Read() => Array.ConvertAll(Console.ReadLine().Split(), uint.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		return Rec(30, a);

		uint Rec(int k, uint[] a)
		{
			var f = 1U << k;
			var b = Array.FindAll(a, x => (x & f) != 0);
			if (b.Length < 2)
			{
				if (k == 0) return 0;
				return Rec(k - 1, a);
			}

			if (k == 0) return 2;
			return (f << 1) + Rec(k - 1, b);
		}
	}
}
