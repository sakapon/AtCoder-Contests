using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var n = h[0];

		var s = new int[n + 1];
		for (int i = 0; i < h[1]; i++) s[int.Parse(Console.ReadLine())] = -1;
		s[0] = 1;
		if (s[1] != -1) s[1] = 1;

		for (int i = 2; i <= n; i++)
		{
			if (s[i] == -1) continue;
			if (s[i - 2] != -1) s[i] += s[i - 2];
			if (s[i - 1] != -1) s[i] += s[i - 1];
			s[i] %= 1000000007;
		}
		Console.WriteLine(s[n]);
	}
}
