using System;

class J2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var d = ((decimal)n - 1) / 2;

		(int, int) Map(int i, int j, Transform t)
		{
			var (ni, nj) = t.Execute(i - d, j - d);
			return ((int, int))(ni + d, nj + d);
		}

		var u = NewArray2<int>(n, n);
		var e = new Transform(1, 2);
		var t = e;

		Array.Reverse(qs);
		foreach (var q in qs)
		{
			if (q[0] == "1")
			{
				var x = int.Parse(q[1]) - 1;
				var y = int.Parse(q[2]) - 1;

				var (nx, ny) = Map(x, y, t);
				u[nx][ny] = 1 - u[nx][ny];
			}
			else if (q[0] == "2")
			{
				t = e.Rotate(q[1] != "A").ByLeft(t);
			}
			else
			{
				t = e.Reverse(q[1] == "A").ByLeft(t);
			}
		}

		foreach (var r in u)
			Console.WriteLine(string.Join("", r));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	struct Transform
	{
		// 1: x, 2: y
		public int X, Y;
		public Transform(int x, int y) { X = x; Y = y; }

		public Transform Rotate(bool positive)
		{
			if (positive) return new Transform(-Y, X);
			return new Transform(Y, -X);
		}

		public Transform Reverse(bool x)
		{
			if (x) return new Transform(-X, Y);
			return new Transform(X, -Y);
		}

		public Transform Inverse()
		{
			if (Math.Abs(X) == 1) return this;
			if (X * Y > 0) return this;
			return new Transform(-X, -Y);
		}

		public Transform ByLeft(Transform t)
		{
			var nx = Math.Sign(t.X) * (Math.Abs(t.X) == 1 ? X : Y);
			var ny = Math.Sign(t.Y) * (Math.Abs(t.Y) == 1 ? X : Y);
			return new Transform(nx, ny);
		}

		public (decimal, decimal) Execute(decimal i, decimal j)
		{
			var ni = Math.Sign(X) * (Math.Abs(X) == 1 ? i : j);
			var nj = Math.Sign(Y) * (Math.Abs(Y) == 1 ? i : j);
			return (ni, nj);
		}
	}
}
