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
    public delegate void MutantLocation(GeoCoordinate previousLocation, GeoCoordinate currentLocation);

    abstract class Mutant
    {
        const int LEVEL_MAX = 2000; //The max value
        const int LEVEL_MIN = 1; // The min value


        private string name;
        private int level;
        private GeoCoordinate location;

        public string Name { get => name; set => name = value; }

        public int Level
        {
            get => level;
            set
            {
                if (value >= LEVEL_MIN && value <= LEVEL_MAX)
                {
                    level = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Level need to be ranged from 1 to 2000"); //if the value is not in between 1 and 2000
                }
            }
        }

        public GeoCoordinate Location
        {
            get => location;
            set
            {
                GeoCoordinate previousLocation = Location;
                location = value;
                EventLocation?.Invoke(previousLocation, location);
            }
        }

        public event MutantLocation EventLocation;

        public Mutant()
        {
            //default
        }

        public Mutant(string name, int level, GeoCoordinate location)
        {
            Name = name;
            Level = level;
            Location = location;
        }

        public virtual double Calculate()
        {
            return 0;
        }

        public override string ToString()
        {
            return string.Format("Name: {0, 45}\n", Name) +
                string.Format("Level: {0, 44}\n", Level) +
                string.Format("Location: {0, 41}\n", Location);
        }

    }
}
