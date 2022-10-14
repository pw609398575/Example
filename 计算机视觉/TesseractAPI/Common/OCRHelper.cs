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
using System.Collections;
using System.Threading;
using System.Configuration;

namespace Tess_API.Common
{
    /// <summary>
    /// OCR使用帮助类
    /// </summary>
    public class OCRHelper
    {
        private static string _webapi = ConfigurationManager.AppSettings["API"];
        private static string _logpath = ConfigurationManager.AppSettings["LogPath"];
        private static string _tessdatapath = ConfigurationManager.AppSettings["TessdataPath"];
        private static string _language= ConfigurationManager.AppSettings["Language"];
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
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
            try
            {
                StreamWriter sw = null;
                CreateDirectory(_logpath);
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
        /// <summary>
        /// 运行TesseractOCR
        /// </summary>
        /// <param name="image_body"></param>
        /// <returns></returns>
        public static string RunTess(ImageUploadBody image_body)
        {
            var stopWatch = new Stopwatch();
            ResultState root = new ResultState();
            root.results = new List<List<OCRResult>>();
            List<OCRResult> items = new List<OCRResult>();
            OCRResult item;
            try
            {
                stopWatch.Start();
                byte[] arr = Convert.FromBase64String(image_body.Images.FirstOrDefault().ToString());
                using (var engine = new Engine(_tessdatapath, _language, EngineMode.Default))
                {
                    using (var img = TesseractOCR.Pix.Image.LoadFromMemory(arr))
                    {                       
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
                            stopWatch.Stop();
                            root.msg = stopWatch.ElapsedMilliseconds.ToString() + "ms";
                            root.results.Add(items);
                            root.status = "000";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                root.msg = ex.ToString();
                root.results.Add(items);
                root.status = "-1";
            }
            return JsonConvert.SerializeObject(root);
        }
    }
}