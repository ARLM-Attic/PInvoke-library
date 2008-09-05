//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         TEL:86-28-85098980/66026180                   ==
//==               Code By Arjun                           ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Config
{
    public class Series
    {
        private string _Code;
        private string _tempStr;
        public string Code
        {
            get
            {
                return _Code;
            }
        }
        public Series()
        {
            _Code = GetSeries();
        }

        private string GetSeries()
        {
            int i;
            string returnStr = "";
            string tempStr = "";
            for (i = 0; i < 5; i++)
            {
                _tempStr = tempStr;
                tempStr = GetOneSeries();
                while (!Check(tempStr) || (tempStr == _tempStr))
                {
                    tempStr = GetOneSeries();
                }
                if (i == 0)
                {
                    returnStr = tempStr;
                }
                else
                {
                    returnStr += "-" + tempStr;
                }
            }
            return returnStr;
        }


        private string GetOneSeries()
        {
            string returnStr = "";
            //string[] tempArray;
            string randomNum;
            Random rnd = new Random();
            int i = 1;
            string oldStr = "";
            while (i <= 5)
            {
                int rndnum = rnd.Next(47, 91);
                randomNum = Convert.ToString(rndnum);
                if (((rndnum >= 48) && (rndnum <= 57)) || ((rndnum >= 65) && (rndnum <= 90)))
                {
                    if ((oldStr == "") || (oldStr.Substring(0, 1) != randomNum.Substring(0, 1)))
                    {
                        returnStr += (char)rndnum;
                        oldStr = randomNum;
                        i++;
                    }
                }
            }
            return returnStr;
        }


        private bool Check(string str)
        {
            bool returnBool = false;
            string tempNum = "1";
            string tempStr;
            int i;
            if (str.Length == 5)
            {
                for (i = 0; i < str.Length; i++)
                {
                    tempStr = str.Substring(i, 1);
                    tempNum = cheng(tempNum, Convert.ToString((int)Convert.ToChar(tempStr)));
                }
                string tempNumStr = tempNum;
                if (tempNumStr.Substring(tempNumStr.Length - 1, 1) == "1")
                {
                    returnBool = true;
                }
            }

            return returnBool;
        }

        public bool FsConfig(string str)
        {
            string[] tempArray;
            int i;
            bool flag = false;
            bool tempFlag = true;
            tempArray = str.Split('-');
            for (i = 0; i < tempArray.Length; i++)
            {
                if (Check(tempArray[i]))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                tempFlag = tempFlag && flag;
            }
            return tempFlag && flag;
        }

        public string EnPas(string CodeStr)
        {
            int codeLen = 30;
            int codeSpace = codeLen - CodeStr.Length;
            string newCode;
            string returnStr = "";
            string CodeString = CodeStr;
            int cecr;
            int cecb;
            int cec;
            if (codeSpace >= 1)
            {
                for (cecr = 1; cecr <= codeSpace; cecr++)
                {
                    CodeString += (char)21;
                }
            }
            newCode = "1";
            //long neCode = 1;
            long been;
            for (cecb = 1; cecb <= codeLen; cecb++)
            {
                been = Convert.ToInt64(codeLen) + Convert.ToInt64((long)(Convert.ToChar(CodeString.Substring(cecb - 1, 1)))) * cecb;
                newCode = cheng(newCode, Convert.ToString(been));
            }
            //newCode = Convert.ToString(neCode);
            CodeString = quling(newCode);
            newCode = "";
            for (cec = 0; cec < CodeString.Length; cec++)
            {
                if (cec < CodeString.Length - 3)
                {
                    newCode += CfsCode(CodeString.Substring(cec, 3));
                }
                else
                {
                    newCode += CfsCode(CodeString.Substring(cec, CodeString.Length - cec));
                    //Response.Write(CodeString.Substring(cec, CodeString.Length - cec));
                    //Response.Write("<BR>");
                }

            }
            for (cec = 20; cec < newCode.Length - 16; cec += 2)
            {
                returnStr += newCode.Substring(cec - 1, 1);
            }
            return returnStr;
        }

        public string CfsCode(string Word)
        {
            int cc;
            string returnStr = "";
            for (cc = 1; cc <= Word.Length; cc++)
            {
                returnStr += Convert.ToString((int)(Convert.ToChar(Word.Substring(cc - 1, 1))));
            }
            return Convert.ToString(Convert.ToUInt32(returnStr), 16).ToUpper();
        }

        private string cheng(string a, string b)
        {
            string Aa = a;
            int[] A = new int[Aa.Length];
            for (int i = 0; i < Aa.Length; i++)
            {
                A[Aa.Length - 1 - i] = Convert.ToInt32(Aa.Substring(i, 1));
                //Console.WriteLine(A[Aa.Length - 1 - i]);
            }

            // Console.Write("Number B is : ");
            string Bb = b;
            int[] B = new int[Bb.Length];
            for (int i = 0; i < Bb.Length; i++)
            {
                B[Bb.Length - 1 - i] = Convert.ToInt32(Bb.Substring(i, 1));
                //Console.WriteLine(B[Bb.Length - 1 - i]);
            }
            int[,] sum = new int[Aa.Length, Bb.Length];
            int[] wsum = new int[Aa.Length + Bb.Length];
            for (int i = 0; i < Aa.Length; i++)
            {
                for (int j = 0; j < Bb.Length; j++)
                {
                    sum[i, j] = A[i] * B[j];
                    wsum[i + j] = wsum[i + j] + sum[i, j];
                    if (wsum[i + j].ToString().Length > 1)
                    {
                        wsum[i + j + 1] = wsum[i + j + 1] + wsum[i + j] / 10;
                        wsum[i + j] = Convert.ToInt32(wsum[i + j].ToString().Substring(1, 1));
                    }
                    // Console.WriteLine("sum[" + i + "," + j + "] = {0}", sum[i, j]);
                }
            }
            string end = "";
            for (int m = 0; m < Aa.Length + Bb.Length; m++)
            {
                //Console.WriteLine("wsum[" + m + "] = {0}", wsum[m]);
                end = wsum[m].ToString() + end;
            }
            return end;
        }

        private string quling(string str)
        {
            int m = 0;
            for (int i = 0; i < str.Length; i++)
            {

                if (str.Substring(i, 1) != "0")
                {
                    break;
                }
                m++;
            }

            return ss(str.Substring(m, 1) + "." + str.Substring(m, str.Length - m).Substring(1, 15)) + "E+" + Convert.ToString((str.Length - 1 - m));
        }

        private string ss(string str)
        {
            Double istr = Convert.ToDouble(str) * Math.Pow(10, str.Length - 3);
            istr = Math.Round(istr);
            istr = istr / Math.Pow(10, str.Length - 3);
            string tempStr = Convert.ToString(istr);
            return tempStr;
        }
    }
}
