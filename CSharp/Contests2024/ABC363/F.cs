class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		const string Invalid = "-1";
		var d = new Dictionary<long, string>();
		return Try(n);

		string Try(long n)
		{
			if (d.ContainsKey(n)) return d[n];

			var ns = n.ToString();
			if (!ns.Contains('0') && IsPalindrome(ns)) return d[n] = ns;

			for (int x = 2; x < 1000000; x++)
			{
				if (n % x != 0) continue;
				var xs = x.ToString();
				if (xs.Contains('0')) continue;

				var cs = xs.ToCharArray();
				Array.Reverse(cs);
				var xs_ = new string(cs);
				var x_ = int.Parse(xs_);

				var n2 = n / x;
				if (n2 % x_ != 0) continue;
				n2 /= x_;

				var r = Try(n2);
				if (r == Invalid) continue;
				return d[n] = $"{xs}*{r}*{xs_}";
			}

			return d[n] = Invalid;
		}
	}

	static bool IsPalindrome(string s)
	{
		for (int i = 0; i < s.Length; ++i) if (s[i] != s[s.Length - 1 - i]) return false;
		return true;
	}
}
