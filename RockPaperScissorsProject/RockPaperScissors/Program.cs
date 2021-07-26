using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            int numRounds;
            int compWins, ties, userWins;
            string inputRounds;
            string inputChoice;
            int compChoice;
            int userChoice;
            
            while(true)
            {   
                // Reset score for each new round
                compWins = 0;
                ties = 0;
                userWins = 0;

                ResetToBaseColor();
                Random random = new Random();
                Console.Write("Welcome to RockPaperScissors!\nHow many rounds would you like to play(1-10)? ");
                inputRounds = Console.ReadLine();

                if(int.TryParse(inputRounds,out numRounds))
                {
                    if(numRounds >= 1 && numRounds <= 10)
                    {
                        for(int i = 0; i < numRounds; i++)
                        {
                            while(true)
                            {
                                Console.WriteLine("1 - Rock\t 2 - Paper\t 3 - Scissors");
                                Console.Write("Please enter the digit that corresponds to rock, paper, or scissors to begin the game: ");
                                inputChoice = Console.ReadLine();

                                if(int.TryParse(inputChoice, out userChoice))
                                {
                                    if(userChoice == 1 || userChoice == 2 || userChoice ==3)
                                    {
                                        break;
                                    }
                                }
                            }
                            compChoice = random.Next(1, 4);
                            
                            // Difference between user and computer choices determines who was winner
                            int winner = userChoice - compChoice;

                            ChangeConsoleColor("yellow");
                            // No difference means tie
                            if(winner == 0)
                            {
                                ties++;
                                Console.WriteLine("The winner of this round is...IT'S A TIE!");
                            }
                            else if(winner == -1 || winner == 2 )
                            {
                                compWins++;
                                Console.WriteLine("The winner of this round is...COMPUTER!");
                            }
                            else
                            {
                                userWins++;
                                Console.WriteLine("The winner of this round is...YOU!");
                            }

                            Console.WriteLine($"\nUser Wins:{userWins}\nComputer Wins:{compWins}\nTies:{ties}\n");
                            // Change color back to white
                            ResetToBaseColor();
                        }

                        ChangeConsoleColor("green");

                        if (compWins == userWins)
                        {
                            Console.WriteLine("\nThe overall winner is...IT'S A TIE!!!");
                        }
                        else if(compWins > userWins)
                        {
                            Console.WriteLine("\nThe overall winner is...COMPUTER!!!");
                        }
                        else
                        {
                            Console.WriteLine("\nThe overall winner is...YOU!!!");
                        }

                        ResetToBaseColor();
                        Console.WriteLine("Would you like to play again? Enter 'y' to play again or anything else to quit");
                        string answer = Console.ReadLine();

                        if(answer.ToLower() == "y")
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // User Input not in range
                        ChangeConsoleColor("red");
                        Console.WriteLine("Error: You can only play between 1-10 rounds");
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
            Console.WriteLine("Thanks for Playing!");
        }

        // Changes console color to color of inputted string
        static void ChangeConsoleColor(string color)
        {
            switch (color.ToLower())
            {
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
        }
        // Resets color back to base color white
        static void ResetToBaseColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
