class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var l = new List<int>();
		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				l.Add(q[1]);
				l.Sort();
			}
			else
			{
				Console.WriteLine(l[0]);
				l.RemoveAt(0);
			}
		}
	}
}
