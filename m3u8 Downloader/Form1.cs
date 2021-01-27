using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace m3u8_Downloader
{
    public partial class main : Form
    {
        //Constants :
        const string CO_VIDEO_NAME_TS = "video.ts";
        const string CO_VIDEO_NAME_MP4 = "video.mp4";
        const string CO_WORKING_FOLDER_NAME = "job_";
        const string CO_M3U8_STARTS_WITH = "#EXTM3U";
        const string CO_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";


        //Status texts :
        private static string STATUS_DECODING_M3U8 = "Decoding m3u8 file...";
        private static string STATUS_DOWNLOADING = "Downloading video parts...";
        private static string STATUS_COMBINING_PARTS = "Combining parts into one...";
        private static string STATUS_COMPLETED = "Done";

        //Error texts :
        private static string ERROR_DOWNLOAD_FAILED = "Failed to download file (*)";
        private static string ERROR_INVALID_URL = "Invalid URL";

        public main()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            if (input_link.Text == "") return;

            button_download.Enabled = false;
            status.Text = STATUS_DECODING_M3U8;

            List<string> urls = new List<string>();
            urls = decodeM3U8(input_link.Text);

            Thread download = new Thread(() => downloadFiles(this,urls));
            download.Start();
        }

        private List<string> decodeM3U8(string link)
        {
            List<string> urls = new List<string>();
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            byte[] m3u8_bytes = null;
            using (var client = new WebClient())
            {
                try
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, CO_USER_AGENT);
                    m3u8_bytes = client.DownloadData(link);
                }
                catch (Exception eror)
                {
                    MessageBox.Show(ERROR_DOWNLOAD_FAILED.Replace("*", eror.Message));
                    button_download.Enabled = true;
                    changeStatus(this, "");
                    return null;
                }
            }
            MemoryStream m3u8MemoryStream = new MemoryStream(m3u8_bytes);
            StreamReader m3u8 = new StreamReader(m3u8MemoryStream); //new StreamReader(path + @"\" + tmp_filename);
            if (!m3u8.ReadLine().StartsWith(CO_M3U8_STARTS_WITH))
            {
                MessageBox.Show(ERROR_INVALID_URL);
                m3u8.Close();
                m3u8.Dispose();
                button_download.Enabled = true;
                changeStatus(this, "");
                return null;
            }
            string baseURL = link.Substring(0, link.Length - link.Split('/').GetValue(link.Split('/').Length - 1).ToString().Length);
            string line = "";
            while ((line = m3u8.ReadLine()) != null)
            {
                if (line.StartsWith("#")) continue;
                if (line.ToLower().EndsWith(".m3u8"))
                {
                    List<string> tmpUrls = decodeM3U8(baseURL + line);
                    urls.AddRange(tmpUrls);
                }
                else
                    urls.Add(baseURL + line);
            }
            m3u8.Close();
            m3u8.Dispose();

            return urls;
        }

        private void downloadFiles(main control, List<string> urls)
        {
            changeStatus(control, STATUS_DOWNLOADING);
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string folder = CO_WORKING_FOLDER_NAME + (new Random(Guid.NewGuid().GetHashCode() + 123)).Next(0, 10000000);
            
            if (Directory.Exists(path + @"\" + folder)) Directory.Delete(path + @"\" + folder, true);
            Directory.CreateDirectory(path + @"\" + folder);
            path = path + @"\" + folder + @"\";
            
            int i = 0;
            bool tryagain = true;
            int urlNumber = urls.Count();
            MemoryStream videoTS = new MemoryStream();
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, CO_USER_AGENT);
                foreach (string url in urls)
                {
                    tryagain = true;
                    while (tryagain)
                    {
                        tryagain = false;
                        try
                        {
                            byte[] file = client.DownloadData(url);
                            videoTS.Write(file, 0, file.Length);
                        }
                        catch
                        {
                            if (File.Exists(path + i.ToString() + ".ts")) File.Delete(path + i.ToString() + ".ts");
                            Thread.Sleep(5000);
                            tryagain = true;
                        }
                    }
                    Thread.Sleep(50);
                    control.list.Invoke((MethodInvoker)delegate
                    {
                        list.Items.Add(i.ToString() + ".ts - Downloaded");
                        status.Text = STATUS_DOWNLOADING + " (" + i.ToString() + "/" + urlNumber + ")...";
                        list.SelectedIndex = list.Items.Count - 1;
                    });
                    i++;
                }
            }
            changeStatus(control, STATUS_COMBINING_PARTS);
            Thread.Sleep(100);

            File.WriteAllBytes(path + CO_VIDEO_NAME_TS, videoTS.ToArray());
            videoTS.Close();
            videoTS.Dispose();

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\ffmpeg.exe";
            ffmpeg.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            ffmpeg.StartInfo.Arguments = "-i \"" + path + CO_VIDEO_NAME_TS + "\" -acodec copy -vcodec copy \"" + path + CO_VIDEO_NAME_MP4 + "\"";
            ffmpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ffmpeg.Start();
            ffmpeg.WaitForExit();

            File.Delete(path + CO_VIDEO_NAME_TS);

            Thread.Sleep(100);
            changeStatus(control, STATUS_COMPLETED);
            Process.Start(path);

            control.button_download.Invoke((MethodInvoker)delegate
            {
                button_download.Enabled = true;
                input_link.Text = "";
                list.Items.Clear();
            });
        }

        private void changeStatus(main control ,string text)
        {
            control.status.Invoke((MethodInvoker)delegate
            {
                control.status.Text = text;
            });
        }
    }
}
