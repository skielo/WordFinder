using Finder.Business.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Finder.ColsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stay = true;
                // Start!
                while (stay)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("###################################################");
                    Console.WriteLine("####### Welcome to the main application ###########");
                    Console.WriteLine("###################################################");
                    Console.WriteLine("Please provide the path to the file that contains the board:");
                    var pathToFile = Console.ReadLine();
                    Console.WriteLine($"Wait until we get the file from {pathToFile}");
                    if (File.Exists(pathToFile))
                    {
                        var lines = File.ReadAllLines(pathToFile);
                        var finder = new WordFinder(lines);
                        Console.WriteLine($"The file {pathToFile} has been processed successfully");
                        Console.WriteLine("Please provide the path to the file that contains the words stream:");
                        pathToFile = Console.ReadLine();
                        if (File.Exists(pathToFile))
                        {
                            lines = File.ReadAllLines(pathToFile);
                            var tokens = lines[0].Split(' ');
                            Console.WriteLine($"The file {pathToFile} has been processed successfully");
                            var results = finder.Find(tokens);
                            Console.WriteLine("Starting to find the words.. let's have fun");
                            Console.WriteLine("Those are the top 10 most repeated words:");
                            foreach (var item in results)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine("Thanks for using the word finder tool");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"The file does not exists: {pathToFile}. Please provide a valid path.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"The file does not exists: {pathToFile}. Please provide a valid path.");
                        Console.ResetColor();
                    }
                    
                    Console.WriteLine("Do you want to finish (Y/N):");
                    if (Console.ReadLine().ToUpper() == "Y")
                        stay = false;
                }

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ups, something went extremily wrong. Please try again");
            }
        }
    }
}
