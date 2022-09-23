using System;
using System.Linq;
using System.Text;

class C
{
	static void Main()
	{
		var n = ulong.Parse(Console.ReadLine());
		var sb = new StringBuilder();

		var r60 = Enumerable.Range(0, 60).ToArray();
		var fs = r60.Where(i => (n & (1UL << i)) != 0).ToArray();
		var m = fs.Length;

		for (int x = 0; x < 1 << m; x++)
		{
			var r = 0UL;
			for (int i = 0; i < m; i++)
			{
				if ((x & (1 << i)) != 0)
				{
					r += 1UL << fs[i];
				}
			}
			sb.AppendLine(r.ToString());
		}
		Console.Write(sb);
	}
}
