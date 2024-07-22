class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, y) = Read3();
		var a = new bool[h].SelectMany(_ => Read()).ToArray();

		var n = h * w;
		int ToVertexId(int i, int j) => w * i + j;
		(int i, int j) FromVertexId(int v) => (v / w, v % w);

		List<int> GetAllNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			if (j > 0) l.Add(v - 1);
			if (j + 1 < w) l.Add(v + 1);
			if (i > 0) l.Add(v - w);
			if (i + 1 < h) l.Add(v + w);
			return l;
		}

		const int max = 100000;
		var sinked = Array.ConvertAll(new bool[max + 1], _ => new List<int>());

		for (int j = 0; j < w; j++)
		{
			var v = ToVertexId(0, j);
			sinked[a[v]].Add(v);
			v = ToVertexId(h - 1, j);
			sinked[a[v]].Add(v);
		}
		for (int i = 1; i < h - 1; i++)
		{
			var v = ToVertexId(i, 0);
			sinked[a[v]].Add(v);
			v = ToVertexId(i, w - 1);
			sinked[a[v]].Add(v);
		}

		var r = new int[y];
		var s = n;
		var u = new bool[n];
		var q = new Queue<int>();

		for (int k = 1; k <= y; k++)
		{
			foreach (var sv in sinked[k])
			{
				if (u[sv]) continue;
				s--;
				u[sv] = true;
				q.Enqueue(sv);

				while (q.Count > 0)
				{
					var v = q.Dequeue();

					foreach (var nv in GetAllNexts(v))
					{
						if (u[nv]) continue;

						if (a[nv] <= k)
						{
							s--;
							u[nv] = true;
							q.Enqueue(nv);
						}
						else
						{
							sinked[a[nv]].Add(nv);
						}
					}
				}
			}

			r[k - 1] = s;
		}

		return string.Join("\n", r);
	}
}
