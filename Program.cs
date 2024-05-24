using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class FileOrganizer
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: FileOrganizer <sourceDir> <destDir> [--dry-run] [--overwrite] [--config <configFile>]");
            return;
        }

        string sourceDir = args[0];
        string destDir = args[1];
        bool dryRun = args.Contains("--dry-run");
        bool overwrite = args.Contains("--overwrite");
        string configFile = args.SkipWhile(arg => arg != "--config").Skip(1).FirstOrDefault();

        if (!Directory.Exists(sourceDir))
        {
            Console.WriteLine($"Error: Source directory '{sourceDir}' does not exist.");
            return;
        }

        if (!Directory.Exists(destDir))
        {
            Console.WriteLine($"Error: Destination directory '{destDir}' does not exist.");
            return;
        }

        var fileTypes = new Dictionary<string, List<string>>();

        if (!string.IsNullOrEmpty(configFile))
        {
            try
            {
                var lines = File.ReadAllLines(configFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        var folder = parts[0].Trim();
                        var extensions = parts[1].Split(',').Select(ext => ext.Trim().ToLower()).ToList();
                        fileTypes[folder] = extensions;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration file '{configFile}': {ex.Message}");
                return;
            }
        }
        else
        {
            fileTypes = new Dictionary<string, List<string>>
            {
                { "Documents", new List<string> { ".pdf", ".docx", ".txt", ".doc", ".odt", ".rtf" } },
                { "Images", new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg" } },
                { "Spreadsheets", new List<string> { ".xlsx", ".xls", ".csv", ".ods" } },
                { "Presentations", new List<string> { ".pptx", ".ppt", ".odp" } },
                { "Audio", new List<string> { ".mp3", ".wav", ".flac", ".aac", ".ogg" } },
                { "Videos", new List<string> { ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv" } },
                { "Archives", new List<string> { ".zip", ".rar", ".7z", ".tar", ".gz" } }
            };
        }

        foreach (var folder in fileTypes.Keys)
        {
            Directory.CreateDirectory(Path.Combine(destDir, folder));
        }

        int totalFiles = Directory.GetFiles(sourceDir).Length;
        int processedFiles = 0;

        foreach (var filePath in Directory.GetFiles(sourceDir))
        {
            try
            {
                string fileExtension = Path.GetExtension(filePath).ToLower();
                bool fileMoved = false;

                foreach (var folder in fileTypes)
                {
                    if (folder.Value.Contains(fileExtension))
                    {
                        string destPath = Path.Combine(destDir, folder.Key, Path.GetFileName(filePath));

                        if (dryRun)
                        {
                            Console.WriteLine($"Dry Run: '{Path.GetFileName(filePath)}' will move to '{folder.Key}' folder.");
                        }
                        else
                        {
                            if (File.Exists(destPath))
                            {
                                if (overwrite)
                                {
                                    File.Delete(destPath);
                                }
                                else
                                {
                                    destPath = GetUniqueFilePath(destPath);
                                }
                            }

                            File.Move(filePath, destPath);
                        }

                        fileMoved = true;
                        break;
                    }
                }

                if (!fileMoved)
                {
                    string otherFolder = Path.Combine(destDir, "Others");
                    Directory.CreateDirectory(otherFolder);
                    string destPath = Path.Combine(otherFolder, Path.GetFileName(filePath));

                    if (dryRun)
                    {
                        Console.WriteLine($"Dry Run: '{Path.GetFileName(filePath)}' will move to 'Others' folder.");
                    }
                    else
                    {
                        if (File.Exists(destPath))
                        {
                            if (overwrite)
                            {
                                File.Delete(destPath);
                            }
                            else
                            {
                                destPath = GetUniqueFilePath(destPath);
                            }
                        }

                        File.Move(filePath, destPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file '{filePath}': {ex.Message}");
            }

            processedFiles++;
            Console.WriteLine($"Progress: {processedFiles}/{totalFiles} files processed.");
        }

        if (dryRun)
        {
            Console.WriteLine("Dry run completed. No files were moved.");
        }
        else
        {
            Console.WriteLine("Files have been organized successfully.");
        }
    }

    static string GetUniqueFilePath(string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string extension = Path.GetExtension(filePath);
        int count = 1;

        string newFilePath;
        do
        {
            newFilePath = Path.Combine(directory, $"{fileName} ({count++}){extension}");
        } while (File.Exists(newFilePath));

        return newFilePath;
    }
}
