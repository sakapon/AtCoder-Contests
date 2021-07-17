using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var v = Read();
		Array.Sort(v);
		var (a, b, c) = (v[0], v[1], v[2]);

		if (Gcd(Gcd(a, b), c) > 1) return "INF";
		if (Gcd(a, b) > 1) (b, c) = (c, b);
		if (Gcd(a, b) > 1) (a, c) = (c, a);

		var u = new bool[a * b];
		for (int k = 0; k < a; k++)
			for (int i = k * b; i < u.Length; i += a)
				u[i] = true;

		var r = 0;
		for (int k = 0; k < c; k++)
			for (int i = k; i < u.Length && !u[i]; i += c)
				r++;
		return r;
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
