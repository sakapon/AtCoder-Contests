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
		double a = Norm(p, q), b = Norm(q, r), c = Norm(r, p), t;
		if (b > a) { t = b; b = a; a = t; }
		if (c > a) { t = c; c = a; a = t; }

		var cos = CosA(a, b, c);
		return a / 2 / (cos < 0 ? 1 : Math.Sqrt(1 - cos * cos));
	}

	static double Norm(int[] p, int[] q) => Math.Sqrt(Math.Pow(p[0] - q[0], 2) + Math.Pow(p[1] - q[1], 2));
	static double CosA(double a, double b, double c) => (b * b + c * c - a * a) / (2 * b * c);
}
