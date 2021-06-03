using System;

class Q055C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p, q) = Read3();
		var a = Read();

		var n1 = n / 2;
		var n2 = n - n1;

		var g1 = a[..n1];
		var g2 = a[n1..];

		var r = 0;
		r += Count50(n1, g1, p, q);
		r += Count41(n1, g1, n2, g2, p, q);
		r += Count32(n1, g1, n2, g2, p, q);
		r += Count32(n2, g2, n1, g1, p, q);
		r += Count41(n2, g2, n1, g1, p, q);
		r += Count50(n2, g2, p, q);
		return r;
	}

	static int Count50(int n, int[] a, int p, int q)
	{
		var r = 0;

		for (int i = 0; i < n; i++)
		{
			long vi = a[i];
			for (int j = i + 1; j < n; j++)
			{
				var vj = vi * a[j] % p;
				for (int k = j + 1; k < n; k++)
				{
					var vk = vj * a[k] % p;
					for (int l = k + 1; l < n; l++)
					{
						var vl = vk * a[l] % p;
						for (int m = l + 1; m < n; m++)
						{
							if (vl * a[m] % p == q) r++;
						}
					}
				}
			}
		}
		return r;
	}

	static int Count41(int n, int[] a, int m, int[] b, int p, int q)
	{
		var r = 0;

		for (int i = 0; i < n; i++)
		{
			long vi = a[i];
			for (int j = i + 1; j < n; j++)
			{
				var vj = vi * a[j] % p;
				for (int k = j + 1; k < n; k++)
				{
					var vk = vj * a[k] % p;
					for (int l = k + 1; l < n; l++)
					{
						var vl = vk * a[l] % p;
						for (int i2 = 0; i2 < m; i2++)
						{
							if (vl * b[i2] % p == q) r++;
						}
					}
				}
			}
		}
		return r;
	}

	static int Count32(int n, int[] a, int m, int[] b, int p, int q)
	{
		var r = 0;
		var mc = m * (m - 1) / 2;
		var lb = new long[mc];
		var bi = -1;

		for (int i = 0; i < m; i++)
		{
			long vi = b[i];
			for (int j = i + 1; j < m; j++)
			{
				lb[++bi] = vi * b[j] % p;
			}
		}

		for (int i = 0; i < n; i++)
		{
			long vi = a[i];
			for (int j = i + 1; j < n; j++)
			{
				var vj = vi * a[j] % p;
				for (int k = j + 1; k < n; k++)
				{
					var vk = vj * a[k] % p;
					for (int i2 = 0; i2 < mc; i2++)
					{
						if (vk * lb[i2] % p == q) r++;
					}
				}
			}
		}
		return r;
	}
}
