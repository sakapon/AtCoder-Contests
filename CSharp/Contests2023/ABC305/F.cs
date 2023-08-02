using System;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();

		var sv = 1;
		var ev = n;

		var f = false;
		int[] Nexts()
		{
			f = true;
			return Read()[1..];
		}
		void Move(int v)
		{
			if (!f) Console.ReadLine();
			f = false;
			Console.WriteLine(v);
		}

		var u = new bool[n + 1];
		u[sv] = true;
		DFS(sv);
		Console.ReadLine();

		bool DFS(int v)
		{
			if (v == ev) return true;

			foreach (var nv in Nexts())
			{
				if (u[nv]) continue;
				u[nv] = true;
				Move(nv);
				if (DFS(nv)) return true;
				Move(v);
			}
			return false;
		}
	}
}
