using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Threading.Tasks;
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
            HttpResponseMessage httpResponseMessage = await DataApiRepository.GetApiUri<HttpResponseMessage>("http://172.16.0.9/Api/FileDown/Update/" + downFileName, showDownProgress);
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
        /// <summary>
        /// 使用GET方法，获取服务器资源。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ApiUri">资源Uri</param>
        /// <param name="processMessageHander">进度报告委托</param>
        /// <returns></returns>
        public static async Task<T> GetApiUri<T>(string ApiUri, ProgressMessageHandler processMessageHander = null)
        {
            HttpClient _Client = CreateHttpClient(processMessageHander);
            try
            {
                Object TResult = null;
                HttpResponseMessage ResultResponse = await _Client.GetAsync(ApiUri).ConfigureAwait(false);
                if (typeof(T) == typeof(HttpResponseMessage))
                {
                    TResult = ResultResponse;
                }
                else if (typeof(T) == typeof(Stream))
                {
                    TResult = await ResultResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
                }
                else if (typeof(T) == typeof(byte[]))
                {
                    TResult = await ResultResponse.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
                else if (typeof(T) == typeof(string))
                {
                    TResult = await ResultResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else
                {
                    string ResponseString = await ResultResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TResult = JsonConvert.DeserializeObject<T>(ResponseString);
                }
                return (T)TResult;
            }
            catch (Exception)
            {
            }
            finally
            {
                _Client.Dispose();
            }
            return default(T);
        }
    }
}
