using System;
using System.Linq;

class F
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var count = 0;
		foreach (var si in s)
			foreach (var c in si)
				if (c == '(') count--;
				else count++;
		if (count != 0) return false;

		var lrs = new (int l, int r)[n];
		for (int i = 0; i < n; i++)
		{
			var (l, r) = (0, 0);
			foreach (var c in s[i])
				if (c == '(') l++;
				else if (l > 0) l--;
				else r++;
			lrs[i] = (l, r);
		}

		var al = 0;
		var q1 = lrs.Where(lr => lr.l >= lr.r).OrderBy(lr => lr.r);
		var q2 = lrs.Where(lr => lr.l < lr.r).OrderBy(lr => -lr.l);
		foreach (var (l, r) in q1.Concat(q2))
		{
			if (al < r) return false;
			al += l - r;
		}
		return al == 0;
	}
}
