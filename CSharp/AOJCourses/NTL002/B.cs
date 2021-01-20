using System;

class B
{
	static void Main()
	{
		var h = Console.ReadLine().Split();
		var sa = h[0];
		var sb = h[1];

		var neg_a = sa[0] == '-';
		var neg_b = sb[0] == '-';

		var a = new int[sa.Length + (neg_a ? 0 : 1)];
		var b = new int[sb.Length + (neg_b ? 0 : 1)];

		for (int i = neg_a ? 1 : 0; i < sa.Length; i++)
		{
			var v = sa[i] - '0';
			a[sa.Length - 1 - i] = neg_a ? -v : v;
		}
		for (int i = neg_b ? 1 : 0; i < sb.Length; i++)
		{
			var v = sb[i] - '0';
			b[sb.Length - 1 - i] = neg_b ? v : -v;
		}

		var ab = a.Length >= b.Length ? Add(a, b) : Add(b, a);

		var neg_ab = false;
		var zero = true;
		for (int i = 0; i < ab.Length; i++)
		{
			if (ab[i] == 0) continue;
			neg_ab = ab[i] < 0;
			zero = false;
			break;
		}
		Array.Reverse(ab);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		if (zero)
		{
			Console.Write(0);
		}
		else if (neg_ab)
		{
			Console.Write('-');
			var none = true;
			for (int i = 0; i < ab.Length; i++)
			{
				if (none && ab[i] == 0) continue;
				none = false;
				Console.Write(-ab[i]);
			}
		}
		else
		{
			var none = true;
			for (int i = 0; i < ab.Length; i++)
			{
				if (none && ab[i] == 0) continue;
				none = false;
				Console.Write(ab[i]);
			}
		}
		Console.WriteLine();
		Console.Out.Flush();
	}

	// a.Length >= b.Length
	static int[] Add(int[] a, int[] b)
	{
		for (int i = 0; i < b.Length; i++)
			a[i] += b[i];

		var negative = false;
		for (int i = a.Length - 1; i >= 0; i--)
		{
			if (a[i] == 0) continue;
			negative = a[i] < 0;
			break;
		}

		if (negative)
		{
			for (int i = 0; i < a.Length - 1; i++)
			{
				if (a[i] <= 0)
				{
					a[i + 1] += a[i] / 10;
					a[i] %= 10;
				}
				else
				{
					a[i + 1]++;
					a[i] -= 10;
				}
			}
		}
		else
		{
			for (int i = 0; i < a.Length - 1; i++)
			{
				if (a[i] >= 0)
				{
					a[i + 1] += a[i] / 10;
					a[i] %= 10;
				}
				else
				{
					a[i + 1]--;
					a[i] += 10;
				}
			}
		}
		return a;
	}
}
