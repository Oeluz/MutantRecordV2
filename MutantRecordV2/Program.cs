using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using GeoCoordinatePortable;

/* Author:      Zhencheng Chen
 * Program:     MRTF Data Recording Program where the user will load and save the mutants' data (Version 2)
 * Date:        3/17/2019
 * Instructor:  Kelley Duran
 */

namespace MutantRecordV2
{
    class Program
    {
        const string FILE_NAME = "MutantCrisis.dat";
        const int DANGER_QUOTIENT_THREAT_VALUE = 10000;
        const int DISTANCE_ALERT_MINIMUM = 25;
        //for the if and else statement to display the string in red if danger quotient is over 10,000

        static List<Mutant> mutantList = new List<Mutant>();

        static void Main(string[] args)
        {
            Console.Title = "Mutant Record Version 2";
            LoadFile();

            do
            {
                try
                {
                    DisplayMenu(); //Displaying the menu
                    if (int.TryParse(Console.ReadLine(), out int option) && option >= 0 && option < 7)
                    {
                        switch (option)
                        {
                            case 1:
                                NewMutant(); //add new mutant
                                break;

                            case 2:
                                RemoveMutant(); //remove an existing mutant
                                break;

                            case 3:
                                UpdateLocation(); //Update the location
                                break;

                            case 4:
                                DisplayDQ(); //display the danger quotient report which include --> name, power level, type, their charasterics and danger quotient
                                break;

                            case 5:
                                SaveFile(); //save the list into the mutantCrisis.dat file
                                break;

                            case 6:
                                Console.WriteLine("Exiting...");
                                SaveFile(); //save automatically if the user exit
                                System.Threading.Thread.Sleep(1000);
                                Environment.Exit(0);
                                break;

                            default:
                                break;
                        }
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message); //if anything in the code throw exception
                }
            } while (true);
        }

        private static void LoadFile() //to load the file once the file open up
        {
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    using (StreamReader sr = new StreamReader(FILE_NAME))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            var token = line.Split(',');
                            double.TryParse(token[7], out double latitude);
                            double.TryParse(token[8], out double longitude);

                            if (token[0].ToLower() == "psychic") //Instanize new objects based on the type and assign the variables/property
                            {
                                mutantList.Add(new Psychic(token[1], int.Parse(token[2]), int.Parse(token[3]), int.Parse(token[4]), new GeoCoordinate(latitude, longitude)));
                            }
                            else if (token[0].ToLower() == "physical")
                            {
                                mutantList.Add(new Physical(token[1], int.Parse(token[2]), int.Parse(token[3]), int.Parse(token[5]), new GeoCoordinate(latitude, longitude)));
                            }
                            else if (token[0].ToLower() == "elemental")
                            {
                                mutantList.Add(new Elemental(token[1], int.Parse(token[2]), int.Parse(token[6]), new GeoCoordinate(latitude, longitude)));
                            }
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException("Unable to load the existing record due to the lack of the file"); //If the file is not found, the message will display
                }
            }
            catch (FileNotFoundException x)
            {
                Console.WriteLine(x.Message); //catch the exception thrown
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message); //catch other exception
            }

        }

        private static void SaveFile() //save the file into mutantCrisis.dat file
        {
            try
            {
                string comma = ",";
                StreamWriter sw = new StreamWriter(FILE_NAME);
                using (sw)
                {
                    foreach (Mutant var in mutantList) //write the lines based on the type
                    {
                        if (var is Psychic)
                        {
                            Psychic savePsychic = (Psychic)var;
                            sw.WriteLine("Psychic" + comma +
                                savePsychic.Name + comma +
                                savePsychic.Level + comma +
                                savePsychic.Iq + comma +
                                savePsychic.UsageCount + comma +
                                comma + comma + savePsychic.Location.Latitude +
                                comma + savePsychic.Location.Longitude);
                        }
                        else if (var is Physical)
                        {
                            Physical savePhysical = (Physical)var;
                            sw.WriteLine("Physical" + comma +
                                savePhysical.Name + comma +
                                savePhysical.Level + comma +
                                savePhysical.Iq + comma +
                                comma + savePhysical.Strength +
                                comma + comma + savePhysical.Location.Latitude +
                                comma + savePhysical.Location.Longitude);
                        }
                        else if (var is Elemental)
                        {
                            Elemental saveElemental = (Elemental)var;
                            sw.WriteLine("Elemental" + comma +
                                saveElemental.Name + comma +
                                saveElemental.Level + comma +
                                comma + comma + comma +
                                saveElemental.Region + comma +
                                saveElemental.Location.Latitude + comma +
                                saveElemental.Location.Longitude);
                        }
                    }
                    Console.WriteLine($"The .dat file {FILE_NAME} is saved successfully."); // if it is successful
                }
            }
            catch (Exception x) //Display error message if the streamwriter fail
            {
                Console.WriteLine(x.Message);
            }
        }

        private static void DisplayMenu() //the method to display the menu 
        {
            Console.Write("\n1. Enter a mutant's data\n" +
                "2. Delete a mutant's data\n" +
                "3. Update a mutant's location\n" +
                "4. Display a danger quotient report\n" +
                "5. Save the data\n" +
                "6. Exit the program\n" +
                "\nEnter your option: ");
        }

        private static void NewMutant() //the method to add new mutant
        {
            Console.Write("\nPlease pick the mutant type of the mutant that you desire to enter...\n" + //ask which type of mutant to add
                "1. Psychic\n" +
                "2. Physical\n" +
                "3. Elemental\n" +
                "4. Go back to the menu" +
                "\n\nPlease enter your option: ");
            if (int.TryParse(Console.ReadLine(), out int option) && option > 0 && option < 5)
            {
                string name = "";
                int level = 0;
                int iq = 0;

                switch (option)
                {
                    case 1:
                        Console.Write("The Name of the Mutant: ");
                        name = Console.ReadLine();

                        foreach (Mutant var in mutantList) //same to other 2 types of mutant, if the name is already existed. It will exit to the main menu
                        {
                            if (var.Name == name)
                            {
                                Console.WriteLine("The name is existed in the record.\n" +
                                    "Exiting to main menu....");
                                System.Threading.Thread.Sleep(1000);
                                break;
                            }
                        }

                        Console.Write("Level of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out level);

                        Console.Write("The IQ of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out iq);

                        Console.Write("The Usage Count of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out int usageCount);

                        Console.Write("The Current Location of the Mutant (latitude,longitude): ");
                        var locationSplit = Console.ReadLine().Split(',');
                        double.TryParse(locationSplit[0], out double latitude);
                        double.TryParse(locationSplit[1], out double longitude);

                        mutantList.Add(new Psychic(name, level, iq, usageCount, new GeoCoordinate(latitude, longitude)));
                        break;

                    case 2:
                        Console.Write("The Name of the Mutant: ");
                        name = Console.ReadLine();

                        foreach (Mutant var in mutantList)
                        {
                            if (var.Name == name)
                            {
                                Console.WriteLine("The name is existed in the record.\n" +
                                    "Exiting to main menu....");
                                System.Threading.Thread.Sleep(1000);
                                break;
                            }
                        }

                        Console.Write("Level of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out level);

                        Console.Write("The IQ of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out iq);

                        Console.Write("The Strength of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out int strength);

                        Console.Write("The Current Location of the Mutant (latitude,longitude): ");
                        locationSplit = Console.ReadLine().Split(',');
                        double.TryParse(locationSplit[0], out latitude);
                        double.TryParse(locationSplit[1], out longitude);

                        mutantList.Add(new Physical(name, level, iq, strength, new GeoCoordinate(latitude, longitude)));
                        break;

                    case 3:
                        Console.Write("The Name of the Mutant: ");
                        name = Console.ReadLine();

                        foreach (Mutant var in mutantList)
                        {
                            if (var.Name == name)
                            {
                                Console.WriteLine("The name is existed in the record.\n" +
                                    "Exiting to main menu....");
                                System.Threading.Thread.Sleep(1000);
                                break;
                            }
                        }

                        Console.Write("Level of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out level);

                        Console.Write("Region of the Mutant: ");
                        int.TryParse(Console.ReadLine(), out int region);

                        Console.Write("The Current Location of the Mutant (latitude,longitude): ");
                        locationSplit = Console.ReadLine().Split(',');
                        double.TryParse(locationSplit[0], out latitude);
                        double.TryParse(locationSplit[1], out longitude);

                        mutantList.Add(new Elemental(name, level, region, new GeoCoordinate(latitude, longitude)));
                        break;

                    case 4: //leave to main menu
                        break;
                }
            }


        }

        private static void RemoveMutant() //remove the existing mutant
        {
            Console.Write("\nThe Name of the Mutant you desire to remove: ");
            string name = Console.ReadLine();
            bool found = true; //to confirm if the mutant is removed or not
            foreach (Mutant var in mutantList)
            {
                if (var.Name == name)
                {
                    mutantList.Remove(var);
                    found = true;
                    break;
                }
                else
                {
                    found = false; //if the mutant did not find which mean it did not get removed or it is not there
                }
            }

            if (!found)
            {
                Console.WriteLine("The mutant can't be found.\n" +
                    "Please try again.\n" +
                    "Exiting to main menu.....");
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void DisplayDQ() //display the danger quotient report
        {
            foreach (Mutant var in mutantList)
            {
                if (var is Psychic)
                {
                    Console.WriteLine("\n" + ((Psychic)var).ToString());
                    if (((Psychic)var).Calculate() >= DANGER_QUOTIENT_THREAT_VALUE) //if it is great than 10,000
                    {
                        Console.ForegroundColor = ConsoleColor.Red; //display the words in red
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine("Danger Quotient: {0, 34}", ((Psychic)var).Calculate());
                    Console.ResetColor(); //reset the color
                }
                else if (var is Physical)
                {
                    Console.WriteLine("\n" + ((Physical)var).ToString());
                    if (((Physical)var).Calculate() >= DANGER_QUOTIENT_THREAT_VALUE)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine("Danger Quotient: {0, 34}", ((Physical)var).Calculate());
                    Console.ResetColor();
                }
                else if (var is Elemental)
                {
                    Console.WriteLine("\n" + ((Elemental)var).ToString());
                    if (((Elemental)var).Calculate() >= DANGER_QUOTIENT_THREAT_VALUE)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine("Danger Quotient: {0, 34}", ((Elemental)var).Calculate());
                    Console.ResetColor();
                }
                Console.WriteLine("-------------------------------------------------------------------");
            }
        }

        private static void UpdateLocation()
        {
            Console.Write("Please enter the Mutant's name: ");
            string name = Console.ReadLine();
            bool found = true;
            foreach (Mutant var in mutantList)
            {
                if (var.Name == name)
                {
                    var.EventLocation += LocationChangeHandler;
                    GeoCoordinate previousLocation = new GeoCoordinate(var.Location.Latitude, var.Location.Longitude);

                    Console.Write("The Current Location of the Mutant (latitude,longitude): ");
                    var locationSplit = Console.ReadLine().Split(',');
                    double.TryParse(locationSplit[0], out double latitude);
                    double.TryParse(locationSplit[1], out double longitude);

                    var.Location = new GeoCoordinate(latitude, longitude);
                    found = true;
                    break;
                }
                else
                {
                    found = false; //if the mutant did not find which mean it did not get removed or it is not there
                }
            }

            if (!found)
            {
                Console.WriteLine("The mutant can't be found.\n" +
                    "Please try again.\n" +
                    "Exiting to main menu.....");
                System.Threading.Thread.Sleep(1000);
            }

        }

        private static void LocationChangeHandler(GeoCoordinate previousLocation, GeoCoordinate currentLocation)
        {
            double distance = previousLocation.GetDistanceTo(currentLocation) / 1000;
            if (distance >= DISTANCE_ALERT_MINIMUM)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The mutant's previous and current location is greater than 25km");
                Console.ResetColor();
            }
        }
    }
}
