using System;
using System.Collections.Generic;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var d = new Map<string, string>("0");
		for (int i = 0; i < n; i++)
		{
			var q = Console.ReadLine().Split();
			if (q[0] == "0")
				d[q[1]] = q[2];
			else if (q[0] == "1")
				Console.WriteLine(d[q[1]]);
			else
				d.Remove(q[1]);
		}
	}
}

class Map<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public Map(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
