class C3
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var a = new int[n];
		int l = 0, r = n - 1;
		var isLeft = false;

		for (int k = n; k > 0; k--)
		{
			if (s[k - 1] == 'o') isLeft = !isLeft;
			if (isLeft) a[l++] = k;
			else a[r--] = k;
		}
		return string.Join(" ", a);
	}
}
