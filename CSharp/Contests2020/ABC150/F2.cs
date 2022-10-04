using System;
using System.Linq;
using System.Text;

class F2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var b = ReadL();
		var sb = new StringBuilder();

		var ad = Enumerable.Range(0, 2 * n - 1).Select(i => a[(i + 1) % n] ^ a[i % n]).ToArray();
		var bd = Enumerable.Range(0, n - 1).Select(i => b[i + 1] ^ b[i]).ToArray();

		var rh = new RollingHashBuilder(2 * n);
		var ah = rh.Build(ad);
		var bh = RollingHashBuilder.Hash(bd);

		for (int k = 0; k < n; k++)
			if (ah.Hash(k, n - 1) == bh) sb.AppendLine($"{k} {a[k] ^ b[0]}");
		Console.Write(sb);
	}
}
