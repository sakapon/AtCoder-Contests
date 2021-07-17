using System;

class Q055
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p, q) = Read3();
		var a = Read();

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
}
