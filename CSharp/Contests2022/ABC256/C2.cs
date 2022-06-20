using System;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = Read();
		var h = a[..3];
		var w = a[3..];

		var r = 0;
		for (int v00 = 1; v00 <= 30; v00++)
		{
			for (int v01 = 1; v01 <= 30; v01++)
			{
				for (int v10 = 1; v10 <= 30; v10++)
				{
					for (int v11 = 1; v11 <= 30; v11++)
					{
						var v02 = h[0] - v00 - v01;
						var v12 = h[1] - v10 - v11;
						var v20 = w[0] - v00 - v10;
						var v21 = w[1] - v01 - v11;

						var v22h = h[2] - v20 - v21;
						var v22w = w[2] - v02 - v12;
						if (v02 > 0 && v12 > 0 && v20 > 0 && v21 > 0 && v22h > 0 && v22h == v22w) r++;
					}
				}
			}
		}
		return r;
	}
}
