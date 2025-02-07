using System;
namespace TextRPG;

public class Monster
{
    //프로퍼티는 {get; set;}은 읽기+쓰기, {get;}은 읽기
    public int Level { get; }
    public string Name { get; }
    public int Hp { get; set; }
    public int MaxHp { get; }
    public int Atk { get; }
    public bool IsDead { get; set; }

    public Monster(int level, string name, int maxHp, int atk)
    {
        Level = level;
        Name = name;
        Hp = maxHp;
        MaxHp = maxHp;
        Atk = atk;
    }
    public string MonsterDisplay()
    {
        string str = $"Lv. {Level} | {Name} | {DeadString()}";
        return str;
    }

    public string DeadString()
    {
        string str = IsDead ? "Dead" : $"HP {Hp}";
        return str;
    }
}
