import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class D {
	static class Tuple {
		public int u, v;

		public Tuple(int u, int v) {
			this.u = u;
			this.v = v;
		}
	}

	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	static String readLine() {
		try {
			return in.readLine();
		} catch (IOException ex) {
			return "";
		}
	}

	static int[] read() {
		return Arrays.stream(readLine().split(" ")).mapToInt(Integer::parseInt).toArray();
	}

	static long[] readL() {
		return Arrays.stream(readLine().split(" ")).mapToLong(Long::parseLong).toArray();
	}

	static int[] read(int n) {
		return IntStream.range(0, n).map(i -> Integer.parseInt(readLine())).toArray();
	}

	static Tuple[] readTuple(int n) {
		var r = new Tuple[n];
		for (int i = 0; i < n; i++) {
			var a = read();
			r[i] = new Tuple(a[0], a[1]);
		}
		return r;
	}

	static List<String> mapToString(int[] a) {
		return Arrays.stream(a).mapToObj(Integer::toString).collect(Collectors.toList());
	}

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var n = Integer.parseInt(readLine());
		var s = readLine();
		var a = read();

		return String.join("\n", mapToString(a));
	}
}
