using System.Text;

class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var a = s.Split('(', ')');
		a = Enumerable.Range(0, a.Length).Select(i => i % 2 == 0 ? a[i] : Transpose(a[i])).ToArray();
		var q = new Queue<string>(a);

		var parens = s.Where(c => !char.IsLetter(c)).ToArray();

		var n = parens.Length + parens.Count(c => c == '(') + 2;
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		var depths = new int[n];
		Array.Fill(depths, -1);
		var texts = new string[n];

		var t = 0;
		var ps = new Stack<int>();
		ps.Push(t);
		depths[t] = 0;

		foreach (var c in parens)
		{
			var p = ps.Peek();
			map[p].Add(++t);
			texts[t] = q.Dequeue();

			if (c == '(')
			{
				map[p].Add(++t);
				ps.Push(t);
				depths[t] = depths[p] + 1;
			}
			else
			{
				ps.Pop();
			}
		}
		map[0].Add(++t);
		texts[t] = q.Dequeue();

		for (int v = 0; v < n; v++)
		{
			if (depths[v] % 2 == 1)
			{
				map[v].Reverse();
			}
		}

		var sb = new StringBuilder();
		DFS(0);
		return sb;

		void DFS(int v)
		{
			if (texts[v] == null)
			{
				foreach (var nv in map[v])
				{
					DFS(nv);
				}
			}
			else
			{
				sb.Append(texts[v]);
			}
		}
	}

	static string Transpose(string s)
	{
		var a = s.ToArray();
		Array.Reverse(a);
		a = Array.ConvertAll(a, c => char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c));
		return new string(a);
	}
}
