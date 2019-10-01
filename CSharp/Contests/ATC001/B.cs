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
			if (q[0] == 0) uf.Unite(q[1], q[2]);
			else Console.WriteLine(uf.AreUnited(q[1], q[2]) ? "Yes" : "No");
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
}
