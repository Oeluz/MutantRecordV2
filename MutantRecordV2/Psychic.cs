using System;
using System.Collections.Generic;
using System.Text;
using GeoCoordinatePortable;

/* Author:      Zhencheng Chen
 * Program:     MRTF Data Recording Program where the user will load and save the mutants' data (Version 2)
 * Date:        3/17/2019
 * Instructor:  Kelley Duran
 */
namespace MutantRecordV2
{
    class Psychic : Mutant
    {
        const int IQ_MAX = 200;  //constant variable for max and min value 
        const int IQ_MIN = 40;
        const int USAGE_COUNT_MAX = 10;
        const int USAGE_COUNT_MIN = 1;

        private int iq;
        private int usageCount;

        public int Iq
        {
            get => iq;
            set
            {
                if (value <= IQ_MAX && value >= IQ_MIN)
                {
                    iq = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Intelligene Quotient need to be ranged from 40 to 200"); //if it is not in between 40 and 200
                }
            }
        }
        public int UsageCount
        {
            get => usageCount;
            set
            {
                if (value <= USAGE_COUNT_MAX && value >= USAGE_COUNT_MIN)
                {
                    usageCount = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Usage Count need to be ranged from 1 to 10"); //if it is not in between 1 and 10
                }
            }
        }

        public Psychic()
        {
            //default
        }

        public Psychic(string name, int level, int iq, int usageCount, GeoCoordinate location) : base(name, level, location)
        {
            Iq = iq;
            UsageCount = usageCount;
        }

        public override double Calculate()
        {
            return Iq * UsageCount * Level;
        }

        public override string ToString()
        {
            return base.ToString() +
                string.Format("Intelligence Quotient: {0, 28}\n", Iq) +
                string.Format("Usage Count: {0, 38}\n", UsageCount);
        }
    }
}
