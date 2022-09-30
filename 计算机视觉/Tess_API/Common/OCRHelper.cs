using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Tess_API.Models;
using TesseractOCR.Enums;
using TesseractOCR.Renderers;
using TesseractOCR;

namespace Tess_API.Common
{
    public class OCRHelper
    {

        public static string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void Base64StringToImage(string strbase64, string save_path)
        {
            try
            {
                CreateDirectory(@"./IdentifyingPicture");
                CreateDirectory(@"./tessdata");
                byte[] arr = Convert.FromBase64String(strbase64);
                MemoryStream ms = new MemoryStream(arr);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);                
                img.Save(save_path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void ClearDirectory(List<string> Filepaths)
        {
            foreach (var path in Filepaths)
            {
                foreach (var item in Directory.GetFileSystemEntries(path))
                {
                    if (System.IO.File.Exists(item))
                    {
                        FileInfo fi = new FileInfo(item);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                        var ft = File.GetCreationTime(item);
                        var elapsedTicks = System.DateTime.Now.Ticks - ft.Ticks;
                        var elaspsedSpan = new TimeSpan(elapsedTicks);
                        if (elaspsedSpan.TotalSeconds > 10)
                        {
                            System.IO.File.Delete(item);
                        }
                    }
                }
            }
        }
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void CreateDirectory(List<string> Filepaths)
        {
            foreach (var path in Filepaths)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
        }

        /// <summary>
        /// 输出指定信息到文本文件(追加的方式)
        /// </summary>
        /// <param name="path">文本文件路径</param>
        /// <param name="msg">输出信息</param>
        public static void WriteMessage(string path, string msg)
        {
            CreateDirectory(path);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("{0}\n", msg, DateTime.Now);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
        }
        /// <summary>
        /// 输出指定信息到文本文件
        /// </summary>
        /// <param name="msg">输出信息</param>
        public static void WriteMessage(string msg)
        {
            //string path = ".\\ResultBackUp\\";
            //CreateDirectory(path);
            //using (FileStream fs = new FileStream(Path.Combine(path , "results.txt"), FileMode.Open, FileAccess.Read,FileShare.ReadWrite))
            //{
            //    using (StreamWriter sw = new StreamWriter(fs))
            //    {
            //        sw.BaseStream.Seek(0, SeekOrigin.End);
            //        sw.WriteLine("{0}\n", msg, DateTime.Now);
            //        sw.Flush();
            //        sw.Close();
            //        fs.Close();
            //    }
            //}
            //第一种方法，太麻烦了
            StreamWriter sw = null;
            if (!File.Exists("log.txt"))
            {
                FileStream fs = new FileStream("log.txt", FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine(msg + DateTime.Now);
                //记得要关闭！不然里面没有字！
                sw.Close();
                fs.Close();
            }
            else
            {
                sw = File.AppendText("log.txt");
                sw.WriteLine(msg + DateTime.Now);
                sw.Close();
                //MessageBox.Show("已经有log文件了!");
            }

            //第二种方法，比较简单
            //\r\n要加在前面才会换行！
            //File.AppendAllText(".\\ResultBackUp\\log.txt", "\r\n" + msg + DateTime.Now);
        }

        public static void RunTess(ImageUploadBody image_body, out string result)
        {
            var stopWatch = new Stopwatch();
            ResultState root = new ResultState();
            root.results = new List<List<OCRResult>>();
            List<OCRResult> items = new List<OCRResult>();
            OCRResult item;
            string file_name = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            OCRHelper.Base64StringToImage(image_body.images.FirstOrDefault().ToString(), System.IO.Path.Combine(".\\IdentifyingPicture\\", file_name));

            try
            {
                stopWatch.Start();
                using (var engine = new Engine(".\\tessdata\\", "eng", EngineMode.Default))
{
                    using (var img = TesseractOCR.Pix.Image.LoadFromFile(".\\IdentifyingPicture\\" + file_name))
                    {
                        stopWatch.Stop();
                        using (var page = engine.Process(img))
                        {
                            foreach (var block in page.Layout)
                            {
                                foreach (var paragraph in block.Paragraphs)
                                {
                                    foreach (var textLine in paragraph.TextLines)
                                    {
                                        item = new OCRResult();
                                        var content = textLine.Text.Trim();
                                        if (!string.IsNullOrEmpty(content) && content != "" && content != " ")
                                        {
                                            var boundingBox = textLine.BoundingBox.Value;
                                            int[][] region = new int[][]
                                            {
                                                new int[]{boundingBox.X1, boundingBox.Y1 },
                                                new int[]{boundingBox.X2, boundingBox.Y1 },
                                                new int[]{boundingBox.X1, boundingBox.Y2 },
                                                new int[]{ boundingBox.X2, boundingBox.Y2 }
                                            };

                                            item.confidence = textLine.Confidence / 100;
                                            item.text = textLine.Text;
                                            item.text_region = region;
                                            items.Add(item);
}
}
}
                            }
                            root.msg = stopWatch.ElapsedMilliseconds.ToString() + "ms";
                            root.results.Add(items);
                            root.status = "000";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            result = JsonConvert.SerializeObject(root);
        }

    }
}