import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;

public class B {

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

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var n = Integer.parseInt(readLine());
		var a = read();

		for (int x = n + 1; x >= 0; x--) {
			var c = 0;
			for (var v : a) {
				if (v >= x)
					c++;
			}
			if (c >= x)
				return x;
		}
		return -1;
	}
}
