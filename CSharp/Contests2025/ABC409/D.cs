class D
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().ToCharArray();

		var i = 0;
		while (i < n - 1 && s[i] <= s[i + 1])
		{
			i++;
		}
		while (i < n - 1 && s[i] >= s[i + 1])
		{
			(s[i], s[i + 1]) = (s[i + 1], s[i]);
			i++;
		}
		return new string(s);
	}
}
