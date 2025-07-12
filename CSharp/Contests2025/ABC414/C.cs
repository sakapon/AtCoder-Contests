using CoderLib8.Numerics;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = int.Parse(Console.ReadLine());
		var n = long.Parse(Console.ReadLine());

		var r = 0L;

		for (int i = 1; i < 1000000; i++)
		{
			var s = i.ToString();
			var sr = string.Join("", s.Reverse());

			Check(s + sr[1..]);
			Check(s + sr);
		}
		return r;

		void Check(string s2)
		{
			var j = long.Parse(s2);
			if (j > n) return;
			var t = j.ConvertAsString(a);
			if (!IsPalindrome(t)) return;
			r += j;
		}
	}

	static bool IsPalindrome(string s) => IsPalindrome(s, 0, s.Length);
	static bool IsPalindrome(string s, int l, int r)
	{
		for (--l; ++l < --r;) if (s[l] != s[r]) return false;
		return true;
	}
}
