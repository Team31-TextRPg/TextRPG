using System;

public class Player
{
    public int Level;
    public string Name;
    public string JobClass;
    public float Attack;
    public float Depense;
    public int Health;
    public int Gold;

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
