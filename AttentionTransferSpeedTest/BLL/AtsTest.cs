using System;

namespace AttentionTransferSpeedTest.SYS
{
    internal class AtsTest
    {
        private int randomPicture()
        {
            Random rd = new Random();
            int x = rd.Next(1, 4);
            if (x == 4)
            {
                x = 5;
            }
            return x;
        }

        private int randomPoint()
        {
            Random rd = new Random();
            int x = rd.Next(1, 12);
            return x;
        }
    }
}