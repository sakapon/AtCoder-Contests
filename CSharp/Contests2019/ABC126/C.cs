using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
		double n = h[0], k = h[1];
		Console.WriteLine(n < k ? Push(n, k) : (n - k + 1) / n + (k - 1) / n * Push(k - 1, k));
	}

	static double Push(double n, double k)
	{
		double s = 0.0, p = 1.0, m = k, x = n;
		while (x > 0)
		{
			p /= 2;
			m /= 2;
			var c = Math.Max(Math.Floor(x - m + 1), 0);
			x -= c;
			s += c / n * p;
		}
		return s;
	}
}
