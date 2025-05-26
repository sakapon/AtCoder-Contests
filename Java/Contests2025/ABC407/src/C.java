import java.util.Scanner;
import java.util.stream.IntStream;

public class C {
	static Scanner sc = new Scanner(System.in);

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var s = sc.next();
		var n = s.length();

		var t = s + "0";
		return n + IntStream.range(0, n).map(i -> (t.charAt(i) - t.charAt(i + 1) + 10) % 10).sum();
	}
}
