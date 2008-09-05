using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    public enum EnumCstmFrmItemType
    {
        SingleLineText,
        MultiLineText,
        PassWordText,
        DateTime,
        RadioBox,
        CheckBox,
        Numberic,
        UploadFile,
        DropList,
        List
    }
    /// <summary>
    /// 正定义表单
    /// </summary>
    [Serializable]
    public class CustomFormInfo
    {
        public int id;
        public string formname = string.Empty;
        public string formtablename = string.Empty;
        public string accessorypath = string.Empty;
        public int accessorysize;
        public string memo = string.Empty;
        public bool islock;
        public bool timelimited;
        public DateTime starttime;
        public DateTime endtime;
        public bool showvalidatecode;
        public bool submitonce;
        public byte isdelpoint;
        public int gpoint;
        public int ipoint;
        public string groupnumber = string.Empty;
    }
    /// <summary>
    /// 自定义表单项
    /// </summary>
    [Serializable]
    public class CustomFormItemInfo
    {
        public string formname = string.Empty;
        public string fieldname = string.Empty;
        public int id;
        public int seriesnumber;
        public int formid;
        public string itemname = string.Empty;
        public EnumCstmFrmItemType itemtype;
        public string defaultvalue = string.Empty;
        public bool isnotnull;
        public int itemsize = 0;
        public bool islock;
        public string prompt = string.Empty;
        public string selectitem = string.Empty;

        static public string GetFieldTypeName(EnumCstmFrmItemType ItemType)
        {
            switch (ItemType)
            {
                case  EnumCstmFrmItemType.SingleLineText:
                    return "单行文本";
                case EnumCstmFrmItemType.MultiLineText:
                    return "多行文本";
                case EnumCstmFrmItemType.PassWordText:
                    return "密码";
                case  EnumCstmFrmItemType.RadioBox:
                    return "单选项";
                case EnumCstmFrmItemType.CheckBox:
                    return "多选项";
                case EnumCstmFrmItemType.DateTime:
                    return "日期时间";
                case  EnumCstmFrmItemType.UploadFile:
                    return "附件";
                case EnumCstmFrmItemType.Numberic:
                    return "数字";
                case EnumCstmFrmItemType.DropList:
                    return "下拉框";
                case EnumCstmFrmItemType.List:
                    return "列表框";
                default:
                    return string.Empty;
            }
        }
    }
}
