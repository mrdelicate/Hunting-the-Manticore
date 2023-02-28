int ShowScoreboard(int round, int cityHP, int manticoreHP)
{
    int target;
    Console.WriteLine("-----------------------------------------------------------");
    Console.WriteLine($"STATUS: Round: {round} City: {cityHP}/15 Manticore: {manticoreHP}/10");
    Console.WriteLine(NextShot(round));
    Console.Write("Entered the desired cannon range: ");
    target = Convert.ToInt32(Console.ReadLine());
    return target;
}

string NextShot(int round)
{
    int currentRound = round;
    return $"The cannon is expected to deal {CannonDamage(currentRound)} damage this round.";
}

int SetManticoreDistance()
{
    while (true)
    {
        Console.WriteLine("Manticore Pilot, how far from the city do you want to station the Manticore?");
        Console.Write("The maximum range for the Manticore is 100. ");
        int distance = Convert.ToInt32(Console.ReadLine());
        if (distance <= 0 && distance > 100)
        {
            Console.WriteLine("Invalid range.");
            continue;
        }
        else
        {
            return distance;
        }
    }
}

int CannonDamage(int round)
{
    if (round % 3 == 0 && round % 5 == 0)
    {
        return 10;
    }
    if ((round % 3 != 0 && round % 5 == 0) || (round % 3 == 0 && round % 5 != 0))
    {
        return 3;
    }
    return 1;
}

void GameLoop()
{
    int round = 1;
    int cityHP = 15;
    int manticoreHP = 10;
    while (true)
    {
        Console.Clear();
        int manticoreDistance = SetManticoreDistance();
        Console.Clear();
        while (cityHP > 0 || manticoreHP > 0)
        {
            int targetDistance = ShowScoreboard(round, cityHP, manticoreHP);
            Console.WriteLine("-----------------------------------------------------------");
            if (manticoreDistance > targetDistance)
            {
                Console.WriteLine("That round FELL SHORT of the target!");
                cityHP = 15 - round;
                round++;
            }
            if (manticoreDistance < targetDistance)
            {
                Console.WriteLine("That round OVERSHOT of the target!");
                cityHP = 15 - round;
                round++;
            }
            if (manticoreDistance == targetDistance)
            {
                Console.WriteLine("That round was a DIRECT HIT!");
                manticoreHP -= CannonDamage(round);
                cityHP = 15 - round;
                round++;
            }
            if (manticoreHP <= 0)
            {
                Console.WriteLine("Congratulations!  The Manticore is destroyed and the city saved!");
                Console.ReadKey();
                GameLoop();
            }
            if (cityHP == 0)
            {
                Console.WriteLine("The city is lost...");
                Console.ReadKey();
                GameLoop();
            }
        }
    }
}

GameLoop();