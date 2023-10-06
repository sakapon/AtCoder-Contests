using System;
using System.Linq;

class D2
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

		for (int k1 = 0; k1 < 4; k1++)
		{
			for (int i1 = p1.h; i1 <= 4; i1++)
			{
				for (int j1 = p1.w; j1 <= 4; j1++)
				{
					var x1 = p1.x << 4 * (4 - i1) + 4 - j1;

					for (int k2 = 0; k2 < 4; k2++)
					{
						for (int i2 = p2.h; i2 <= 4; i2++)
						{
							for (int j2 = p2.w; j2 <= 4; j2++)
							{
								var x2 = p2.x << 4 * (4 - i2) + 4 - j2;

								for (int k3 = 0; k3 < 4; k3++)
								{
									for (int i3 = p3.h; i3 <= 4; i3++)
									{
										for (int j3 = p3.w; j3 <= 4; j3++)
										{
											var x3 = p3.x << 4 * (4 - i3) + 4 - j3;
											if (x1 + x2 + x3 == 65535) return true;
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

		// # のあるインデックスのビット表現
		public int x;
		public int h, w;

		public Polyomino(int x)
		{
			var a = Array.FindAll(r16, i => (x & (1 << i)) != 0);

			var iMin = a.Min(i => i / 4);
			var iMax = a.Max(i => i / 4);
			h = iMax - iMin + 1;

			var jMin = a.Min(i => i % 4);
			var jMax = a.Max(i => i % 4);
			w = jMax - jMin + 1;

			this.x = x >> 4 * iMin + jMin;
		}

		public Polyomino(string[] s) : this(r16.Where(i => s[i / 4][i % 4] == '#').Sum(i => 1 << i)) { }

		public Polyomino Rotate()
		{
			var a = Array.FindAll(r16, i => (x & (1 << i)) != 0);
			return new Polyomino(a.Select(i => map[i]).Sum(i => 1 << i));
		}
	}
}
