using System;
using System.Linq;

class F
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var lrs = Array.ConvertAll(new bool[n], _ =>
		{
			var s = Console.ReadLine();
			var (l, r) = (0, 0);
			foreach (var c in s)
				if (c == '(') l++;
				else if (l > 0) l--;
				else r++;
			return (l, r);
		});

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
