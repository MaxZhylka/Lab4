using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите путь к каталогу:");
        string directoryPath = Console.ReadLine();

        Console.WriteLine("Введите формат файлов (например, *.txt). Если хотите учесть все файлы, нажмите Enter:");
        string filesFormat = Console.ReadLine();

        if (Directory.Exists(directoryPath))
        {
            Console.WriteLine("Размеры подкаталогов:");

            CalculateDirectorySize(directoryPath, filesFormat);
        }
        else
        {
            Console.WriteLine("Каталог не существует.");
        }

        Console.WriteLine("Нажмите любую клавишу для выхода.");
        Console.ReadKey();
    }

    static long CalculateDirectorySize(string directoryPath, string filesFormat = "")
    {
        long totalSize = 0;

        try
        {
            string[] files;
            if (!string.IsNullOrEmpty(filesFormat))
            {
                files = Directory.GetFiles(directoryPath, filesFormat);
            }
            else
            {
                files = Directory.GetFiles(directoryPath);
            }

            foreach (string filePath in files)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                totalSize += fileInfo.Length;
            }

            string[] subDirectories = Directory.GetDirectories(directoryPath);
            foreach (string subDirectory in subDirectories)
            {
                long subDirectorySize = CalculateDirectorySize(subDirectory, filesFormat);
                totalSize += subDirectorySize;

                Console.WriteLine("{0}: {1} байт", subDirectory, subDirectorySize);
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Обработка исключения при отсутствии доступа к файлу/каталогу.
        }

        return totalSize;
    }
}
