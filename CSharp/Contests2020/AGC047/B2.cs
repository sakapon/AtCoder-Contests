using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = new int[n].Select(_ => Console.ReadLine()).OrderBy(s => s.Length).ToArray();

		var r = 0L;
		var nodes = new List<int[]> { new int[26] };
		var ends = new List<int[]> { new int[26] };

		foreach (var s in ss)
		{
			var id = 0;
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (i == 0)
					ends[id][s[i] - 'a']++;
				else
					for (int j = 0; j < 26; j++)
					{
						if (ends[id][j] == 0) continue;
						var c = (char)('a' + j);
						if (s.IndexOf(c, 0, i + 1) >= 0)
							r += ends[id][j];
					}

				var nid = nodes[id][s[i] - 'a'];
				if (nid == 0)
				{
					nodes[id][s[i] - 'a'] = nid = nodes.Count;
					nodes.Add(new int[26]);
					ends.Add(new int[26]);
				}
				id = nid;
			}
		}
		Console.WriteLine(r);
	}
}
