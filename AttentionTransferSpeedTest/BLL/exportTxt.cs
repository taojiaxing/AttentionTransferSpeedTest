using AttentionTransferSpeedTest.DAL.DBO;
using System.Collections.Generic;
using System.IO;

namespace AttentionTransferSpeedTest.BLL
{
    internal class exportTxt
    {
        public void txt(User user, List<Result> results, string path)
        {
            FileStream aFile = new FileStream(path + "/" + user.Name + "测试报告.txt", FileMode.OpenOrCreate);
            StreamWriter file = new StreamWriter(aFile);
            string line1 = "测试时间：" + user.Time;
            string line2 = "测试类型：空间注意   被试名：" + user.Name + "  电话: " + user.Tel;
            string line3 = "============================================================================================================================";
            string line4 = "序号     测试条件     信号组合     指向位置     正确答案      被试答案      是否正确      RT 时间";
            file.WriteLine(line1);
            file.WriteLine(line2);
            file.WriteLine(line3);
            file.WriteLine(line4);
            foreach (Result result in results)
            {
                int numm = result.Num;
                string nums;
                if (numm < 10)
                {
                    nums = numm.ToString() + " ";
                }
                else
                {
                    nums = numm.ToString();
                }
                int pps = result.P;
                string ppps;
                if (pps < 10)
                {
                    ppps = pps + " ";
                }
                else
                {
                    ppps = pps.ToString();
                }
                string oiii;
                if (result.ISI < 1000 && result.ISI > 100)
                {
                    oiii = result.ISI + " ";
                }
                if (result.ISI < 100)
                {
                    oiii = result.ISI + "  ";
                }
                else
                {
                    oiii = result.ISI.ToString();
                }
                string lines = nums + "        " + oiii + "       " + result.Combination + "       " + ppps + "           " +
                    result.Correct.ToString() + "              " + result.Input.ToString() + "            " + result.isRight.ToString() + "            " + result.RT.ToString();
                file.WriteLine(lines);
            }
            file.Close();
        }
    }
}