using System;
using System.Linq;

class G
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n - 1].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var M = int.MinValue;
		new PowComb<char>().Find("012".ToArray(), n, p =>
		{
			var r = 0;
			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
					if (p[i] == p[j]) r += a[i][j - i - 1];
			M = Math.Max(M, r);
		});
		Console.WriteLine(M);
	}
}

class PowComb<T>
{
	T[] v, p;
	Action<T[]> act;

	public void Find(T[] _v, int r, Action<T[]> _act)
	{
		v = _v; p = new T[r]; act = _act;
		if (p.Length > 0) Dfs(0); else act(p);
	}

	void Dfs(int i)
	{
		for (int j = 0; j < v.Length; ++j)
		{
			p[i] = v[j];
			if (i + 1 < p.Length) Dfs(i + 1); else act(p);
		}
	}
}
