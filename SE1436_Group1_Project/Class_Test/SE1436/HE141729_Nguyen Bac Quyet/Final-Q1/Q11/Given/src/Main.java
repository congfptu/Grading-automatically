// ======= DO NOT EDIT MAIN FUNCTION ============
import java.io.*;
class Main
{
    /**
     * Count the number of words in s that contain at least a vowel letter
     * @param s - an input string 
     * @return - number of words in an input string that contain at least a vowel letter
     */
   public static int f1(String s){
        String[] listWords = s.toLowerCase().split("");
        int count=0;
        for (String o : listWords) {
            if(o.contains("u")||o.contains("e")||o.contains("o")||o.contains("a")||o.contains("i")){
               count++;
            }
        }
        return count;
   }
   public static void main(String args[]) throws Exception
   {
       BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
       String s = "";
       System.out.print("Enter a string s: ");
       s = in.readLine();
       System.out.println("OUTPUT:");
       System.out.println(f1(s));
   }
 }
