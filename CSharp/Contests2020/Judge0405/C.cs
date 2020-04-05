using System;

class C
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var n = h[0] + h[1] + h[2];

		var r = 0;
		var p = new int[3, 3];
		p[0, 0] = 1;

		Dfs(2);
		Console.WriteLine(r);

		void Dfs(int v)
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < h[i]; j++)
					if (p[i, j] == 0)
					{
						if (i > 0 && p[i - 1, j] == 0) break;

						p[i, j] = v;
						if (v < n) Dfs(v + 1);
						else r++;
						p[i, j] = 0;
						break;
					}
		}
	}
}
