using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using CritterBrains;


namespace _100444144
{
    //This critter is able to find the exit to a level if there is no terrain blocking, if not
    //it will continue to react to events such as a neraby critter being much weaker
    //eating food for energy that he is passing as well as attempting to avoid poo, however if poo spawns
    //close by then there is a chance he will turn and head straight for it
    public class One : CritterBrains.CritterBrain
    {
        OneConfiguration configuration = null;
        string ConfigurationFileName = "One.txt";
        //integers for keeping track of last known poop. To avoid reacting to the same poop without moving
        int lastPoopX;
        int lastPoopY;
        //X and Y co-ords of the exit
        int centreDestinationX;
        int centreDestinationY;
        //allows for random movement when hitting a wall
        Random random = new Random();
        public One() : base("Pedro", "Ryan Skull")
        {
            LoadConfiguration();
        }

        public void LoadConfiguration()
        {
            TextReader reader = null;
            try
            {
                configuration = new OneConfiguration();
                reader = new StreamReader(ConfigurationFileName);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //splits the string based on '=' sign, part after the equals is the value of the corresponding attribute
                    string[] components = line.Split('=');
                    if (components.Length != 2)
                        continue;
                    string key = components[0];
                    string value = components[1];
                    //uses first part of string to know which attribute to change
                    if (key == "nominalSpeed")
                    {
                        if (int.TryParse(value, out int nominalSpeed))
                        {
                            configuration.NominalSpeed = nominalSpeed;
                        }
                    }
                    if (key == "NotifyFood")
                    {
                        if (int.TryParse(value, out int notifyFood))
                        {
                            configuration.NominalSpeed = notifyFood;
                        }
                    }
                    if (key == "NotifyPoo")
                    {
                        if (int.TryParse(value, out int notifyPoo))
                        {
                            configuration.NominalSpeed = notifyPoo;
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(ConfigurationFileName + " does not exist. Using defaults.");
            }
            catch (Exception e)
            {
                Console.WriteLine("LoadConfiguration error: " + e.Message);
            }
            finally
            {
                //closes the reader
                if (reader != null)
                    reader.Close();
            }
        }

        public void SaveConfiguration()
        {
            TextWriter writer = null;
            try
            {
                //writes the attribute name and value to One.txt
                writer = new StreamWriter(ConfigurationFileName);
                writer.WriteLine("nominalSpeed=" + configuration.NominalSpeed);
                writer.WriteLine("NotifyFood=" + configuration.NotifyFood);
                writer.WriteLine("NotifyPoo=" + configuration.NotifyPoo);
            }
            catch (Exception e)
            {
                Console.WriteLine("SaveConfiguration error: " + e.Message);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public void ObtainDefaultConfiguration()
        {
            configuration = new OneConfiguration();
        }
        //Used to be able to open the form
        public override Form Form
        {
            get
            {
                return new OneConfigForm(this);
            }
        }
        //Properties
        // Speed - the speed of the critter
        public int Speed
        {
            get
            {
                return configuration.NominalSpeed;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Speed must be greater than or equal to zero.");
                }
                configuration.NominalSpeed = value;
            }
        }
        //NotifyFood and poo are there to allow the configuration menu to be able to disable the critter from reacting to food or poo
        public int NotifyFood
        {
            get
            {
                return configuration.NotifyFood;
            }
            set
            {
                configuration.NotifyFood = value;
            }
        }
        public int NotifyPoo
        {
            get
            {
                return configuration.NotifyFood;
            }
            set
            {
                configuration.NotifyPoo = value;
            }
        }

        //Methods for stuff

        //Easy way to change the critter direction based on its current direction
        private void ChangeDirection(int angle)
        {
            Critter.Direction += angle;
        }

        //Resets poop knowledge and speed
        private void Reset()
        {
            lastPoopX = 0;
            lastPoopY = 0;
        }

        //math to get the distance between the critter and a thing
        private double DrawLine(int x, int y)
        {
            double xDistance = Critter.X - x;
            double yDistance = Critter.Y - y;
            double totalDistance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
            return totalDistance;
        }

        //finds coords of the exit point
        private void FindLevelExit()
        {
            Rectangle destination = Critter.GetDestination();
            centreDestinationX = destination.X + destination.Width / 2;
            centreDestinationY = destination.Y + destination.Height / 2;
        }

        //heads to exit if the path is not blocked
        private void HeadToExit()
        {
            if (!Critter.IsTerrainBlockingRouteTo(centreDestinationX, centreDestinationY))
            {
                int direction = Critter.GetDirectionTo(centreDestinationX, centreDestinationY);
                Critter.Direction = direction;
            }
        }

        public override void Birth()
        {
            //If exit is visible critter will head there if not he will go off to the right andrespond to events
            Critter.Speed = configuration.NominalSpeed;
            Reset();
            FindLevelExit();
            Critter.Direction = 90;
            HeadToExit();
        }

        public override void Think()
        {
            //if exit has become visible will head there
            HeadToExit();
            //Updates values after the configuration form has been used
            Critter.Speed = configuration.NominalSpeed;
            NotifyPoo = configuration.NotifyPoo;
            NotifyFood = configuration.NotifyFood;
        }

        public override void NotifyBlockedByTerrain()
        {
            //Randomly bounces away from terrain upon colliding
            ChangeDirection(random.Next(45, 315));
            Reset();
            HeadToExit();
        }

        public override void NotifyBumpedCritter(OtherCritter other)
        {
            //if the other critter is much weaker then this critter will try to kill
            if (other.Strength == Strength.MuchWeaker)
            {
                other.Attack();
            }
            else
            {
                ChangeDirection(180);
            }
            Critter.Speed = configuration.NominalSpeed;
        }

        public override void NotifyBumpedObject(IWorldObject thing)
        {
        }

        public override void NotifyCloseToCritter(OtherCritter otherCritter)
        {
            //If the nearby critter is closer then this critter will try and go faster than it currently is to catch up
            if (otherCritter.Strength == Strength.MuchWeaker)
            {
                Critter.Speed = configuration.NominalSpeed + 2;
                Critter.Direction = otherCritter.DirectionTo;
            }
        }

        public override void NotifyCloseToFood(int x, int y)
        {
            //if food is nearby then head straight to it
            if (NotifyFood == 1)
            {
                if (!Critter.IsTerrainBlockingRouteTo(x, y))
                {
                    Critter.Direction = Critter.GetDirectionTo(x, y);
                }
                Critter.Speed = configuration.NominalSpeed;
            }
        }

        public override void NotifyCloseToPoop(int x, int y)
        {
            if (NotifyPoo == 1)
            {
                //if poo is nearby and is a certain distance close then it will change the critters direction to the right as well as tracking where the poop is
                if (!Critter.IsTerrainBlockingRouteTo(x, y) && lastPoopX != x && lastPoopY != y && DrawLine(x, y) < 50)
                {
                    ChangeDirection(90);
                }
                Critter.Speed = configuration.NominalSpeed;
                lastPoopX = x;
                lastPoopY = y;
            }
        }
    }
}
