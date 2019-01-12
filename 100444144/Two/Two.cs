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
    public class Two : CritterBrains.CritterBrain
    {
        //If pathfinder can see the exit then it will head straight there, if not path finder will go unitl it hits terrain
        // when it hits terrain it will add a small angle to its current until it is following the terrain
        TwoConfiguration configuration = null;
        string ConfigurationFileName = "Two.txt";
        //X and Y co-ords of the exit
        int centreDestinationX;
        int centreDestinationY;
        //allows for random movement when hitting a wall
        Random random = new Random();
        int timePast;
        DateTime lastTerrainCollision;
        public Two() : base("Pathfinder", "Ryan Skull")
        {
            LoadConfiguration();
        }

        public void LoadConfiguration()
        {
            TextReader reader = null;
            try
            {
                configuration = new TwoConfiguration();
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
            configuration = new TwoConfiguration();
        }

        public override Form Form
        {
            get
            {
                return new TwoConfigForm(this);
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

        //Easy way to change the critter direction based on its current direction
        //Y centre = 230. X centre =300
        private void ChangeDirection(int angle)
        {
            Critter.Direction += angle;
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
            angle = Math.Tanh((x1 - x2) / (y1 - y2));
            //MessageBox.Show(angle.ToString());
            return angle;
        }

        //Method to return the amount of time that has passed since an event
        private int TimePast(DateTime lastResponse)
        {
            timePast = DateTime.Now.Subtract(lastResponse).Seconds;
            return timePast;
        }

        public override void Birth()
        {
            Critter.Speed = configuration.NominalSpeed;
            Critter.Direction = 90;
            FindLevelExit();
        }
        public override void Think()
        {
            IWorldObject[] scan = new IWorldObject[10];
            scan = Critter.Scan();
            Critter.Speed = configuration.NominalSpeed;
            for (int i = 0; i < scan.Length; i++)
            {
                int itemX = scan[i].X;
                int itemY = scan[i].Y;
                string itemType = scan[i].Type;
                if (itemType == "Poop")
                {
                    //Causes the critter to stay still for two seconds if poop is within a certain distance
                    //Will be recalled if poop has not despawned
                    DateTime lastResponse = DateTime.Now;
                    while (TimePast(lastResponse) < 2)
                    {
                        if (DrawLine(itemX, itemY) <= 50 && !Critter.IsTerrainBlockingRouteTo(itemX ,itemY))
                        {
                            ChangeDirection(30);
                            Critter.Speed = 0;
                        }
                    }
                    Critter.Speed = configuration.NominalSpeed;
                    //rescan at end of loop as other things may have changed since
                    scan = Critter.Scan();
                }
                else if(itemType == "Food")
                {
                    DateTime lastResponse = DateTime.Now;
                    while (TimePast(lastResponse) < 2)
                    {
                        if (Critter.Energy <= 20)
                        {
                            if (!Critter.IsTerrainBlockingRouteTo(itemX, itemY))
                            {
                                Critter.Direction = Critter.GetDirectionTo(itemX, itemY);
                            }
                        }
                        //rescan at end of loop as other things may have changed since
                        scan = Critter.Scan();
                    }
                }
                else if(itemType == "Critter")
                {
                    if(DrawLine(itemX, itemY) < 25)
                    {
                        ChangeDirection(15);
                        Critter.Speed = configuration.NominalSpeed;
                    }
                }
            }
            if (!Critter.IsTerrainBlockingRouteTo(centreDestinationX, centreDestinationY))
            {
                HeadToExit();
            }
            else
            {
                //If the critter hasnt collided with the terrain for awhile it will head back right in hopes to find the end
                if (TimePast(lastTerrainCollision) > 4)
                {
                    Critter.Direction = 90;
                }
            }
        }
        //Slowly turns the critter until he is facing at the same angle as the terrain caught on
        public override void NotifyBlockedByTerrain()
        {
            ChangeDirection(10);
            Critter.Speed = configuration.NominalSpeed;
            lastTerrainCollision = DateTime.Now;
        }
    }
}
