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
    class Physical : Mutant
    {
        const int IQ_MAX = 200; //max and min values
        const int IQ_MIN = 40;
        const int STRENGTH_MAX = 10;
        const int STRENGTH_MIN = 1;

        private int iq;
        private int strength;

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
                    throw new ArgumentOutOfRangeException("Intelligene Quotient need to be ranged from 40 to 200"); // if the value is not in between 40 and 200
                }
            }
        }
        public int Strength
        {
            get => strength;
            set
            {
                if (value >= STRENGTH_MIN && value <= STRENGTH_MAX)
                {
                    strength = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Strength need to be ranged from 1 to 10."); // if the value is not in between 1 and 10
                }
            }
        }

        public Physical()
        {
            //default
        }

        public Physical(string name, int level, int iq, int strength, GeoCoordinate location):base(name, level, location)
        {
            Iq = iq;
            Strength = strength;
        }

        public override double Calculate()
        {
            return Iq * Strength * Level;
        }

        public override string ToString()
        {
            return base.ToString() +
                string.Format("Intelligence Quotient: {0, 28}\n", Iq) +
                string.Format("Strength: {0, 41}\n", Strength);
        }
    }
}
