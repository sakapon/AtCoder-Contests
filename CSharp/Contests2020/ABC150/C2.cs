using System;
using System.Linq;

class C2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		int i = 0, j = 0, c = 0;
		new Perm<int>().Find(Enumerable.Range(1, n).ToArray(), n, p =>
		{
			++c;
			if (p.SequenceEqual(a)) i = c;
			if (p.SequenceEqual(b)) j = c;
		});
		Console.WriteLine(Math.Abs(i - j));
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
