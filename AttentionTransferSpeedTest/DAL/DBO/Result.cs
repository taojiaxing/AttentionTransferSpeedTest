using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttentionTransferSpeedTest.DAL.DBO
{
    class Result
    {
        public string Name { get; set; }
        public int Num { get; set; }
        public int ISI { get; set; }
        public string Combination { get; set; }
        public int P{ get; set; }
        public int Correct { get; set; }
        public int Input { get; set; }
        public int RT { get; set; }
    }
}
