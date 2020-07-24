using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var s = Console.ReadLine();
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine()).ToArray();

		var f = false;
		var l1 = new List<char>();
		var l2 = new List<char>();

		foreach (var q in qs)
			if (q == "1")
				f = !f;
			else if (q[2] == '1' ^ f)
				l1.Add(q[4]);
			else
				l2.Add(q[4]);

		l1.Reverse();
		var r = l1.Concat(s).Concat(l2);
		if (f) r = r.Reverse();
		Console.WriteLine(new string(r.ToArray()));
	}
}
