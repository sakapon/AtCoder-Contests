using System;

class F2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Array.ConvertAll(new int[n], _ => Array.ConvertAll(Console.ReadLine().Split(), int.Parse));

		if (n == 2) { Console.WriteLine(Norm(p[0], p[1]) / 2); return; }

		var M = 0.0;
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				for (int k = j + 1; k < n; k++)
					M = Math.Max(M, GetRadius(p[i], p[j], p[k]));
		Console.WriteLine(M);
	}

	static double GetRadius(int[] p, int[] q, int[] r)
	{
		var e = new[] { Norm(p, q), Norm(q, r), Norm(r, p) };
		if (e[0] < e[1]) Swap(e, 0, 1);
		if (e[0] < e[2]) Swap(e, 0, 2);

		var d = e[1] * e[1] + e[2] * e[2] - e[0] * e[0];
		if (d < 0) return e[0] / 2;

		var cos = d / 2 / e[1] / e[2];
		var sin = Math.Sqrt(1 - cos * cos);
		return e[0] / 2 / sin;
	}

	static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }
	static double Norm(int[] p, int[] q) => Math.Sqrt(Math.Pow(p[0] - q[0], 2) + Math.Pow(p[1] - q[1], 2));
}
