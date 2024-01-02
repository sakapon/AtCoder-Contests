using System;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s1 = Array.ConvertAll(new bool[4], _ => Console.ReadLine());
		var s2 = Array.ConvertAll(new bool[4], _ => Console.ReadLine());
		var s3 = Array.ConvertAll(new bool[4], _ => Console.ReadLine());

		var p1 = new Polyomino(s1);
		var p2 = new Polyomino(s2);
		var p3 = new Polyomino(s3);
		if (p1.a.Length + p2.a.Length + p3.a.Length != 16) return false;

		for (int k1 = 0; k1 < 4; k1++)
		{
			for (int i1 = p1.h; i1 <= 4; i1++)
			{
				for (int j1 = p1.w; j1 <= 4; j1++)
				{
					var a1 = Array.ConvertAll(p1.a, i => i + 4 * (4 - i1) + 4 - j1);

					for (int k2 = 0; k2 < 4; k2++)
					{
						for (int i2 = p2.h; i2 <= 4; i2++)
						{
							for (int j2 = p2.w; j2 <= 4; j2++)
							{
								var a2 = Array.ConvertAll(p2.a, i => i + 4 * (4 - i2) + 4 - j2);
								var a12 = a1.Concat(a2).ToArray();
								if (a12.Distinct().Count() != a12.Length) continue;

								for (int k3 = 0; k3 < 4; k3++)
								{
									for (int i3 = p3.h; i3 <= 4; i3++)
									{
										for (int j3 = p3.w; j3 <= 4; j3++)
										{
											var a3 = Array.ConvertAll(p3.a, i => i + 4 * (4 - i3) + 4 - j3);
											if (a12.Concat(a3).Distinct().Count() == 16) return true;
										}
									}
									p3 = p3.Rotate();
								}
							}
						}
						p2 = p2.Rotate();
					}
				}
			}
			p1 = p1.Rotate();
		}

		return false;
	}

	public class Polyomino
	{
		static readonly int[] r16 = Enumerable.Range(0, 16).ToArray();
		static readonly int[] map = new[] { 12, 8, 4, 0, 13, 9, 5, 1, 14, 10, 6, 2, 15, 11, 7, 3 };

		// # のあるインデックス
		public int[] a;
		public int h, w;

		public Polyomino(int[] a)
		{
			var iMin = a.Min(i => i / 4);
			var iMax = a.Max(i => i / 4);
			h = iMax - iMin + 1;

			var jMin = a.Min(i => i % 4);
			var jMax = a.Max(i => i % 4);
			w = jMax - jMin + 1;

			this.a = Array.ConvertAll(a, i => i - 4 * iMin - jMin);
		}

		public Polyomino(string[] s) : this(Array.FindAll(r16, i => s[i / 4][i % 4] == '#')) { }

		public Polyomino Rotate() => new Polyomino(Array.ConvertAll(a, i => map[i]));
	}
}
