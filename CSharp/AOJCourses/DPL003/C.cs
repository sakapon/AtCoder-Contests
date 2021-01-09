using System;
using System.Collections.Generic;

class C
{
	struct P
	{
		public int i, length;
		public P(int _i, int _l) { i = _i; length = _l; }
	}

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll((Console.ReadLine() + " 0").Split(), int.Parse);

		var M = 0L;
		var q = new Stack<P>();

		for (int i = 0; i <= n; i++)
		{
			var p = new P(i, a[i]);
			while (q.Count > 0 && q.Peek().length > a[i])
			{
				p = q.Pop();
				M = Math.Max(M, (long)(i - p.i) * p.length);
			}

			if (q.Count == 0 || q.Peek().length < a[i])
				q.Push(new P(p.i, a[i]));
		}
		Console.WriteLine(M);
	}
}
