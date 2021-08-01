// ======= DO NOT EDIT MAIN FUNCTION ============

import java.util.*;
import java.io.*;

class Main {

    /**
     * Increment the last prime number in a by 1
     *
     * @param a - a list of whole numbers
     */
    public static void f1(ArrayList<Integer> a) {
        int n = 0;
        int index = 0;
        for (int i = 0; i < a.size(); i++) {
            if (isPrime(a.get(i))) {
                n = a.get(i);
                index = i;
            }
        }
        n += 1;
        a.set(index, n);
    }

    public static boolean isPrime(int n) {
        if (n <= 1) {
            return false;
        }
        for (int i = 2; i < Math.sqrt(n); i++) {
            if (n % i == 0) {
                return false;
            }
        }
        return true;
    }

    public static void main(String args[]) throws Exception {
        BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
        ArrayList<Integer> a = new ArrayList<>();
        System.out.print("Enter number of item: ");
        int n = Integer.parseInt(in.readLine());
        for (int i = 0; i < n; i++) {
            System.out.print("Number " + i + " (th): ");
            a.add(Integer.parseInt(in.readLine()));
        }
        f1(a);
        System.out.println("OUTPUT:");
        for (int i = 0; i < n; i++) {
            System.out.println(a.get(i));
        }
    }
}
