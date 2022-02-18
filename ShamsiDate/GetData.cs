
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ShamsiDate
{
    public class GetData
    {
        public List<List<Dictionary<string,string>>> Data; //Doc<Row<Cl<>>>
        public GetData()
        {
            Data = new List<List<Dictionary<string, string>>>();
        }
        public List<Dictionary<string, string>> Events(int day,int month,int year)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = @"https://www.time.ir/fa/event/list/0/" + year.ToString() + @"/" + month.ToString() + @"/" + day.ToString();
            WebClient web = new WebClient();
            web.Encoding = Encoding.UTF8;
            string input = web.DownloadString(url);
            MatchCollection MDayEvents = Regex.Matches(input, @"li class.*?te"">.*?</span>.*?<", RegexOptions.Singleline);
            List<Dictionary<string, string>> DaysEvent = new List<Dictionary<string, string>>();
            foreach(Match m in MDayEvents)
            {
                string [] SpM =m.Value.Split(new char[] { '>', '<' });
                Dictionary<string, string> Row = new Dictionary<string,string>();
                string[] temp = SpM[3].Trim().Split(' ')[0].Replace("&#", "").Split(';');
                SpM[3] = Convert.ToString(Convert.ToInt32(temp[0]) - 1776) + ((temp[1] != "") ? (Convert.ToString(Convert.ToInt32(temp[1]) - 1776)) : "");
                Row.Add("Day", SpM[3]);
                Row.Add("Event", SpM[5].Trim());
                Row.Add("Holiday", Convert.ToString(SpM[0].Contains("eventHoliday")));
                DaysEvent.Add(Row);
            }

            Data.Add(DaysEvent);
            return DaysEvent;
        }
        public List<Dictionary<string, string>> Events(int month, int year)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = @"https://www.time.ir/fa/event/list/0/" + year.ToString() + @"/" + month.ToString();
            WebClient web = new WebClient();
            web.Encoding = Encoding.UTF8;
            string input = web.DownloadString(url);
            MatchCollection MDayEvents = Regex.Matches(input, @"li class.*?te"">.*?</span>.*?<", RegexOptions.Singleline);
            List<Dictionary<string, string>> DaysEvent = new List<Dictionary<string, string>>();
            foreach (Match m in MDayEvents)
            {
                string[] SpM = m.Value.Split(new char[] { '>', '<' });
                Dictionary<string, string> Row = new Dictionary<string, string>();
                string[] temp = SpM[3].Trim().Split(' ')[0].Replace("&#", "").Split(';');
                SpM[3] = Convert.ToString(Convert.ToInt32(temp[0]) - 1776) + ((temp[1]!="")? (Convert.ToString(Convert.ToInt32(temp[1]) - 1776)) :"") ;
                Row.Add("Day", SpM[3]);
                Row.Add("Event", SpM[5].Trim());
                Row.Add("Holiday", Convert.ToString(SpM[0].Contains("eventHoliday")));
                DaysEvent.Add(Row);
            }

            Data.Add(DaysEvent);
            return DaysEvent;
        }
        public List<Dictionary<string, string>> Events()
        {
            string url = @"https://www.time.ir/fa/eventyear-%d8%aa%d9%82%d9%88%db%8c%d9%85-%d8%b3%d8%a7%d9%84%db%8c%d8%a7%d9%86%d9%87";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient web = new WebClient();
            web.Encoding = Encoding.UTF8;
            string input = web.DownloadString(url);
            MatchCollection MDayEvents = Regex.Matches(input, @"li class.*?te"">.*?</span>.*?<", RegexOptions.Singleline);
            List<Dictionary<string, string>> DaysEvent = new List<Dictionary<string, string>>();
            int tempMc = 0;
            int Mnum = 1;
            foreach (Match m in MDayEvents)
            {
                string[] SpM = m.Value.Split(new char[] { '>', '<' });
                Dictionary<string, string> Row = new Dictionary<string, string>();
                string[] temp = SpM[3].Trim().Split(' ')[0].Replace("&#", "").Split(';');
                SpM[3] = Convert.ToString(Convert.ToInt32(temp[0]) - 1776) + ((temp[1] != "") ? (Convert.ToString(Convert.ToInt32(temp[1]) - 1776)) : "");
                if (Convert.ToInt32(SpM[3]) >= tempMc)
                    tempMc = Convert.ToInt32(SpM[3]);
                else
                {
                    tempMc = Convert.ToInt32(SpM[3]);
                    Mnum++;
                }
                Row.Add("Day",   Convert.ToString(Mnum)+ @" / " +SpM[3]);
                Row.Add("Event", SpM[5].Trim());
                Row.Add("Holiday", Convert.ToString(SpM[0].Contains("eventHoliday")));
                DaysEvent.Add(Row);
            }

            Data.Add(DaysEvent);
            return DaysEvent;
        }
    }
}
