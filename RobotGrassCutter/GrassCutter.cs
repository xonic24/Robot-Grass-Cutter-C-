public class GrassCutter
{
    public Position CurrentPos { get; set; }
    public Garden GrassToCut { get; set; }

    public GrassCutter(Garden garden)
    {
        CurrentPos = garden.getStartPosition();
        GrassToCut = garden;
    }

    // go North method
    public bool goNorth()
    {
        // if there is no fence or object in North
        if(this.GrassToCut.GardenObj[CurrentPos.posX - 1][CurrentPos.posY] != Garden.fence && this.GrassToCut.GardenObj[CurrentPos.posX - 1][CurrentPos.posY] != Garden.obj)
        {
            // set the position in North to current
            this.GrassToCut.GardenObj[CurrentPos.posX - 1][CurrentPos.posY] = Garden.currentPosition;

            // set the previous position mowed
            this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY] = Garden.mowed;

            // adjusting the X coordinate of the position
            CurrentPos.posX = CurrentPos.posX - 1;

            // print the garden
            GrassToCut.printGarden();
            return true;
        }
        // if there is a fence or an object in North
        return false;
    }

    public bool goSouth()
    {
        if (this.GrassToCut.GardenObj[CurrentPos.posX + 1][CurrentPos.posY] != Garden.fence && this.GrassToCut.GardenObj[CurrentPos.posX + 1][CurrentPos.posY] != Garden.obj)
        {
            this.GrassToCut.GardenObj[CurrentPos.posX + 1][CurrentPos.posY] = Garden.currentPosition;
            this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY] = Garden.mowed;
            CurrentPos.posX = CurrentPos.posX + 1;
            GrassToCut.printGarden();
            return true;
        }
        return false;
    }
    public bool goEast()
    {
        if (this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY + 1] != Garden.fence && this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY + 1] != Garden.obj)
        {
            this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY + 1] = Garden.currentPosition;
            this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY] = Garden.mowed;
            CurrentPos.posY = CurrentPos.posY + 1;
            GrassToCut.printGarden();
            return true;
        }
        return false;
    }
    public bool goWest()
    {
        if (this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY - 1] != Garden.fence && this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY - 1] != Garden.obj)
        {
            this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY - 1] = Garden.currentPosition;
            this.GrassToCut.GardenObj[CurrentPos.posX][CurrentPos.posY] = Garden.mowed;
            CurrentPos.posY = CurrentPos.posY - 1;
            GrassToCut.printGarden();
            return true;
        }
        return false;
    }   

    // cut method (starts the cutting process of the grass cutter)
    public void cut()
    {
        // stores positions for later usage
        List<Position> storedPositions = new List<Position>();

        // looks for nearest grass to cut in the garden
        Position nearestGrass = findNearestGrass();

        // while the result is not -1, -1
        while (nearestGrass.posX > 0 && nearestGrass.posY > 0)
        {
            // ads the current position to storedPositions
            storedPositions.Add(new Position(CurrentPos.posX, CurrentPos.posY));
            
            // if the nearest grass' X coordinate is lower
            if(CurrentPos.posX > nearestGrass.posX)
            {
                // go North
                goNorth();
            }

            // if the nearest grass' X coordinate is higher
            if(CurrentPos.posX < nearestGrass.posX)
            {
                // go South
                goSouth();
            }

            // if the nearest grass' Y coordinate is higher
            if(CurrentPos.posY < nearestGrass.posY)
            {
                // go East
                goEast();
            }

            // if the nearest grass' Y coordinate is lower
            if(CurrentPos.posY > nearestGrass.posY)
            {
                // go West
                goWest(); 
            }

            // based on movements there might be a closer grass, so it starts looking for grass again
            nearestGrass = findNearestGrass();

            // if there is at least 2 storedPositions
            if(storedPositions.Count > 2)
            {
                // if the last 2 positions are the same (robot got stuck)
                if (storedPositions.ElementAt(storedPositions.Count - 2).posX == storedPositions.ElementAt(storedPositions.Count - 1).posX && storedPositions.ElementAt(storedPositions.Count - 2).posY == storedPositions.ElementAt(storedPositions.Count - 1).posY)
                {
                    // make a clockwise movement alongside the fence/objects
                    goForward(0, nearestGrass);
                }
            }

            // after the goForward method returned, checks and updates the nearestGrass
            nearestGrass = findNearestGrass();

            // if the nearestGrass is -1, -1
            if(nearestGrass.posX < 0 && nearestGrass.posY < 0)
            {
                // stops the algorithm
                return;
            }
            Thread.Sleep(1000);
        }
    }

    // clockwise movement alongside walls/objects
    public void goForward(int turnRight, Position nearestGrass)
    {
        //turn right counter
        switch (turnRight)
        {
            // case 0 turn right, move north
            case 0:
                {
                    // if moving north is possible
                    if (goNorth())
                    {
                        Thread.Sleep(1000);

                        // if we are on the nearest grass exit from goForward
                        if ((CurrentPos.posX == nearestGrass.posX && CurrentPos.posY == nearestGrass.posY))
                        {
                            return;
                        }

                        // based on movement there might be a closer grass, so checking again
                        nearestGrass = findNearestGrass();

                        // if there is no grass anymore exit from goForward
                        if (nearestGrass.posX < 0 && nearestGrass.posY < 0)
                        {
                            return;
                        }

                        // if we did not find the nearest grass try to go north again
                        goForward(turnRight, nearestGrass);
                        break;
                    }
                    else
                    {
                        // if we couldn't go to north, turn right and go forward
                        goForward(turnRight + 1, nearestGrass);
                        break;
                    }
                }


            case 1:
                // if turn right is 1 go east
                {
                    // if moving east is possible
                    if (goEast())
                    {
                        Thread.Sleep(1000);
                        // if we are on the nearest grass exit from goForward
                        if ((CurrentPos.posX == nearestGrass.posX && CurrentPos.posY == nearestGrass.posY))
                        {
                            return;
                        }
                        // based on movement there might be a closer grass, so checking again
                        nearestGrass = findNearestGrass();
                        // if there is no grass anymore exit from goForward
                        if (nearestGrass.posX < 0 && nearestGrass.posY < 0)
                        {
                            return;
                        }
                        // if we did not find the nearest grass try to move north again
                        goForward(turnRight - 1, nearestGrass);
                        break;
                    }
                    else
                    {
                        // if we couldn't go to east, turn right again and go forward
                        goForward(turnRight + 1, nearestGrass);
                        break;
                    }

                }
            case 2:
                // if turn right is 2 go south
                {
                    // if moving south is possible
                    if (goSouth())
                    {
                        Thread.Sleep(1000);
                        // if we are on the nearest grass exit from goForward
                        if ((CurrentPos.posX == nearestGrass.posX && CurrentPos.posY == nearestGrass.posY))
                        {
                            return;
                        }
                        // based on movement there might be a closer grass, so checking again
                        nearestGrass = findNearestGrass();
                        // if there is no grass anymore exit from goForward
                        if (nearestGrass.posX < 0 && nearestGrass.posY < 0)
                        {
                            return;
                        }
                        // if we did not find the nearest grass try to move east again
                        goForward(turnRight - 1, nearestGrass);
                        break;
                    }
                    else
                    {
                        // if we couldn't go to south, turn right again and go forward
                        goForward(turnRight + 1, nearestGrass);
                        break;
                    }

                }
            case 3:
                // if turn right is 3 go west
                {
                    // if moving west possible
                    if (goWest())
                    {
                        Thread.Sleep(1000);
                        // if we are on the nearest grass exit from goForward
                        if ((CurrentPos.posX == nearestGrass.posX && CurrentPos.posY == nearestGrass.posY))
                        {
                            return;
                        }
                        // based on movement there might be a closer grass, so checking again
                        nearestGrass = findNearestGrass();
                        // if there is no grass anymore exit from moveForward
                        if (nearestGrass.posX < 0 && nearestGrass.posY < 0)
                        {
                            return;
                        }
                        // if we did not find the nearest grass try to move south again
                        goForward(turnRight - 1, nearestGrass);
                        break;
                    }
                    else
                    {
                        // if we couldn't go to south, turn right again and go forward
                        goForward(turnRight + 1, nearestGrass);
                        break;
                    }
                }
            case 4:
                // if turn right is 4 go north
                {
                    // if moving north is possible
                    if (goNorth())
                    {
                        Thread.Sleep(1000);
                        // if we are on the nearest grass exit from goForward
                        if ((CurrentPos.posX == nearestGrass.posX && CurrentPos.posY == nearestGrass.posY))
                        {
                            return;
                        }
                        // based on movement there might be a closer grass, so checking again
                        nearestGrass = findNearestGrass();
                        // if there is no grass anymore exit from moveForward
                        if (nearestGrass.posX < 0 && nearestGrass.posY < 0)
                        {
                            return;
                        }
                        // if we did not find the nearest grass try to move west again
                        goForward(turnRight - 1, nearestGrass);
                        break;
                    }
                    else
                    {
                        // if we couldn't go to north, turn right again and go forward
                        goForward(1, nearestGrass);
                        break;
                    }
                }
            default:
                break;
        }
    }

    // searching for the nearest grass
    public Position findNearestGrass()
    {
        // set a big distance for initialized value
        double nearestDistance = 8000;
        // set a wrong value for x and y
        int nearestX = -1;
        int nearestY = -1;
        // searching in the garden, not counting the first and last row (fence)
        for (int i = 1; i < this.GrassToCut.GardenObj.Length - 1; i++)
        {
            // searching in the room, not counting the first and last column (fence)
            for (int j = 1; j < this.GrassToCut.GardenObj[i].Length - 1; j++)
            {
                // if the current position has grass
                if (this.GrassToCut.GardenObj[i][j] == Garden.grass)
                {
                    // calculate the distance from current position
                    double distance = Math.Sqrt((CurrentPos.posX - i) * (CurrentPos.posX - i) + (CurrentPos.posY - j) * (CurrentPos.posY - j));
                    // if this distance is smaller then the previously assigned nearestDistance
                    if (distance < nearestDistance)
                    {
                        // set the nearestDistance to the new distance, and set the coordinates for it
                        nearestDistance = distance;
                        nearestX = i;
                        nearestY = j;
                    }
                }
            }
        }
        // return the position for the nearest coordinates
        return new Position(nearestX, nearestY);
    }
}
