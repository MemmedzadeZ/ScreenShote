using System;
using System.IO;
using System.Diagnostics;

class Program
{
    static string desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Images");

    static void Main()
    {
        // Her Enter tuşuna basıldığında ekran görüntüsü al
        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                TakeScreenshotAndSave();
            }
        }
    }

    static void TakeScreenshotAndSave()
    {
        // Images klasörü yoksa oluştur
        if (!Directory.Exists(desktopPath))
        {
            Directory.CreateDirectory(desktopPath);
        }

        // Ekran görüntüsünü al
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string screenshotPath = Path.Combine(desktopPath, $"Screenshot_{timestamp}.png");

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "SnippingTool.exe";
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        Process.Start(startInfo).WaitForExit();

        // Dosyayı kaydet
        File.Copy(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Snip", "Screenshot.png"), screenshotPath, true);

        // Eski dosyaları sil
        DeleteOldFiles();

        // Dosya adlarını yazdır
        ListFileNames();

        // Dosyayı varsayılan programla aç
        OpenWithDefaultProgram(screenshotPath);
    }

    static void DeleteOldFiles()
    {
        // 7 gün öncesinden eski dosyaları sil
        DirectoryInfo directoryInfo = new DirectoryInfo(desktopPath);
        foreach (var file in directoryInfo.GetFiles())
        {
            if (DateTime.Now - file.CreationTime > TimeSpan.FromDays(7))
            {
                file.Delete();
            }
        }
    }

    static void ListFileNames()
    {
        // Klasör içindeki dosya adlarını yazdır
        Console.WriteLine("File Names:");
        foreach (var file in Directory.GetFiles(desktopPath))
        {
            Console.WriteLine(Path.GetFileName(file));
        }
    }

    static void OpenWithDefaultProgram(string filePath)
    {
        // Dosyayı varsayılan programla aç
        Process.Start(filePath);
    }
}
