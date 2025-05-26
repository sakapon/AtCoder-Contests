import java.util.PriorityQueue;
import java.util.Scanner;
import java.util.stream.IntStream;

public class E {
	static Scanner sc = new Scanner(System.in);

	static long[] readL(int n) {
		return IntStream.range(0, n).mapToLong(i -> sc.nextLong()).toArray();
	}

	public static void main(String[] args) {
		var t = sc.nextInt();
		var r = new String[t];
		for (int i = 0; i < t; i++)
			r[i] = solve().toString();
		System.out.println(String.join("\n", r));
	}

	static Object solve() {
		var n = sc.nextInt();
		var a = readL(2 * n);

		var r = a[0];
		var q = new PriorityQueue<Long>();

		for (int i = 1; i < n; i++) {
			var v = a[2 * i - 1];
			q.offer(-v);
			v = a[2 * i];
			q.offer(-v);
			r += -q.remove();
		}
		return r;
	}
}
