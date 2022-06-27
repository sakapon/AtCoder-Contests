using System;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		var set = new bool[13][];
		for (int i = 0; i < 13; i++)
			set[i] = new bool[1 << i << i];

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
			if (q[0][0] == 'i')
				set[q[1].Length][Hash(q[1])] = true;
			else
				Console.WriteLine(set[q[1].Length][Hash(q[1])] ? "yes" : "no");
		Console.Out.Flush();
	}

	static int Hash(string s)
	{
		var r = 0;
		for (int i = 0; i < s.Length; i++)
		{
			var v = s[i] & 3;
			if (s[i] == 'G') v = 2;
			r |= v << i << i;
		}
		return r;
	}
}
