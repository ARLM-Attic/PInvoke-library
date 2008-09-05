using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;

using System.Data;
using System.Text;
using Discuz.Config;
//using DACBusiness;
namespace Discuz.Web.Services
{
    [WebService(Namespace = "Discuz.Web.Services")]
#if NET1
#else
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
#endif
    public class MixObjects : System.Web.Services.WebService
    {
        public MixObjects()
        {

        }
        [WebMethod]
        public string ADMedia(string pagename, int forumid)
        {
            string[] parameters = Forum.Advertisements.GetMediaAdParams(pagename, forumid);
            return @"<?xml version='1.0'?>" +
                  " <AD>" +
                  "  <play>" +
                  "     <playmodel>3</playmodel>" +
                  "     <backgrounduri>" + parameters[2] + "</backgrounduri>" +
                  "     <mediauri>" + parameters[1] + "</mediauri>" +
                  "  </play>" +
                  "  <speed>" +
                  "     <group>" +
                  "        <speedbackground>" + parameters[4] + "</speedbackground>" +
                  "        <speedphoto>assets/images/heros03.png</speedphoto>" +
                  "        <speedtext>assets/images/heros02.png</speedtext>" +
                  "     </group>" +
                  "  </speed>" +
                  "  <screen>" +
                  "     <group>" +
                  "        <text>" + parameters[5] + "</text>" +
                  "        <textwidth>320</textwidth>" +
                  "        <textheight>18</textheight>" +
                  "        <image>assets/images/smallmsnbc.png</image>" +
                  "        <imagewidth>15</imagewidth>" +
                  "        <imageheight>9</imageheight>" +
                  "        <text>" + parameters[6] + "</text>" +
                  "        <textwidth>320</textwidth>" +
                  "        <textheight>18</textheight>" +
                  "        <image>assets/images/smallmsnbc.png</image>" +
                  "        <imagewidth>15</imagewidth>" +
                  "        <imageheight>9</imageheight>" +
                  "        <text>" + parameters[7] + "</text>" +
                  "        <textwidth>320</textwidth>" +
                  "        <textheight>18</textheight>" +
                  "        <image>assets/images/smallmsnbc.png</image>" +
                  "        <imagewidth>15</imagewidth>" +
                  "        <imageheight>9</imageheight>" +
                /*
                "        <text>80 DAYS OR BUST: A HANKERING FOR HOTCAKES IN BEIJING</text>" +
                "        <textwidth>364</textwidth>" +
                "        <textheight>18</textheight>" +
                "        <image>assets/images/smallmsnbc.png</image>" +
                "        <imagewidth>15</imagewidth>" +
                "        <imageheight>9</imageheight>" +
                "        <text>FIND OUT WHY RAIDING THE FRIDGE AT NIGHT IS A BAD IDEA</text>" +
                "        <textwidth>364</textwidth>" +
                "        <textheight>18</textheight>" +
                "        <image>assets/images/smallmsnbc.png</image>" +
                "        <imagewidth>15</imagewidth>" +
                "        <imageheight>9</imageheight>" +
                */
                  "    </group>" +
                  "  </screen>" +
                  " </AD>";
        }
        [WebMethod]
        public string ChartData(int topicid)
        {
            DataTable polllist = Discuz.Forum.Polls.GetPollOptionList(topicid);
            StringBuilder builder = new StringBuilder("{CreateDate:'");
            builder.Append(DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
            builder.Append("',Sectors:[");
            int i = 0;
            foreach (DataRow dr in polllist.Rows)
            {
                builder.Append("{");
                builder.AppendFormat("ID:{0},", i);
                builder.AppendFormat("Value:{0},", (Convert.ToDouble(dr["percent"].ToString().Replace("%", string.Empty))/100.00).ToString());
                builder.AppendFormat("Title:'{0}',", dr["name"]);
                builder.AppendFormat("Comment:'{0}'", dr["value"]);
                builder.Append("},");
                i++;
            }
            if (polllist.Rows.Count > 0)
                builder.Remove(builder.Length - 1, 1);
            builder.Append("]}");
            return builder.ToString();


            //return @"<?xml version='1.0'?>" +
            //      " <chart>" +
            //      "   <double>120</double>" +
            //      "   <double>100</double>" +
            //      " </chart>";
        }
        
        [WebMethod]
        public string AlbumData(int albumid)
        {
            string jsonpath = BaseConfigs.GetForumPath + "cache/album/" + (albumid/1000+1).ToString() + "/" + albumid + "_json.txt";
            if (File.Exists(jsonpath))
            {
                using (StreamReader sr = new StreamReader(jsonpath, Encoding.UTF8))
                {
                    string Content = sr.ReadToEnd();
                    sr.Close();
                    if (Content.Trim() != string.Empty)
                        return Content.Trim();
                }
            }
            try
            {
                return Plugin.Album.AlbumPluginProvider.GetInstance().GetAlbumJsonData(albumid);
            }
            catch
            {
                return "";
            }

        }
        
        #region useless codes
        /*
        //[WebMethod]
        //public string PhotoesData(int albumid)
        //{
        //    DataTable dt = DatabaseProvider.GetInstance().GetPhotosByAlbumid(albumid);
        //    StringBuilder builder = new StringBuilder(@"<?xml version='1.0'?><photoes>");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        string url = dr["filename"].ToString().Trim().ToLower();
        //        if (!url.EndsWith(".jpg") && !url.EndsWith(".png"))
        //        {
        //            url = "tools/imageconverter.aspx?u=" + BaseConfigs.GetForumPath + url;
        //        }
        //        builder.AppendFormat("<photo>{0}</photo><caption>{1}</caption>", BaseConfigs.GetForumPath + url, dr["title"].ToString().Trim());
        //    }
        //    builder.Append("</photoes>");
        //    return builder.ToString();
        //    //return @"<?xml version='1.0'?>" +
        //    //      " <photoes>" +
        //    //      "   <photo>Assets/Images/photo/1.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/2.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/3.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/4.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/5.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/6.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/7.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/8.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/9.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/10.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/11.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/12.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/13.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      "   <photo>Assets/Images/photo/14.jpg</photo>" +
        //    //      "   <caption>my photo</caption>" +
        //    //      " </photoes>";
        //}
        [WebMethod]
        public string EchoInput(String input)
        {
            string inputString = Server.HtmlEncode(input);
            if (!String.IsNullOrEmpty(inputString))
            {
                return String.Format("You entered {0}. The "
                  + "current time is {1}.", inputString, DateTime.Now);
            }
            else
            {
                return "The input string was null or empty.";
            }
        }
        [WebMethod]
        public StockQuote[] GetQuotes(string symbol)
        {

            Thread.Sleep(5000);

            string QuotesFile = Server.MapPath(symbol + ".xml");

            if (!File.Exists(QuotesFile))
            {
                return null;
            }


            System.IO.StreamReader QuoteData = new System.IO.StreamReader(QuotesFile);

            XmlReader reader = XmlReader.Create(QuoteData);

            List<StockQuote> quotes = new List<StockQuote>();

            while (reader.Read())
            {

                if (reader.Name == "Stock")
                {

                    StockQuote quote = new StockQuote();
                    quote.Symbol = reader.GetAttribute(0);
                    quote.Date = reader.GetAttribute(1);
                    quote.Open = reader.GetAttribute(2);
                    quote.High = reader.GetAttribute(3);
                    quote.Low = reader.GetAttribute(4);
                    quote.Close = reader.GetAttribute(5);
                    quote.Volume = reader.GetAttribute(6);

                    quotes.Add(quote);

                }

            }



            StockQuote[] quotesArray = new StockQuote[quotes.Count];
            quotes.CopyTo(quotesArray);

            return quotesArray;
        }
         */
         #endregion
    }
    /*
    public class StockQuote
    {
        public string Symbol;
        public string Date;
        public string Open;
        public string High;
        public string Low;
        public string Close;
        public string Volume;
    }
    */
}
