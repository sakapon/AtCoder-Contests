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
		if (s1.Length == 0 && t1.Length == 0) { Console.WriteLine(0); return; }

		//var d1 = s.Count(c => c == '1') - t.Count(c => c == '1');
		var d1 = s1.Length - t1.Length;
		if (d1 < 0 || d1 % 2 != 0) { Console.WriteLine(-1); return; }
		d1 /= 2;

		var r = 0L;
		// Span?
		var s1e = s1.AsEnumerable().GetEnumerator();
		s1e.MoveNext();
		var i = s1e.Current;

		if (t1.Length == 0)
		{
			Console.WriteLine(Enumerable.Range(0, s1.Length / 2).Sum(i => (long)s1[2 * i + 1] - s1[2 * i]));
			return;
		}

		foreach (var j in t1)
		{
			while (i < j)
			{
				if (d1 == 0) { Console.WriteLine(-1); return; }
				d1--;
				s1e.MoveNext();
				r += s1e.Current - i;
				s1e.MoveNext();
				i = s1e.Current;
			}
			r += i - j;
			if (s1e.MoveNext()) i = s1e.Current;
		}
		Console.WriteLine(r);
	}
}
