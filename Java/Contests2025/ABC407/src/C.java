import java.util.Scanner;

public class C {
	static Scanner sc = new Scanner(System.in);

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var s = sc.next();
		var n = s.length();

		var r = n;
		s += "0";

		for (int i = 0; i < n; i++)
			r += (s.charAt(i) - s.charAt(i + 1) + 10) % 10;
		return r;
	}
}
