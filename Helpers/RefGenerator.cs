using System;
using System.Text;

namespace PerfumeStore.Helpers
{
    public class RefGenerator
    {
        private readonly Random random= new Random();
       public int RandomNumber(int max, int min)
        { return random.Next(max, min); }

        public string RandomString(int size, bool lowerCase=false) 
        {
            var builder = new StringBuilder(size);
            
            char offset = lowerCase? 'a' : 'A';
            const int lettersOffset= 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString().ToUpper();

        }

        public string OrdRefNumber()
        {
            var idbuilder = new StringBuilder();

            //9 digits btw 100000000 and 999999999
            idbuilder.Append(RandomNumber(1000000000, 999999999));

            //Add Letters 

            idbuilder.Append(RandomString(3));

            return idbuilder.ToString();
        }
    }
}
