using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		// 最後に箱に入れたときのインデックス
		var u = new int[1 << 7];
		Array.Fill(u, -1);
		var q = new Stack<int>();

		for (int i = 0; i < n; i++)
		{
			var c = s[i];
			if (c == '(')
			{
				q.Push(i);
			}
			else if (c == ')')
			{
				var j = q.Pop();
				for (char z = 'a'; z <= 'z'; z++)
				{
					if (u[z] > j)
					{
						u[z] = -1;
					}
				}
			}
			else
			{
				if (u[c] != -1) return false;
				u[c] = i;
			}
		}
		return true;
	}
}
