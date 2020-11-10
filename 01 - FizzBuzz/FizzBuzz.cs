/*
  FizzBuzz is a very simple coding problem:
  Iterate 1 through 100.  When a number is divisible by 3 say "Fizz",
  when a number is divisible by 5 say "Buzz", and when it is divisible
  by both 3 and 5 say "FizzBuzz".
*/

using System;
					
public class FizzBuzz {

    public static void Main() {
        for (int i = 1; i <= 100; i++) {
            Console.Write(i + " ");  

            if (i % 3 == 0) {
                Console.Write("Fizz");
            }

            if (i % 5 == 0) {
                Console.Write("Buzz");
            }

            Console.WriteLine();
        }
		Console.WriteLine("Finished!");
    }
}