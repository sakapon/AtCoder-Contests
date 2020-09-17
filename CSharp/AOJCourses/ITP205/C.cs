using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		int[] prev = null;
		var end = false;
		new Perm<int>().Find(a.OrderBy(x => x).ToArray(), n, p =>
		{
			if (end)
			{
				Console.WriteLine(string.Join(" ", p));
				Environment.Exit(0);
			}

			if (Enumerable.SequenceEqual(p, a))
			{
				if (prev != null) Console.WriteLine(string.Join(" ", prev));
				Console.WriteLine(string.Join(" ", p));
				end = true;
			}
			prev = (int[])p.Clone();
		});
	}
}

class Perm<T>
{
	T[] v, p;
	bool[] u;
	Action<T[]> act;

	public void Find(T[] _v, int r, Action<T[]> _act)
	{
		v = _v; p = new T[r]; u = new bool[v.Length]; act = _act;
		if (p.Length > 0) Dfs(0); else act(p);
	}

	void Dfs(int i)
	{
		for (int j = 0; j < v.Length; ++j)
		{
			if (u[j]) continue;
			p[i] = v[j];
			u[j] = true;
			if (i + 1 < p.Length) Dfs(i + 1); else act(p);
			u[j] = false;
		}
	}
}
