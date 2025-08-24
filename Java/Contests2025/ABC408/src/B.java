import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

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

	static List<String> mapToString(int[] a) {
		return Arrays.stream(a).mapToObj(Integer::toString).collect(Collectors.toList());
	}

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var n = Integer.parseInt(readLine());
		var a = read();

		var r = Arrays.stream(a).distinct().sorted().toArray();
		return r.length + "\n" + String.join(" ", mapToString(r));
	}
}
