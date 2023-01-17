using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WBTrees;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c - 'a').ToArray();
		var qc = int.Parse(Console.ReadLine());
		var sb = new StringBuilder();

		var sets = Array.ConvertAll(new bool[26], _ => new WBSet<int>());
		for (int i = 0; i < n; i++)
		{
			sets[s[i]].Add(i);
		}
		var il = new List<int>();

		while (qc-- > 0)
		{
			var q = Console.ReadLine().Split();
			if (q[0][0] == '1')
			{
				var x = int.Parse(q[1]) - 1;
				var c = q[2][0] - 'a';

				sets[s[x]].Remove(x);
				s[x] = c;
				sets[s[x]].Add(x);
			}
			else
			{
				var lr = Array.ConvertAll(q, int.Parse);
				var l = lr[1] - 1;
				var r = lr[2] - 1;

				sb.AppendLine(Check() ? "Yes" : "No");

				bool Check()
				{
					if (s[l] > s[r]) return false;

					il.Clear();
					il.Add(l);
					for (int c = s[l] + 1; c < s[r]; c++)
					{
						if (sets[c].Count == 0) continue;
						il.Add(sets[c].GetFirst().Item);
						il.Add(sets[c].GetLast().Item);
					}
					il.Add(r);

					for (int i = 1; i < il.Count; i++)
					{
						if (il[i - 1] > il[i]) return false;
					}

					var count = 0;
					for (int c = s[l]; c <= s[r]; c++)
					{
						if (sets[c].Count == 0) continue;
						count += sets[c].GetCount(x => x >= l, x => x <= r);
					}
					return count == r - l + 1;
				}
			}
		}
		Console.Write(sb);
	}
}
