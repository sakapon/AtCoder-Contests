using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var s1 = Enumerable.Range(0, n).Where(i => s[i] == '1').ToArray();
		var t1 = Enumerable.Range(0, n).Where(i => t[i] == '1').ToArray();

		var d1 = s1.Length - t1.Length;
		if (d1 < 0 || d1 % 2 != 0) { Console.WriteLine(-1); return; }
		d1 /= 2;

		var r = 0L;
		var s1e = s1.AsSpan().GetEnumerator();
		var i = s1e.MoveNext() ? s1e.Current : n;

		foreach (var j in t1)
		{
			while (i < j)
			{
				if (d1 == 0) { Console.WriteLine(-1); return; }
				d1--;
				s1e.MoveNext();
				r += s1e.Current - i;
				i = s1e.MoveNext() ? s1e.Current : n;
			}
			r += i - j;
			i = s1e.MoveNext() ? s1e.Current : n;
		}
		while (i < n)
		{
			s1e.MoveNext();
			r += s1e.Current - i;
			i = s1e.MoveNext() ? s1e.Current : n;
		}
		Console.WriteLine(r);
	}
}
