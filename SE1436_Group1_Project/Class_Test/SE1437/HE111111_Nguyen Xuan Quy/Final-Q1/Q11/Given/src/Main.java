// ======= DO NOT EDIT MAIN FUNCTION ============

import java.io.*;

class Main {

    /**
     * Count the number of words in s that contain at least a vowel letter
     *
     * @param s - an input string
     * @return - number of words in an input string that contain at least a
     * vowel letter
     */
    public static int f1(String s) {
        String[] a = {"a", "e", "i", "o", "u"};
        int count = 0;
        String ss[] = s.split(" ");
        int vowel = 0;
        for (String s1 : ss) {
            for (String a1 : a) {
                if (s1.contains(a1)) {
                    count++;
                }
            }
            if (count > 0) {
                vowel++;
            }
        }

        return vowel;
    }

    public static void main(String args[]) throws Exception {
        BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
        String s = "";
        System.out.print("Enter a string s: ");
        s = in.readLine();
        System.out.println("OUTPUT:");
        System.out.println(f1(s));
    }
}
