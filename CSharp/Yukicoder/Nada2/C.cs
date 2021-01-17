using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = Read();
		Array.Sort(a);

		//if (a.Any(x => x == 1)) return 0;
		if (Gcd(Gcd(a[0], a[1]), a[2]) > 1) return "INF";

		var u = new bool[a[0] * a[1] + 5000000];
		for (int k = 0; k < a[0]; k++)
			for (int i = k * a[1]; i < u.Length; i += a[0])
				u[i] = true;
		for (int i = 0; i < u.Length; i += a[2])
			u[i] = true;

		var r = 0;
		for (int k = 1; k < a[2]; k++)
			for (int i = k; i < u.Length && !u[i]; i += a[2])
				r++;
		return r;
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
