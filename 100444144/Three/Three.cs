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
    public class Three : CritterBrains.CritterBrain
    {
        //Three aims to see if a critter is nearby if so then head away from the other critter to hopefully have them chase it around.
        ThreeConfiguration configuration = null;
        string ConfigurationFileName = "Three.txt";
        //X and Y co-ords of the exit
        int centreDestinationX;
        int centreDestinationY;
        //allows for random movement when hitting a wall
        Random random = new Random();
        public Three() : base("The Charm", "Ryan Skull")
        {
            LoadConfiguration();
        }

        public void LoadConfiguration()
        {
            TextReader reader = null;
            try
            {
                configuration = new ThreeConfiguration();
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
                    if (key == "chaseSpeed")
                    {
                        if (int.TryParse(value, out int chaseSpeed))
                        {
                            configuration.NominalSpeed = chaseSpeed;
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
                if (reader != null)
                    reader.Close();
            }
        }

        public void SaveConfiguration()
        {
            TextWriter writer = null;
            try
            {
                //anytime the configuration is saved it will save the values for the attributes
                writer = new StreamWriter(ConfigurationFileName);
                writer.WriteLine("nominalSpeed=" + configuration.NominalSpeed);
                writer.WriteLine("chaseSpeed=" + configuration.NominalSpeed);
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
            configuration = new ThreeConfiguration();
        }

        public override Form Form
        {
            get
            {
                return new ThreeConfigForm(this);
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
        //The speed of the critter whilst being chased
        public int ChaseSpeed
        {
            get
            {
                return configuration.ChaseSpeed;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Speed must be greater than or equal to zero.");
                }
                configuration.ChaseSpeed = value;
            }
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
        //Easy way to change the critter direction based on its current direction
        private void ChangeDirection(int angle)
        {
            Critter.Direction += angle;
        }
        private double DrawLine(int x, int y)
        {
            double xDistance = Critter.X - x;
            double yDistance = Critter.Y - y;
            double totalDistance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
            return totalDistance;
        }
        //method to return the angle between two points
        //x1 and y1 are to be the critter
        private double GetAngle(int x1, int y1, int x2, int y2)
        {
            double angle;
            if(x1 == x2 || y1 == y2)
            {
                //if to avoid any attempts at dividing by zero
                return -1;
            }
            else
            {
                angle = (Math.Tanh((x1 - x2) / (y1 - y2))) * (180/Math.PI);
                if(angle<0)
                {
                    angle += 360;
                }
               // MessageBox.Show(angle.ToString());
                return angle;
            }
        }

        public override void Birth()
        {
            Critter.Speed = Speed;
            Critter.Direction = 180;
        }

        public override void Think()
        {
            IWorldObject[] scan = new IWorldObject[10];
            scan = Critter.Scan();
            //sets speed to nominal so if the critter is no longer within 40 range then it will calm back down
            Critter.Speed = configuration.NominalSpeed;
            for (int i = 0; i < scan.Length; i++)
            {
                int itemX = scan[i].X;
                int itemY = scan[i].Y;
                string itemType = scan[i].Type;
                if (itemType == "Critter")
                {
                    //if the other critter has an angle within 20 degrees of the charm then it will enter a being chased mode where it should circle as well as go at chase speed
                    if (DrawLine(itemX, itemY) < 50 && ((GetAngle(Critter.X, Critter.Y, itemX, itemY) >= Critter.Direction + 10) || (GetAngle(Critter.X, Critter.Y, itemX, itemY) <= Critter.Direction - 10)))
                    {
                        ChangeDirection(5);
                        Critter.Speed = configuration.ChaseSpeed;
                      //  MessageBox.Show(GetAngle(Critter.X, Critter.Y, itemX, itemY).ToString());
                    };
                }
            }
        }
        public override void NotifyCloseToCritter(OtherCritter otherCritter)
        {
            if (otherCritter.DirectionTo == Critter.Direction)
            {
                Critter.Speed = configuration.ChaseSpeed;
            }
            else
            {
                Critter.Speed = configuration.NominalSpeed;
            }
        }

        public override void NotifyBumpedCritter(OtherCritter other)
        {
            //If a critter does bump then run away
            Critter.Speed = configuration.NominalSpeed;
            ChangeDirection(180);
        }

        public override void NotifyBlockedByTerrain()
        {
            //Will bounce away in a random direction, possible the critter will try and go into the wall again again however this 
            //will be called until it is no longer directed in the way of the wall
            ChangeDirection(random.Next(0, 361));
            Critter.Speed = configuration.NominalSpeed;
        }
    }
}
