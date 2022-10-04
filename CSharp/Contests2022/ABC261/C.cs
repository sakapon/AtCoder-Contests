using System;
using System.Collections.Generic;
using System.Text;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var sb = new StringBuilder();

		var d = new Dictionary<string, int>();

		while (n-- > 0)
		{
			var s = Console.ReadLine();
			if (d.ContainsKey(s))
			{
				sb.AppendLine($"{s}({d[s]})");
				d[s]++;
			}
			else
			{
				sb.AppendLine(s);
				d[s] = 1;
			}
		}
		Console.Write(sb);
	}
}
