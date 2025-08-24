import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.PriorityQueue;

public class E {
	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	static long[] readL() throws IOException {
		return Arrays.stream(in.readLine().split(" ")).mapToLong(s -> Long.parseLong(s)).toArray();
	}

	static long[] readL(int n) throws IOException {
		var r = new long[n];
		for (int i = 0; i < n; i++)
			r[i] = Long.parseLong(in.readLine());
		return r;
	}

	public static void main(String[] args) throws IOException {
		var t = Integer.parseInt(in.readLine());
		var r = new String[t];
		for (int i = 0; i < t; i++)
			r[i] = solve().toString();
		System.out.println(String.join("\n", r));
	}

	static Object solve() throws IOException {
		var n = Integer.parseInt(in.readLine());
		var a = readL(2 * n);

		var r = a[0];
		var q = new PriorityQueue<Long>();

		for (int i = 1; i < n; i++) {
			q.offer(-a[2 * i - 1]);
			q.offer(-a[2 * i]);
			r += -q.remove();
		}
		return r;
	}
}
