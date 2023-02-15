////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Code description: The purpose of this program is to convert all the files in a given directory into a different
// format. 
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Revision history:
//  |-------------------|---------------|-----------------------------------|
//  | Date Modified:    | Modified by:  | Notes:                            |
//  |-------------------|---------------|-----------------------------------|
//  | 15/02/2023        | K Robinson    |   -- Initial Release --           |
//  |-------------------|---------------|-----------------------------------|
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Reflection.Metadata;

// Initial description:
Console.ForegroundColor = ConsoleColor.White;
Console.BackgroundColor = ConsoleColor.Blue;
Console.WriteLine("*** File Converter ***");
Console.ResetColor();
Console.WriteLine("Application to convert files to a different file format.");
Console.WriteLine();

// Selected directory:
string? currDirectory = string.Empty;

Console.WriteLine("Enter a directory to convert files:");
currDirectory = Console.ReadLine();

while (!Directory.Exists(currDirectory))
{
    Console.ForegroundColor= ConsoleColor.Red;
    Console.Write("ERROR: ");
    Console.ResetColor();
    Console.WriteLine("Directory invalid. Please try again:");
    currDirectory = Console.ReadLine();
    Console.WriteLine();
}
Console.WriteLine();

// The file format to convert:
Console.WriteLine("Enter the format of the files to change (Example input: png): ");
Console.ForegroundColor= ConsoleColor.Yellow;
Console.WriteLine("[WARNING: Please ensure this is correct as there is no validation.]");
Console.ResetColor();
string? fileFormatToConvert = string.Empty;
fileFormatToConvert = Console.ReadLine();
Console.WriteLine();

// The new file format to convert to:
Console.WriteLine("Enter the format of the files to change to (Example input: png)");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("WARNING: Please ensure this is correct as there is no validation.");
Console.ResetColor();
string? newFileFormat = string.Empty;
newFileFormat = Console.ReadLine();
Console.WriteLine();

// Input to see if files should be copied or replaced:
string? copyToNewDirectory = string.Empty;

while (!(copyToNewDirectory == "y" || copyToNewDirectory == "Y" || copyToNewDirectory == "yes" ||
         copyToNewDirectory == "n" || copyToNewDirectory == "N" || copyToNewDirectory == "no")
      )
{
    Console.WriteLine("Would you like to write the converted files to a different directory");
    Console.WriteLine("instead of overwriting them? (Example input: y OR Y OR yes OR n OR N OR no"); 
    copyToNewDirectory = Console.ReadLine();
    Console.WriteLine();
}

string? newDirectory = string.Empty;
bool newDirectoryUsed = false;

if((copyToNewDirectory == "y" || copyToNewDirectory == "Y" || copyToNewDirectory == "yes"))
{
    Console.WriteLine("Enter a directory to store the converted files: ");
    newDirectory = Console.ReadLine();
    
    while (!Directory.Exists(newDirectory))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ERROR: ");
        Console.ResetColor();
        Console.WriteLine("Directory invalid. Please try again:");
        newDirectory = Console.ReadLine();
    }
    newDirectoryUsed = true;
}
else 
{
    newDirectory = currDirectory;
}

string[] files = Directory.GetFiles(currDirectory);
int fileCount = 0;
int fileErrorCount = 0;

foreach(string file in files)
{
    if(file.Contains(fileFormatToConvert))
    {
        fileCount++;
        string newFileName = Path.ChangeExtension(file, "." + newFileFormat);
        if (newDirectoryUsed)
        {
            Console.WriteLine($"Copying and renaming file {Path.GetFileName(file)} to the new directory.");

            try
            {
                File.Copy(file, newDirectory + "\\" + Path.GetFileName(newFileName), true);
            }
            catch (Exception e)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine($"EXCEPTION: An exception occured, " +
                    $"file {Path.GetFileName(file)} was not copied or created.");
                Console.ResetColor();
                Console.WriteLine(e);
                fileErrorCount++;
            }
        }
        else
        {
            FileInfo newFile = new FileInfo(file);
            if(newFile.Exists)
            {
                try
                {
                    Console.WriteLine($"Modifying file {Path.GetFileName(file)} to type {newFileFormat}.");
                    newFile.MoveTo(newFileName);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"EXCEPTION: An exception occured, " +
                        $"file {Path.GetFileName(file)} caused an issue, and was not modified.");
                    Console.ResetColor();
                    Console.WriteLine(e);
                    fileErrorCount++;
                }
            }
        }
    }
}
Console.WriteLine();
Console.Write("COMPLETED: ");
string completeCondition = string.Empty;
if (fileErrorCount > 0)
{
    Console.ForegroundColor = ConsoleColor.Red;
    completeCondition = "FAILED";
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    completeCondition = "PASSED";
}
Console.Write(completeCondition);
Console.ResetColor();
Console.WriteLine();
Console.WriteLine($"Number of files detected: {fileCount}, number of file errors: {fileErrorCount}");
Console.WriteLine();