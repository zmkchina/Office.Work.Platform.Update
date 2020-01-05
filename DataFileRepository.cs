using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Windows.Forms;

namespace Office.Work.Platform.Update
{
    public static class DataFileRepository
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="downFileName">将下载的文件名</param>
        /// <param name="showDownProgress">显示下载进度的委托方法,可为空</param>
        /// <returns>返回下载成功的文件目录（包括路径）</returns>
        public static async void DownloadFile(string downFileName, ProgressMessageHandler showDownProgress = null)
        {
            HttpResponseMessage httpResponseMessage = await DataApiRepository.GetApiUri<HttpResponseMessage>("http://Localhost/Api/FileDown/Update/" + downFileName, showDownProgress);
            Stream responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            //以上两句代码不能用下一句代替，否则进度报告出现卡顿。
            if (httpResponseMessage.Content.Headers.ContentLength > 0 && responseStream != null)
            {
                string needUpDateFile = System.IO.Path.Combine(Application.StartupPath, downFileName);

                //创建一个文件流
                FileStream fileStream = new FileStream(needUpDateFile, FileMode.Create);
                //await responseStream.CopyToAsync(fileStream);
                byte[] buffer = new byte[2048];
                int readLength;
                while ((readLength = await responseStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    // 写入到文件
                    fileStream.Write(buffer, 0, readLength);
                }
                responseStream.Close();
                fileStream.Close();
            }
            else
            {
                MessageBox.Show("下载失败，可能该文件已被从服务器上删除", "不存在", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
