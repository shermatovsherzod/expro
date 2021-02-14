using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expro.Common.Utilities
{
    public static class RandomUtils//<T> where T : class
    {
        /// <summary>
        /// if unable to convert then return DateTime.MinValue
        /// </summary>
        /// <param name="inputDatetimeString"></param>
        /// <returns></returns>
        public static List<int> ExtractRandomInts(List<int> incomingElements, int count)
        {
            List<int> randomlySelectedIDs = new List<int>();

            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                if (incomingElements.Count == 0)
                    break;

                int randomIndex = rnd.Next(0, incomingElements.Count);
                randomlySelectedIDs.Add(incomingElements[randomIndex]);
                incomingElements.RemoveAt(randomIndex);
            }

            return randomlySelectedIDs;
        }
    }
}
