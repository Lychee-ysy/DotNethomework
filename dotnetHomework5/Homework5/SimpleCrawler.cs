using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace Homework5
{
    class SimpleCrawler
    { 
    }
    public class url_info
    { 
        public bool processing { get; set; }
        public string html { get; set; }
        public string url { get; set; }

    }
    public class Crawler
    {
        public Hashtable urls = new Hashtable();
        public int count = 0;
        private readonly string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
        private static readonly string pattern = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
        public event Action<Crawler, url_info> crawlerstopped;
        public event Action<Crawler, string> pagedownload;
        public ConcurrentBag<url_info> urls1 = new ConcurrentBag<url_info>();
        public static string urlstart = "";
        public static string urlwith = "";
        public delegate void crawlevent(string status);//委托
        public event crawlevent crawer;//生成事件，下载完成后的url返回给窗口
        public void Crawl()
        {
            url_info url_Info = new url_info() { url = urlstart, processing = false, html = "" };
            urls1.Add(url_Info);
            string str = @"(www\.){0,1}.*?\..*?/";
            Regex r = new Regex(str);
            Match m = r.Match(urlstart);
            urlstart = m.Value;

            while (true)
            {
                url_info current = null;
                foreach (var url in urls1)
                {
                    if (url.processing) continue;
                    current = url;
                    if (count > 20)
                        break;
                    if (current == null)
                        continue;
                    current.processing = true;
                    var t = new Thread(() => Download(current));
                    t.Start();
                    count++;
                }
            }
        }
        public void Download(url_info url)
        {
            try
            {

                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url.url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                url.html = html;
                crawlerstopped(this, url);
                Parsse(html, url.url);//解析,并加入新的链接
            }
            catch (Exception)
            {

            }
        }

    
        bool UrlExists(string url)
        {
            foreach (url_info url_ in urls1)
            {
                return true;
            }
            return false;
        }
        public void Parsse(string html,string oldUrl)//解析
        {
            //爬取网页中的超链接并放入hashtable中，再进行下一步爬取
            
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                var url = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\"', '#', '>');
                if (url.Length == 0)
                    continue;
                //仅包含起始网站上的网页
                if (url.Contains(urlwith))
                {
                    if (!UrlExists(url))
                    {
                        urls1.Add(new url_info() { url = url, processing = false, html = "" });
                    }

                }
            }
            matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                var url = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\"', '#', '>');
                if (url.Length == 0) continue;
                Uri baseUri = new Uri(oldUrl);
                Uri absoluteUri = new Uri(baseUri, url);
                //仅包含起始网站上的网页
                if (url.Contains(urlwith))
                {
                    if (!UrlExists(url))
                    {
                        urls1.Add(new url_info() { url = url, processing = false, html = "" });
                    }
                }
            }
        }
        static private string FixUrl(string url, string baseUrl)
        {
            if (url.Contains("://"))
            {
                return url;
            }
            if (url.StartsWith("//"))
            {
                return "http:" + url;
            }
            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(baseUrl, pattern);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }

            if (url.StartsWith("../"))
            {
                url = url.Substring(3);
                int idx = baseUrl.LastIndexOf('/');
                return FixUrl(url, baseUrl.Substring(0, idx));
            }

            if (url.StartsWith("./"))
            {
                return FixUrl(url.Substring(2), baseUrl);
            }

            int end = baseUrl.LastIndexOf("/");
            return baseUrl.Substring(0, end) + "/" + url;
        }
    }

}
