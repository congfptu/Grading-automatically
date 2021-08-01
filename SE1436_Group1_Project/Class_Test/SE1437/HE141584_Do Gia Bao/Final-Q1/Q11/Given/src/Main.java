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
        int count = 0;
        int counted = 0;
        int total = 0;
        s = s.toLowerCase();
        //Alibaba ubaka kc
        for (int i = 0; i < s.length(); i++) {
            if (s.charAt(i) == 'u' || s.charAt(i) == 'e' || s.charAt(i) == 'o' || s.charAt(i) == 'a' || s.charAt(i) == 'i') {
                count++;
            }
           
        }
        return count;
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
