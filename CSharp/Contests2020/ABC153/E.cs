using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		var h = z[0];
		var ms = Array.ConvertAll(new int[z[1]], _ => Read());

		var dp = Array.ConvertAll(new int[h + 1], _ => 1 << 30);
		dp[0] = 0;
		for (int j, i = 1; i <= h; i++)
			foreach (var m in ms)
				dp[i] = Math.Min(dp[i], dp[(j = i - m[0]) < 0 ? 0 : j] + m[1]);
		Console.WriteLine(dp[h]);
	}
}
