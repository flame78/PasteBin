using System;

namespace ZeroTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new System.Text.StringBuilder();
            var di = new System.IO.DirectoryInfo(@".\");
            var dir = di.GetFiles("*.cs", System.IO.SearchOption.TopDirectoryOnly);

            foreach (var file in dir)
            {
                Console.WriteLine(  file.Name+ "<----\n");
                var reader = new System.IO.StreamReader(file.FullName);

                using (reader)
                {
                    while (reader.EndOfStream != true)
                    {
                        Console.WriteLine(  reader.ReadLine());
                        
                    }
                }    
               
        
            }

            Console.WriteLine(  
            sb.ToString());
        }
    }
}
