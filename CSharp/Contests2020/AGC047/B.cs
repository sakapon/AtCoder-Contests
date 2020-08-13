using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = new int[n].Select(_ => Console.ReadLine()).OrderBy(s => s.Length).ToArray();

		var r = 0L;
		var root = new Node();

		foreach (var s in ss)
		{
			var node = root;
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (i == 0)
					node.Ends[s[i] - 'a']++;
				else
					for (int j = 0; j < 26; j++)
					{
						if (node.Ends[j] == 0) continue;
						var c = (char)('a' + j);
						if (s.IndexOf(c, 0, i + 1) >= 0)
							r += node.Ends[j];
					}

				if (!node.Nexts.ContainsKey(s[i]))
					node.Nexts[s[i]] = new Node();
				node = node.Nexts[s[i]];
			}
		}
		Console.WriteLine(r);
	}
}

class Node
{
	public int[] Ends = new int[26];
	public Dictionary<char, Node> Nexts = new Dictionary<char, Node>();
}
