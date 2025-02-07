using System;

public class Player
{
    public int level;
    public string name;
    public string jobClass;
    public float attack;
    public float depense;
    public int health;
    public int gold;

    // 생성자
    public Player(string name)
    {
        Name = name;
        Level = 1;
        MaxHealth = 100;
        Health = MaxHealth;
        AttackPower = 10;
        Defense = 5;
    }


}
