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
    class Elemental : Mutant
    {
        const int REGION_MAX = 10; //max and min value
        const int REGION_MIN = 1;

        private int region;

        public int Region
        {
            get => region;
            set
            {
                if (value >= REGION_MIN && value <= REGION_MAX)
                {
                    region = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Region need to be ranged from 1 to 10."); //if the value is not in between 1 and 10
                }
            }
        }

        public Elemental()
        {
            //default
        }

        public Elemental(string name, int level, int region, GeoCoordinate location):base(name, level, location)
        {
            Region = region;
        }

        public override double Calculate()
        {
            return Region * Level;
        }

        public override string ToString()
        {
            return base.ToString() +
                string.Format("Region: {0, 43}\n", Region);
        }
    }
}
