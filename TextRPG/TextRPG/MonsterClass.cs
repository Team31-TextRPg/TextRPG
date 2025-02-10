using System;
namespace TextRPG;

public class Monster : ICloneable
{
    //프로퍼티는 {get; set;}은 읽기+쓰기, {get;}은 읽기
    public int Level { get; set; }
    public string Name { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Atk { get; set; }
    public bool IsDead { get; set; }

    public Monster(int level, string name, int maxHp, int atk)
    {
        Level = level;
        Name = name;
        Hp = maxHp;
        MaxHp = maxHp;
        Atk = atk;
    }

    public object Clone()
    {
        Monster newMonster = new Monster(0,"",0,0);

        newMonster.Level = this.Level;
        newMonster.Name = this.Name;
        newMonster.Hp = this.Hp;
        newMonster.MaxHp = this.MaxHp;
        newMonster.Atk = this.Atk;
        newMonster.IsDead = this.IsDead;

        return newMonster;
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
