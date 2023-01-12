using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YJLib8.Data.SparseTable001;

class StaticRMQ
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var sb = new StringBuilder();

		var st = new SparseTable<int>(a, Monoid.Int32_Min);

		while (qc-- > 0)
		{
			var (l, r) = Read2();
			sb.Append(st[l, r]).AppendLine();
		}
		Console.Write(sb);
	}
}
