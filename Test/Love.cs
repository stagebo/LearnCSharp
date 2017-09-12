using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Love
    {
        public static void Run()
        {
            MakeLove();
        }
        /**
         *  for (y = love; y > -love; y -= 0.06*love)
            {
                for (x = -love; x < love; x += 0.03*love)
                {
                    z = x * x + y * y - 0.6*love;
                    std::cout<<(z * z * z - x * x * y * y * y <= 0.0 ? '*' : ' ');
                }
                std::cout<<std::endl;
             }*/
        public static void MakeLove()
        {
            double love = 2;
            double x, y, z;
            for (y = love; y > -love; y -= 0.06 * love)
            {
                for (x = -love; x < love; x += 0.03 * love)
                {
                    z = x * x + y * y - 0.6 * love;
                    Console.Write((z * z * z - x * x * y * y * y <= 0.0 ? '*' : ' '));
                }
                Console.WriteLine();
            }
        }
    }
}
