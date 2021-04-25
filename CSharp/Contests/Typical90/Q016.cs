using System;

class Q016
{
	const int max = 10000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		Array.Sort(a);

		var r = max;
		for (int i = 0; i < max; i++)
		{
			var n0 = n - i * a[0];

			for (int j = 0; i + j < max && n0 >= 0; j++, n0 -= a[1])
			{
				if (n0 % a[2] == 0)
				{
					r = Math.Min(r, i + j + n0 / a[2]);
				}
			}
		}
		return r;
	}
}
