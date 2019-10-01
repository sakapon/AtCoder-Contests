using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();

		var uf = new UF(h[0]);
		foreach (var q in new int[h[1]].Select(_ => read()))
			if (q[0] == 0) uf.SetCommon(q[1], q[2]);
			else Console.WriteLine(uf.AreCommon(q[1], q[2]) ? "Yes" : "No");
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void SetCommon(int a, int b) { if (!AreCommon(a, b)) p[p[b]] = p[p[a]]; }
	public bool AreCommon(int a, int b) => GetRoot(a) == GetRoot(b);
	int GetRoot(int a) => p[p[a]] == p[a] ? p[a] : p[a] = GetRoot(p[a]);
}
