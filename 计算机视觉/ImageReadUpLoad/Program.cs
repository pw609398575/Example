using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ImageReadUpLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = "C:\\Users\\60939\\Desktop\\hundred_pictures";
            try
            {
                ListFiles(new DirectoryInfo(dir));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ListFiles(FileSystemInfo info)
        {
            if (!info.Exists) return;

            DirectoryInfo dir = info as DirectoryInfo;
            //不是目录
            if (dir == null) return;

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                if (file != null)
                {
                    string extension = Path.GetExtension(file.Name);
                    if (extension.ToUpper() == ".JPG"|| extension.ToUpper() == ".PNG")
                    {
                        try
                        {
                            var bytes = File.ReadAllBytes(file.FullName);
                            var base64 = Convert.ToBase64String(bytes);

                            List<string> base64Strs = new List<string>();
                            base64Strs.Add(base64);
                            var keyValues = new Dictionary<string, List<string>>
               {
                   { "images", base64Strs }
               };
                            var re = JsonConvert.SerializeObject(keyValues);
                            var url = "https://localhost:44372//OCRRun//ImageRecognition";
                            using (var client = new HttpClient())
                            {
                                var content = new StringContent(re, Encoding.UTF8, "application/json");
                                var response = client.PostAsync(url, content).GetAwaiter().GetResult();
                                var stringResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }

        }
    }
}
