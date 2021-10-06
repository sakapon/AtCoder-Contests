using System;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, a, b) = Read4();
		var hw = h * w;

		var r = 0L;
		var u = new bool[hw];

		Dfs(0);
		return r;

		void Dfs(int id)
		{
			if (id == hw)
			{
				if (a == 0) r++;
				return;
			}

			if (u[id])
			{
				Dfs(id + 1);
			}
			else
			{
				u[id] = true;

				Dfs(id + 1);

				if (a > 0 && id % w < w - 1 && !u[id + 1])
				{
					a--;
					u[id + 1] = true;
					Dfs(id + 1);
					u[id + 1] = false;
					a++;
				}

				if (a > 0 && id / w < h - 1 && !u[id + w])
				{
					a--;
					u[id + w] = true;
					Dfs(id + 1);
					u[id + w] = false;
					a++;
				}

				u[id] = false;
			}
		}
	}
}
