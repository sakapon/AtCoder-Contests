using System;

class G
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];

		var f = new MInt[k + 1];
		var t = new MInt[k + 1];
		f[0] = 1;
		t[0] = 1;
		for (int i = 1; i <= k; ++i)
		{
			f[i] = f[i - 1] / i;
			t[i] = t[i - 1] + f[i] * (i % 2 == 0 ? 1 : -1);
		}

		MInt r = 0;
		for (int i = 1; i <= k; ++i)
		{
			r += MInt.MPow(i, n) * f[i] * t[k - i];
		}
		Console.WriteLine(r);
	}
}
