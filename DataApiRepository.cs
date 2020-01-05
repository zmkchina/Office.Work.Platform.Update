using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office.Work.Platform.Update
{
    public static class DataApiRepository
    {
        private static HttpClient CreateHttpClient(ProgressMessageHandler processMessageHander = null)
        {
            HttpClient _Client;
            if (processMessageHander != null)
            {
                _Client = HttpClientFactory.Create(processMessageHander);
            }
            else
            {
                _Client = HttpClientFactory.Create();
            }
            _Client.Timeout = TimeSpan.FromSeconds(60);
            return _Client;
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
            Object TResult = null;
            try
            {
                if (typeof(T) == typeof(HttpResponseMessage))
                {
                    TResult = await _Client.GetAsync(ApiUri);
                }
                else if (typeof(T) == typeof(Stream))
                {
                    TResult = await _Client.GetStreamAsync(ApiUri);
                }
                else if (typeof(T) == typeof(byte[]))
                {
                    TResult = await _Client.GetByteArrayAsync(ApiUri);
                }
                else
                {
                    string ResponseString = await _Client.GetStringAsync(ApiUri);
                    TResult = JsonConvert.DeserializeObject<T>(ResponseString);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _Client?.Dispose();
            }
            return (T)TResult;
        }
    }
}
