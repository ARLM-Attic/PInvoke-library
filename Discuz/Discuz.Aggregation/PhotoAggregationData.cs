using System;
using System.Text;
using System.Xml;

using Discuz.Common.Xml;
using Discuz.Entity;

namespace Discuz.Aggregation
{
    /// <summary>
    /// 图片聚合数据类
    /// </summary>
    public class PhotoAggregationData : AggregationData
    {
        
        /// <summary>
        /// 图片聚合信息
        /// </summary>
        private static PhotoAggregationInfo __photoAggregationInfo;


        /// <summary>
        /// 清空数据绑定
        /// </summary>
        public override void ClearDataBind()
        {
            __photoAggregationInfo = null;
        }

        /// <summary>
        /// 得到图片聚合信息对象
        /// </summary>
        /// <returns></returns>
        public PhotoAggregationInfo GetPhotoAggregationInfo()
        {
            if (__photoAggregationInfo != null)
            {
                return __photoAggregationInfo;
            }

            return GetPhotoAggregationInfoFromFile();
        }

        /// <summary>
        /// 从文件中获得数据并初始化图片聚合对象
        /// </summary>
        /// <returns></returns>
        public PhotoAggregationInfo GetPhotoAggregationInfoFromFile()
        {

            XmlNode xmlnode = __xmlDoc.DocumentElement.SelectNodes("/Aggregationinfo/Aggregationpage/Albumindex/Albumconfig")[0];

            __photoAggregationInfo = new PhotoAggregationInfo();
            if (xmlnode != null)
            {

                __photoAggregationInfo.Focusphotoshowtype = (__xmlDoc.GetSingleNodeValue(xmlnode, "Focusphotoshowtype") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "Focusphotoshowtype"));
                __photoAggregationInfo.Focusphotodays = (__xmlDoc.GetSingleNodeValue(xmlnode, "Focusphotodays") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "Focusphotodays"));
                __photoAggregationInfo.Focusphotocount = (__xmlDoc.GetSingleNodeValue(xmlnode, "Focusphotocount") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "Focusphotocount"));
                __photoAggregationInfo.Focusalbumshowtype = (__xmlDoc.GetSingleNodeValue(xmlnode, "Focusalbumshowtype") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "Focusalbumshowtype"));
                __photoAggregationInfo.Focusalbumdays = (__xmlDoc.GetSingleNodeValue(xmlnode, "Focusalbumdays") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "Focusalbumdays"));
                __photoAggregationInfo.Focusalbumcount = (__xmlDoc.GetSingleNodeValue(xmlnode, "Focusalbumcount") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "Focusalbumcount"));
                __photoAggregationInfo.Weekhot = (__xmlDoc.GetSingleNodeValue(xmlnode, "Weekhot") == null) ? 0 : Convert.ToInt32(__xmlDoc.GetSingleNodeValue(xmlnode, "Weekhot"));
            }

            return __photoAggregationInfo;
        }

        /// <summary>
        /// 保存图片聚合对象信息到聚合数据文件
        /// </summary>
        /// <param name="__photoaggregationinfo"></param>
        public void SaveAggregationData(PhotoAggregationInfo __photoaggregationinfo)
        {

            XmlNode photoaggregationsetting = __xmlDoc.SelectSingleNode("/Aggregationinfo/Aggregationpage/Albumindex/Albumconfig");
            if (photoaggregationsetting != null)
            {
                photoaggregationsetting.RemoveAll();
            }
            else
            {
                photoaggregationsetting = __xmlDoc.CreateNode("/Aggregationinfo/Aggregationdata/Space");
            }

            __xmlDoc.AppendChildElementByNameValue(ref photoaggregationsetting, "Focusphotoshowtype", __photoaggregationinfo.Focusphotoshowtype);
            __xmlDoc.AppendChildElementByNameValue(ref photoaggregationsetting, "Focusphotodays", __photoaggregationinfo.Focusphotodays);
            __xmlDoc.AppendChildElementByNameValue(ref photoaggregationsetting, "Focusphotocount", __photoaggregationinfo.Focusphotocount);
            __xmlDoc.AppendChildElementByNameValue(ref photoaggregationsetting, "Focusalbumshowtype", __photoaggregationinfo.Focusalbumshowtype);
            __xmlDoc.AppendChildElementByNameValue(ref photoaggregationsetting, "Focusalbumdays", __photoaggregationinfo.Focusalbumdays);
            __xmlDoc.AppendChildElementByNameValue(ref photoaggregationsetting, "Focusalbumcount", __photoaggregationinfo.Focusalbumcount);
            __xmlDoc.AppendChildElementByNameValue(ref photoaggregationsetting, "Weekhot", __photoaggregationinfo.Weekhot);

            __xmlDoc.Save(DataFilePath);
        }
      
    }
}
