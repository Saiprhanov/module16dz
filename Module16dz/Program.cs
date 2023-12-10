using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static string filePath = "sss.txt";


    
    static void Main()
    {
        Console.WriteLine("Welcome to the file manager!");


        
        while (true)
        {

            
            Console.WriteLine("\nSelect an action:");
            
            Console.WriteLine("1. Viewing the contents of a directory");
            Console.WriteLine("2. Create file/directory");
            Console.WriteLine("3. Delete file/directory");
            Console.WriteLine("4. Copy file/directory");
            Console.WriteLine("5. Moving file/directory");
            Console.WriteLine("6. Reading from a file");
            Console.WriteLine("7. Writing to a file");
            Console.WriteLine("0. Exit");

            int choice = GetUserChoice(0, 7);

            
            switch (choice)
            {
                case 1:
                    ViewDirectoryContents();
                    break;
                case 2:
                    CreateFileOrDirectory();
                    break;
                case 3:
                    DeleteFileOrDirectory();
                    break;
                case 4:
                    CopyFileOrDirectory();
                    break;
                case 5:
                    MoveFileOrDirectory();
                    break;
                case 6:
                    ReadFromFile();
                    break;
                case 7:
                    WriteToFile();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }
    }


    
    static void ViewDirectoryContents()
    {
        Console.Write("Enter the path to the directory: ");
        string path = Console.ReadLine();



        
        if (Directory.Exists(path))
        {



            
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            Console.WriteLine("\nFiles:");
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }

            Console.WriteLine("\nDirectories:");
            foreach (var directory in directories)
            {
                Console.WriteLine(Path.GetFileName(directory));
            }
        }
        else
        {
            Console.WriteLine("Directory is not found.");
        }
    }

    static void CreateFileOrDirectory()
    {
        Console.Write("Enter the path to create the file/directory: ");
        string path = Console.ReadLine();

        Console.Write("Enter (F - file, D - directory): ");
        char type = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        if (type == 'F')
        {
            File.Create(path).Close();
            Console.WriteLine("File is created");
        }
        else if (type == 'D')
        {
            Directory.CreateDirectory(path);
            Console.WriteLine("Directory is created.");
        }
        else
        {
            Console.WriteLine("Incorrect type selection.");
        }
    }

    static void DeleteFileOrDirectory()
    {
        Console.Write("Enter the path to delete the file/directory: ");
        string path = Console.ReadLine();

        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine("File is deleted.");
        }
        else if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Console.WriteLine("Directory is deleted.");
        }
        else
        {
            Console.WriteLine("The file or directory does not exist.");
        }
    }

    static void CopyFileOrDirectory()
    {
        Console.Write("Enter the path to copy the file/directory: ");
        string sourcePath = Console.ReadLine();

        Console.Write("Enter the path to the new location: ");
        string destinationPath = Console.ReadLine();

        if (File.Exists(sourcePath))
        {
            File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)), true);
            Console.WriteLine("File is copied.");
        }
        else if (Directory.Exists(sourcePath))
        {
            CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
            Console.WriteLine("Directory is copied.");
        }
        else
        {
            Console.WriteLine("The file or directory does not exist.");
        }
    }

    static void MoveFileOrDirectory()
    {
        Console.Write("Enter the path to move the file/directory: ");
        string sourcePath = Console.ReadLine();

        Console.Write("Enter the path to the new location: ");
        string destinationPath = Console.ReadLine();

        if (File.Exists(sourcePath))
        {
            File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
            Console.WriteLine("The file was moved successfully.");
        }
        else if (Directory.Exists(sourcePath))
        {
            Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
            Console.WriteLine("The directory was moved successfully.");
        }
        else
        {
            Console.WriteLine("The file or directory does not exist.");
        }
    }

    static void ReadFromFile()
    {
        Console.Write("Enter the path to read the file: ");
        string path = Console.ReadLine();

        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            Console.WriteLine($"\nThe contents of the file:\n{content}");
        }
        else
        {
            Console.WriteLine("The file does not exist.");
        }
    }

    static void WriteToFile()
    {
        Console.Write("Enter the path to the file to write to: ");
        string path = Console.ReadLine();

        Console.Write("Enter the text to write: ");
        string content = Console.ReadLine();

        File.WriteAllText(path, content);
        Console.WriteLine("The text has been successfully written to the file.");
    }

    static void CopyDirectory(string source, string destination)
    {
        Directory.CreateDirectory(destination);

        foreach (var file in Directory.GetFiles(source))
        {
            File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), true);
        }

        foreach (var subDirectory in Directory.GetDirectories(source))
        {
            CopyDirectory(subDirectory, Path.Combine(destination, Path.GetFileName(subDirectory)));
        }
    }

    static int GetUserChoice(int min, int max)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
        {
            Console.WriteLine("Incorrect input. Please enter a number from " + min + " to " + max + ".");
        }
        return choice;
    }
}
