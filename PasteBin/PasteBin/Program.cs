
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
                string IUserKey;

               var IQuery = new System.Collections.Specialized.NameValueCollection();
     
                IQuery.Add("api_dev_key", "955c285ecbc3d1c8b49111a2a9e4aa7d");
                IQuery.Add("api_user_name","nikoi");
                IQuery.Add("api_user_password", "nikoi");

                using (var wc = new System.Net.WebClient())
                {
                    var respBytes = wc.UploadValues("http://pastebin.com/api/api_login.php", IQuery);
                    string resp = System.Text.Encoding.UTF8.GetString(respBytes);

                    if (resp.Contains("Bad API request"))
                    {
                        throw new System.Net.WebException("Bad Request", System.Net.WebExceptionStatus.SendFailure);
                    }

                    IUserKey = resp;
//                    Console.WriteLine(  IUserKey);
                }

                IQuery = new System.Collections.Specialized.NameValueCollection();
                IQuery.Add("api_option", "paste");
                IQuery.Add("api_user_key", IUserKey);
                IQuery.Add("api_dev_key", "955c285ecbc3d1c8b49111a2a9e4aa7d");
                IQuery.Add("api_paste_code", text);
                IQuery.Add("api_paste_expire_date", "10M");
//                IQuery.Add("api_paste_private", "0");
                IQuery.Add("api_paste_format", "text");
                IQuery.Add("api_paste_name", DateTime.Now.ToString());
             

                using (var webc = new System.Net.WebClient())
                {
                    var respByt = webc.UploadValues("http://pastebin.com/api/api_post.php", IQuery);

                    var res = System.Text.Encoding.UTF8.GetString(respByt);

                    if (res.Contains("Bad API request"))
                    {
                        throw new System.Net.WebException("Bad Request", System.Net.WebExceptionStatus.SendFailure);
                    }

                    pasteBinUrl = res;
                }
/*                var wr = System.Net.WebRequest.Create("http://pastebin.com/api/api_post.php");
//                var encoding = new System.Text.ASCIIEncoding();

                    var bData = new System.Text.ASCIIEncoding().GetBytes("api_option=paste&api_user_key=" +
                                                                         System.Net.WebUtility.UrlEncode(IUserKey) +
                                                                         "&api_dev_key=955c285ecbc3d1c8b49111a2a9e4aa7d&api_paste_code=" +
                                                                         System.Net.WebUtility.UrlEncode(text) +
                                                                         "&api_paste_expire_date=1D&api_paste_name=" +
                                                                         DateTime.Now.ToString());

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
                }*/
                return pasteBinUrl;
            }
        }




    }
