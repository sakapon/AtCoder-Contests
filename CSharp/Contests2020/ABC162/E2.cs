using System;

class E2
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];

		MInt r = 0;
		var dp = new MInt[k + 1];
		for (int i = k; i > 0; i--)
		{
			dp[i] = ((MInt)(k / i)).Pow(n);
			for (int j = 2 * i; j <= k; j += i)
				dp[i] -= dp[j];
			r += i * dp[i];
		}
		Console.WriteLine(r);
	}
}
