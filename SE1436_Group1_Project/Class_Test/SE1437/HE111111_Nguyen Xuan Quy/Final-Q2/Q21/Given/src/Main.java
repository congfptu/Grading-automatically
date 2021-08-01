// ======= DO NOT EDIT MAIN FUNCTION ============
import java.util.*;
import java.io.*;
class Main
{    
    /**
     * Increment the last prime number in a by 1
     * @param a - a list of whole numbers     
     */ 
    public static boolean prime (int n){
        if(n %2 ==0 && n !=2){
            return false;
        }else if(n==2) return true;
        
        for (int i = 2; i < n; i++) {
            if(n%i == 0){
                return false;
            }
        }
        return true;
    }
   public static void f1(ArrayList<Integer> a){     
      
        for (int i = 0; i < a.size(); i++) {
           if(prime(i)==true){
               a.lastIndexOf(i+1);
           }
       }
   }
   public static void main(String args[]) throws Exception
   {
       BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
       ArrayList<Integer> a = new ArrayList<>();
       System.out.print("Enter number of item: ");
       int n = Integer.parseInt(in.readLine());
       for(int i = 0; i < n; i++){
           System.out.print("Number " + i + " (th): ");
           a.add(Integer.parseInt(in.readLine()));
       }       
       f1(a);
       System.out.println("OUTPUT:");
       for(int i = 0; i < n; i++){
           System.out.println(a.get(i));
       }
   }
 }
