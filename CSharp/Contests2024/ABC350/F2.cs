using System.Text;

class F2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var parens = new int[n];
		var ps = new Stack<int>();

		for (int i = 0; i < n; i++)
		{
			if (s[i] == '(')
			{
				ps.Push(i);
			}
			else if (s[i] == ')')
			{
				var j = ps.Pop();
				parens[i] = j;
				parens[j] = i;
			}
		}

		var sb = new StringBuilder();
		var d = 1;
		ps.Push(n);

		for (int i = 0; i < n; i += d)
		{
			if (ps.Peek() == i)
			{
				ps.Pop();
				i = parens[i];
				d *= -1;
			}
			else if (char.IsPunctuation(s[i]))
			{
				ps.Push(i);
				i = parens[i];
				d *= -1;
			}
			else
			{
				sb.Append(d == 1 ? s[i] : Transpose(s[i]));
			}
		}
		return sb;
	}

	static char Transpose(char c) => char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c);
}
