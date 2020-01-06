using System;

class F
{
	static void Main()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var t_sub = new int[t.Length];
		t_sub[0] = -1;
		for (int i = 1; i < t.Length; i++)
		{
			var p = t_sub[i - 1];
			t_sub[i] = p > -1 && t[i] == t[p + 1] ? p + 1 : t[i] == t[0] ? 0 : -1;
		}

		var st = new bool[s.Length];
		for (int i = 0, j = 0; i < s.Length;)
		{
			for (; ; j++)
			{
				if (s[(i + j) % s.Length] != t[j]) { j--; break; }
				if (j == t.Length - 1) { st[i] = true; break; }
			}

			if (j == -1)
			{
				i++;
				j = 0;
			}
			else if (t_sub[j] == -1)
			{
				i += j + 1;
				j = 0;
			}
			else
			{
				i += j - t_sub[j];
				j = t_sub[j] + 1;
			}
		}

		var M = 0;
		var u = new int[s.Length];
		for (int i = 0; i < s.Length; i++)
		{
			if (!st[i] || u[i] > 0) continue;

			var j = i;
			while (st[MInt(j - t.Length, s.Length)])
			{
				j = MInt(j - t.Length, s.Length);
				if (j == i) { Console.WriteLine(-1); return; }
			}

			var c = 0;
			while (st[j])
			{
				u[j] = ++c;
				j = (j + t.Length) % s.Length;
			}
			M = Math.Max(M, c);
		}
		Console.WriteLine(M);
	}

	static int MInt(int x, int mod) => (x %= mod) < 0 ? x + mod : x;
}
