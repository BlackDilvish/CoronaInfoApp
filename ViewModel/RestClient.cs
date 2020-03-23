using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;

namespace CoronaInfoAppCore.ViewModel
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    static class RestClient
    {
        public static string EndPoint { get; set; }
        public static httpVerb HttpMethod { get; set; } = httpVerb.GET;

        public static List<string> MakeRequest(string url)
        {
            EndPoint = url;
            List<string> responseValue = new List<string>();

            HttpWebRequest request = WebRequest.Create(EndPoint) as HttpWebRequest;
            request.Method = HttpMethod.ToString();

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new ApplicationException("Error: " + response.StatusDescription);

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            while (!reader.EndOfStream)
                                responseValue.Add(reader.ReadLine());
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return responseValue;
        }

    }
    
}
