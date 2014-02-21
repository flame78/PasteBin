
    using System;

    internal class Test
    {
        private static void Main(string[] args)
        {
            string url = PasteBin.Send("Blaaaaa Hello World, from PasteBinSample!");

            Console.WriteLine(url);
            Console.ReadKey();
        }

        public static class PasteBin
        {
        
            public static string Send(string text)
            {
                string pasteBinUrl = "";

                var wr = System.Net.WebRequest.Create("http://pastebin.com/api/api_post.php");
                var encoding = new System.Text.ASCIIEncoding();
                
                byte[] bData = encoding.GetBytes("api_option=paste&api_dev_key=657832d209658e4cabf0a997789711b7&api_paste_code=" + System.Net.WebUtility.UrlEncode(text) + "&api_paste_private=0&api_paste_expire_date=1D&api_paste_name=" + DateTime.Now.ToString());

                wr.Method = "POST";
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.ContentLength = bData.Length;

                using (var sMyStream = wr.GetRequestStream())
                {
                    sMyStream.Write(bData, 0, bData.Length);
                }

                using (var response = wr.GetResponse())
                {
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        pasteBinUrl = reader.ReadToEnd();
                    }
                }

                return pasteBinUrl;
            }
        }




    }
