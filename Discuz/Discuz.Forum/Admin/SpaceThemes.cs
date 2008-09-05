using System.Xml;

namespace Discuz.Forum
{
    /// <summary>
    /// ��̳ģ�������
    /// </summary>
    public class SpaceThemes
    {
        /// <summary>
        /// ģ��˵���ṹ, ÿ��ģ��Ŀ¼�¾���ʹ��ָ���ṹ��xml�ļ���˵����ģ��Ļ�����Ϣ
        /// </summary>
        public struct SpaceThemeAboutInfo
        {
            /// <summary>
            /// ģ������
            /// </summary>
            public string name;
            /// <summary>
            /// ����
            /// </summary>
            public string author;
            /// <summary>
            /// ��������
            /// </summary>
            public string createdate;
            /// <summary>
            /// ģ��汾
            /// </summary>
            public string ver;
            /// <summary>
            /// ģ�����õ���̳�汾
            /// </summary>
            public string fordntver;
            /// <summary>
            /// ��Ȩ����
            /// </summary>
            public string copyright;

        }


        /// <summary>
        /// ��ģ��˵���ļ��л��ģ��˵����Ϣ
        /// </summary>
        /// <param name="xmlPath">ģ��·��(�������ļ���)</param>
        /// <returns>ģ��˵����Ϣ</returns>
        public static SpaceThemeAboutInfo GetThemeAboutInfo(string xmlPath)
        {
            SpaceThemeAboutInfo __aboutinfo = new SpaceThemeAboutInfo();
            __aboutinfo.name = "";
            __aboutinfo.author = "";
            __aboutinfo.createdate = "";
            __aboutinfo.ver = "";
            __aboutinfo.fordntver = "";
            __aboutinfo.copyright = "";

            ///��Ź�����Ϣ���ļ� about.xml�Ƿ����,�����ڷ��ؿմ�
            if (!System.IO.File.Exists(xmlPath + @"\about.xml"))
            {
                return __aboutinfo;
            }


            XmlDocument xml = new XmlDocument();

            xml.Load(xmlPath + @"\about.xml");

            XmlNode root = xml.SelectSingleNode("about");
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "template")
                {
                    XmlAttribute name = n.Attributes["name"];
                    XmlAttribute author = n.Attributes["author"];
                    XmlAttribute createdate = n.Attributes["createdate"];
                    XmlAttribute ver = n.Attributes["ver"];
                    XmlAttribute fordntver = n.Attributes["fordntver"];
                    XmlAttribute copyright = n.Attributes["copyright"];

                    if (name != null)
                    {
                        __aboutinfo.name = name.Value.ToString();
                    }

                    if (author != null)
                    {
                        __aboutinfo.author = author.Value.ToString();
                    }

                    if (createdate != null)
                    {
                        __aboutinfo.createdate = createdate.Value.ToString();
                    }

                    if (ver != null)
                    {
                        __aboutinfo.ver = ver.Value.ToString();
                    }

                    if (fordntver != null)
                    {
                        __aboutinfo.fordntver = fordntver.Value.ToString();
                    }

                    if (copyright != null)
                    {
                        __aboutinfo.copyright = copyright.Value.ToString();
                    }


                }
            }
            return __aboutinfo;
        }
    }
}
