using System;

class C
{
	static void Main()
	{
		var h = Console.ReadLine().Split();
		char[] sa = h[0].ToCharArray(), sb = h[1].ToCharArray();

		var neg_a = sa[0] == '-';
		var neg_b = sb[0] == '-';
		var neg_ab = neg_a ^ neg_b;

		var a = Array.ConvertAll(sa, c => c - '0');
		var b = Array.ConvertAll(sb, c => c - '0');
		Array.Reverse(a);
		Array.Reverse(b);
		if (neg_a) Array.Resize(ref a, a.Length - 1);
		if (neg_b) Array.Resize(ref b, b.Length - 1);

		var ab = Convolution(a, b);
		for (int i = 0; i < ab.Length - 1; i++)
		{
			ab[i + 1] += ab[i] / 10;
			ab[i] %= 10;
		}

		Array.Reverse(ab);
		var sab = string.Join("", ab);
		sab = sab.TrimStart('0');
		if (neg_ab && sab != "") sab = "-" + sab;
		if (sab == "") sab = "0";
		Console.WriteLine(sab);
	}

	static int[] Convolution(int[] a, int[] b)
	{
		var ab = new int[a.Length + b.Length - 1];
		for (int i = 0; i < a.Length; i++)
			for (int j = 0; j < b.Length; j++)
				ab[i + j] += a[i] * b[j];
		return ab;
	}
}
