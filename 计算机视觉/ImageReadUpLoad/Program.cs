using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageReadUpLoad
{
    class Program
    {
        private static string _oCRurl = ConfigurationManager.AppSettings["XNEBULAOCR_API"];
        private static string _logpath = ConfigurationManager.AppSettings["LogPath"];

        static void Main(string[] args)
        {
            string dir = "C:\\Users\\60939\\Desktop\\hundred_pictures";
            try
            {
                //ListFiles(new DirectoryInfo(dir));
                ListPictures(new DirectoryInfo(dir));
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
                Thread.Sleep(5000);
                FileInfo file = files[i] as FileInfo;
                if (file != null)
                {
                    string extension = Path.GetExtension(file.Name);
                    if (extension.ToUpper() == ".JPG"|| extension.ToUpper() == ".PNG"|| extension.ToUpper() == ".BMP")
                    {
                        try
                        {
                            var bytes = File.ReadAllBytes(file.FullName);
                            var base64 = Convert.ToBase64String(bytes);

                            List<string> base64Strs = new List<string>();
                            base64Strs.Add(base64);
                            var keyValues = new Dictionary<string, List<string>>
                            { {"images",base64Strs}
                            };
                            var re = JsonConvert.SerializeObject(keyValues);
                            using (var client = new HttpClient())
                            {
                                var content = new StringContent(re, Encoding.UTF8, "application/json");
                                var response = client.PostAsync(_oCRurl, content).GetAwaiter().GetResult();
                                var stringResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                Task task =  Task.Run(() =>
                                {
                                    WriteMessage(stringResponse);
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }              
            }

        }

        public static void ListPictures(FileSystemInfo info)
        {
            if (!info.Exists) return;

            DirectoryInfo dir = info as DirectoryInfo;
            //不是目录
            if (dir == null) return;

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                Thread.Sleep(5000);
                FileInfo file = files[i] as FileInfo;
                if (file != null)
                {
                    string extension = Path.GetExtension(file.Name);
                    if (extension.ToUpper() == ".JPG" || extension.ToUpper() == ".PNG" || extension.ToUpper() == ".BMP")
                    {
                        try
                        {
                            Image image = Image.FromFile(file.FullName);
                            var imgguid = image.RawFormat.Guid;
                            var mimeType = "";
                            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
                            {
                                if (codec.FormatID == imgguid)
                                    mimeType = codec.MimeType;
                            }

                            var bytes = File.ReadAllBytes(file.FullName);
                            var base64 = Convert.ToBase64String(bytes);

                            string json = String.Format("{{\"file\":\"data:{0};base64,{1}\"}}", mimeType, base64);

                            using (var client = new HttpClient())
                            {
                                var content = new StringContent(json, Encoding.UTF8, "application/json");
                                var response = client.PostAsync(_oCRurl, content).GetAwaiter().GetResult();
                                var stringResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                Task task = Task.Run(() =>
                                {
                                    WriteMessage(stringResponse);
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

        }

        public static void WriteMessage(string msg)
        {
            try
            {
                StreamWriter sw = null;
                if (!Directory.Exists(_logpath))
                {
                    Directory.CreateDirectory(_logpath);
                }
                string path = _logpath + DateTime.Now.ToString("yyyyMMdd") + ".txt";

                if (!File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.WriteLine(msg + DateTime.Now);
                    //记得要关闭！不然里面没有字！
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    sw = File.AppendText(path);
                    sw.WriteLine(msg + DateTime.Now);
                    sw.Close();
                    //MessageBox.Show("已经有log文件了!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
