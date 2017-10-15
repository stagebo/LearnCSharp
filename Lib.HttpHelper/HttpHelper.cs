using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lib.HttpHelper
{
    public class HttpHelper
    {
        HttpClient _httpClient;

        public HttpHelper()
        {
            this._httpClient = new HttpClient();
            _httpClient.MaxResponseContentBufferSize = 256000;
            _httpClient.DefaultRequestHeaders.Add("user-agent", 
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public string SendGet(string url, Dictionary<string, string> paramList=null)
        {
            if (paramList != null)
            {
                bool isStart = true;
                foreach (var kp in paramList)
                {
                    url += isStart ? "?" : "&";
                    isStart = false;
                    url += kp.Key;
                    url += "=";
                    url += kp.Value;
                }
            }
            HttpResponseMessage response = _httpClient.GetAsync(new Uri(url)).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public string SendPost(string url, Dictionary<string, string> paramList = null)
        {
            HttpResponseMessage response = _httpClient.PostAsync(new Uri(url), new FormUrlEncodedContent(paramList)).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
          
    }
}
