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
    public class Four : CritterBrains.CritterBrain
    {
        /*The idea is to use a search algorithm to find the exit then follow the route through to the exit
         When spawned the critter will evaluate a sequence of moves as to which gets it closer to the end goal
         The playable area is represented as a pixel matrix grouped into ten by tens. Best loaded in with no other critters
         to avoid the path being blocked by themas it is purely navigational*/
        FourConfiguration configuration = null;
        string ConfigurationFileName = "Four.txt";
        //X and Y co-ords of the exit
        int centreDestinationX;
        int centreDestinationY;
        //allows for random movement when hitting a wall
        Random random = new Random();
        //array to represent play area, postion 19,23 should be x = 190, y =230
        int[,] array = new int[60, 46];
        //xlist and ylist will be where the critter should go
        int[] xList = new int[1000];
        int[] yList = new int[1000];
        int i = 0;
        DateTime lastCall = new DateTime();
        public Four() : base("A*", "Ryan Skull")
        {
            LoadConfiguration();
        }

        public void LoadConfiguration()
        {
            TextReader reader = null;
            try
            {
                configuration = new FourConfiguration();
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
            configuration = new FourConfiguration();
        }

        public override Form Form
        {
            get
            {
                return new FourConfigForm(this);
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

        //finds coords of the exit point
        private void FindLevelExit()
        {
            Rectangle destination = Critter.GetDestination();
            centreDestinationX = destination.X + destination.Width / 2;
            centreDestinationY = destination.Y + destination.Height / 2;
        }
        private double DrawLine(int x, int y)
        {
            double xDistance = Critter.X - x;
            double yDistance = Critter.Y - y;
            double totalDistance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
            return totalDistance;
        }
        //Y centre = 230. X centre =300
        private void InitialiseArray()
        {
            for(int i = 0; i<60; i++)
            {
                for(int j=0; j<46; j++)
                {
                    array[i,j] = 0;
                }
            }
        } 

        private double GetDistance(int x1, int x2, int y1, int y2)
        {
            double xDistance = x1 - x2;
            //MessageBox.Show(xDistance.ToString());
            double yDistance = y1 - y2;
           // MessageBox.Show(yDistance.ToString());
            double totalDistance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
           //  MessageBox.Show("Distance is: " + totalDistance.ToString());
            return totalDistance;
        }

        private void Search()
        {
            //left is 0, right is 1, up is 2, down is 3
            double[] direction = new double[4];
            double currentLowest = 100000;
            int i = 0;
            //allows me to mimic the critter movement by using its starting position then theoretically moving the critter to determine 
            // if paths are shorter. Sadly it doesnt allow me to mimic the check blocked by terrain function as I can only
            //check if there is terrain blocking from the critters real position
            int theoreticalX = Critter.X;
            int theoreticalY = Critter.Y;
            string directionToTake = null;
            //if critter can go in direction then set location as visited, also Check distance
            //from that position to exit. Whichever distance is shortest is next place in route.
            //Note which direction is shortest and add x and y value to xlist and ylist to later retrace these steps
            while (currentLowest > 50)
            {
                if (!Critter.IsTerrainBlockingRouteTo(theoreticalX - 10, theoreticalY))
                {
                    direction[0] = GetDistance(theoreticalX - 10, centreDestinationX, theoreticalY, centreDestinationY);
                    array[(theoreticalX - 10) / 10, theoreticalY / 10] = 1;
                    if (direction[0] < currentLowest)
                    {
                        directionToTake = "left";
                        currentLowest = direction[0];
                    }
                }
                if (!Critter.IsTerrainBlockingRouteTo(theoreticalX + 10, theoreticalY))
                {
                    direction[1] = GetDistance(theoreticalX + 10, centreDestinationX, theoreticalY, centreDestinationY);
                    array[(theoreticalX + 10) / 10, theoreticalY / 10] = 1;
                    if (direction[1] < currentLowest)
                    {
                        directionToTake = "right";
                        currentLowest = direction[1];
                    }
                }
                if (!Critter.IsTerrainBlockingRouteTo(theoreticalX, theoreticalY + 10))
                {
                    direction[2] = GetDistance(theoreticalX, centreDestinationX, theoreticalY +10, centreDestinationY);
                    array[theoreticalX / 10, (theoreticalY + 10) / 10] = 1;
                    if (direction[2] < currentLowest)
                    {
                        directionToTake = "down";
                        currentLowest = direction[2];
                    }
                }
                if (!Critter.IsTerrainBlockingRouteTo(theoreticalX, theoreticalY - 10))
                {
                    direction[3] = GetDistance(theoreticalX, centreDestinationX, theoreticalY - 10, centreDestinationY);
                    array[theoreticalX / 10, (theoreticalY - 10) / 10] = 1;
                    if (direction[3] < currentLowest)
                    {
                        directionToTake = "up";
                        currentLowest = direction[3];
                    }
                }
                if (directionToTake == "left")
                {
                    xList[i] = -10;
                    yList[i] = 0;
                    theoreticalX += -10;
                }
                else if (directionToTake == "right")
                {
                    xList[i] = 10;
                    yList[i] = 0;
                    theoreticalX += 10;
                }
                else if (directionToTake == "down")
                {
                    yList[i] = 10;
                    xList[i] = 0;
                    theoreticalY += 10;
                }
                else if (directionToTake == "up")
                {
                    yList[i] = -10;
                    xList[i] = 0;
                    theoreticalY += -10;
                }
                else
                {
                   // MessageBox.Show("I dont know what to do!");
                }
               // MessageBox.Show(directionToTake);
                i++;
            }
            //MessageBox.Show("Route Found!");
        }

        private void Route()
        {
            {
                //Sets a time so it does not update the new direction before it has reached the old one
                lastCall = DateTime.Now;
                Critter.Direction = Critter.GetDirectionTo(Critter.X + xList[i], Critter.Y + yList[i]);
               // MessageBox.Show(Critter.Direction.ToString());
                Critter.Speed = configuration.NominalSpeed;
                i++;
            }
        }

        //Method to return the amount of time that has past since an event
        private int TimePast(DateTime lastCall)
        {
            int timePast = DateTime.Now.Subtract(lastCall).Seconds;
            return timePast;
        }

        public override void Birth()
        {
       //     InitialiseArray();
            FindLevelExit();
            Search();
            // MessageBox.Show("My x" + Critter.X);
            // MessageBox.Show("My y" + Critter.Y);
            // MessageBox.Show("Exit x" + centreDestinationX);
            // MessageBox.Show("Exit y" + centreDestinationY);
            lastCall = DateTime.Now;
        }

        public override void Think()
        {
            if(TimePast(lastCall) > 0.25)
            {
                Route();
            }
            //The search and route should get the critter within a range of 50 to the exit, so once in range the critter should be able to see the exit and head there
            if(GetDistance(Critter.X, centreDestinationX, Critter.Y, centreDestinationY) <= 50)
            {
                Critter.Direction = Critter.GetDirectionTo(centreDestinationX, centreDestinationY);
            }
        }
    }
}
