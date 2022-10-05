using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace questions
{
    public class Program
    {
        public static string Ask(questionClass Q) => Q.AskType == 0 ? AskQuestion(Q) : AskUserInput(Q);

        public static string AskQuestion(questionClass Question)
        {
            List<string> Options = new List<string>();
            Options.Add($"Question: {Question.Text}"); ;
            Options.AddRange(Question.Options);
            Console.WriteLine("Select column: ");
            int columnSelected = 1;
            Console.Clear();
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{(i == 0 ? "" : $"{i})")} {(i == columnSelected ? "> " : "  ")}{Options[i]}");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        columnSelected = columnSelected + 1 >= Options.Count ? columnSelected : columnSelected + 1;
                        break;

                    case ConsoleKey.UpArrow:
                        columnSelected = columnSelected - 1 <= 0 ? 0 : columnSelected - 1;
                        break;
                }

                Console.Clear();

                for (int i = 0; i < Options.Count; i++)
                {
                    Console.WriteLine($"{(i == 0 ? "" : $"{i})")} {(i == columnSelected ? "> " : "  ")}{Options[i]}");
                }

                keyInfo = Console.ReadKey(true);
            }

            return Options[columnSelected];
        }

        public static string AskUserInput(questionClass Question)
        {
            Console.Clear();
            Console.WriteLine($"Question: {Question.Text}\n Your Answer:");
            Console.Write("Answer >> ");
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            List<questionClass> questionsToWrite = new List<questionClass>();
            questionsToWrite.Add(new questionClass("2+2", "4", 0, "1", "2", "3", "Bebra", "4", "5", "6", "7", "8", "9"));
            questionsToWrite.Add(new questionClass("2+3", "5", 1, "1", "2", "3", "Bebra", "4", "5", "666", "7", "8", "9"));
            questionsToWrite.Add(new questionClass("2+4", "6", 0, "1", "2", "3", "Bebra", "4", "5", "6", "7", "8", "9"));
            questionsToWrite.Add(new questionClass("2+5", "7", 0, "1", "2", "3", "Bebra", "4", "5", "6", "7", "8", "9"));
            questionsToWrite.Add(new questionClass("2+6", "8", 0, "1", "2", "3", "Bebra", "4", "5", "6", "7", "8", "9"));
            questionsToWrite.Add(new questionClass("2+7", "9", 0, "1", "2", "3", "Bebra", "4", "5", "6", "7", "8", "9"));
            File.WriteAllText("Questions.txt", JsonConvert.SerializeObject(questionsToWrite));

            var questions = JsonConvert.DeserializeObject<List<questionClass>>(File.ReadAllText(@"C:\Users\bebra\source\repos\questions\questions\bin\Debug\questions.txt"));
            string[] user_answers = questions.Select(x => Ask(x)).ToArray();
            for (int i = 0; i < questions.Count; i++)
                Console.WriteLine($"{questions[i].Text}: {user_answers[i]} Correct Answer: {questions[i].Answer}");
            Console.WriteLine($"You scored: {(100 / questions.Count) * user_answers.Where((x, indexer) => x == questions[indexer].Answer).Count()}%");
            Console.ReadKey();
        }
    }
}
