using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextRPG
{
    public class SkillConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ISkill).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is not ISkill skill) return;

            writer.WriteStartObject();
            writer.WritePropertyName("$type");
            writer.WriteValue(skill.GetType().FullName);

            writer.WritePropertyName("name");
            writer.WriteValue(skill.name);

            writer.WritePropertyName("mpValue");
            writer.WriteValue(skill.mpValue);

            writer.WritePropertyName("damageValue");
            writer.WriteValue(skill.damageValue);

            writer.WritePropertyName("description");
            writer.WriteValue(skill.description);

            writer.WritePropertyName("isRandom");
            writer.WriteValue(skill.isRandom);

            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader.Read();
            string? type = null;
            ISkill? skill = null;

            while (reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string? propertyName = reader.Value as string;
                    reader.Read();

                    if (propertyName == "$type")
                    {
                        type = reader.Value as string;
                    }
                    else if (propertyName == "name" && skill != null)
                    {
                        skill.name = reader.Value as string;
                    }
                    else if (propertyName == "mpValue" && skill != null)
                    {
                        skill.mpValue = reader.Value is int ? (int)reader.Value : Convert.ToInt32(reader.Value);
                    }
                    else if (propertyName == "damageValue" && skill != null)
                    {
                        skill.damageValue = reader.Value is float ? (float)reader.Value : Convert.ToSingle(reader.Value);
                    }
                    else if (propertyName == "description" && skill != null)
                    {
                        skill.description = reader.Value as string;
                    }
                    else if (propertyName == "isRandom" && skill != null)
                    {
                        skill.isRandom = (bool)reader.Value;
                    }
                }
                reader.Read();
            }

            if (type != null)
            {
                // type에 따라 skill 객체 생성
                if (type == typeof(AlphaStrike).FullName)
                {
                    skill = new AlphaStrike();
                }
                else if (type == typeof(DoubleStrike).FullName)
                {
                    skill = new DoubleStrike();
                }
            }

            return skill;
        }
    }

    //  스킬 기본 구성 인터페이스
    public interface ISkill
    {
        public string type { get; set; }
        public string name { get; set; }
        public int mpValue { get; set; }
        public float damageValue { get; set; }
        public string description { get; set; }
        public bool isRandom { get; set; }

        //  스킬의 정보를 보여주는 함수
        public void SkillInfo();

        //  스킬 사용 함수
        public void Use(Player player, Battle battle, int input);
    }


    //  알파 스트라이크 스킬 클래스
    public class AlphaStrike : ISkill
    {
        public string type { get; set; }
        public string name { get; set; }
        public int mpValue { get; set; }
        public float damageValue { get; set; }
        public string description { get; set; }
        public bool isRandom { get; set; }

        public AlphaStrike()
        {
            type = "AlphaStrike";
            name = "알파 스트라이크";
            mpValue = 10;
            damageValue = 2;
            description = $"공격력 * {damageValue} 로 하나의 적을 공격합니다.";
            isRandom = false;
        }

        public void SkillInfo()
        {
            Console.WriteLine($"{name} - MP {mpValue}");
            Console.WriteLine(description);
        }

        public void Use(Player player, Battle battle, int input)
        {
            player.mana -= mpValue;

            int skillDamage = (int)Math.Ceiling(player.attack * 2.0f);

            Console.Clear();
            ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
            Console.WriteLine();
            Console.WriteLine($"{player.character}의 공격! - 알파 스트라이크!!");
            Console.WriteLine();
            Console.Write($"{battle.battleMonsters[input - 1].Name} 을(를) 맞췄습니다. [데미지 : {skillDamage}]");
            Console.WriteLine();
            Console.WriteLine($"LV.{battle.battleMonsters[input - 1].Level} {battle.battleMonsters[input - 1].Name}");
            Console.Write($"HP {battle.battleMonsters[input - 1].Hp} -> ");

            battle.battleMonsters[input - 1].Hp -= skillDamage;

            if (battle.battleMonsters[input - 1].Hp <= 0)
            {
                battle.battleMonsters[input - 1].IsDead = true;
            }

            if (battle.battleMonsters[input - 1].IsDead)
            {
                ConsoleUtility.ColorWriteLine("Dead", ConsoleColor.DarkGray);
            }
            else
            {
                ConsoleUtility.ColorWriteLine($"{battle.battleMonsters[input - 1].Hp} ", ConsoleColor.DarkRed);
            }

            int deadCount = 0;

            for (int j = 0; j < battle.battleMonsters.Count; j++)
            {
                if (battle.battleMonsters[j].IsDead == true)
                {
                    deadCount++;
                }
            }

            if (deadCount == battle.battleMonsters.Count)
            {
                battle.isClear = 1;
            }
        }
    }


    //  더블 스크라이크 스킬 클래스
    public class DoubleStrike : ISkill
    {
        public string type { get; set; }
        public string name { get; set; }
        public int mpValue { get; set; }
        public float damageValue { get; set; }
        public string description { get; set; }
        public bool isRandom { get; set; }

        public DoubleStrike()
        {
            type = "DoubleStrike";
            name = "더블 스트라이크";
            mpValue = 15;
            damageValue = 1.5f;
            description = $"공격력 * {damageValue} 로 2명의 적을 랜덤으로 공격합니다.";
            isRandom = true;
        }

        public void SkillInfo()
        {
            Console.WriteLine($"{name} - MP {mpValue}");
            Console.WriteLine(description);
        }

        public void Use(Player player, Battle battle, int input)
        {
            player.mana -= mpValue;

            Random rand = new Random();

            int randomIndex = 0;
            int overlap = -999;

            for (int i = 0; i < 2; i++)
            {
                int leaveCount = 0;

                for (int j = 0; j < battle.battleMonsters.Count; j++)
                {
                    if (battle.battleMonsters[j].IsDead == false)
                    {
                        leaveCount++;
                    }
                }

                do
                {
                    randomIndex = rand.Next(0, battle.battleMonsters.Count);
                }
                while (battle.battleMonsters[randomIndex].IsDead == true || randomIndex == overlap);

                if (i == 0)
                {
                    overlap = randomIndex;
                }

                int skillDamage = (int)Math.Ceiling(player.attack * 1.5f);

                Console.Clear();
                ConsoleUtility.ColorWriteLine("Battle!!", ConsoleColor.Cyan);
                Console.WriteLine();
                Console.WriteLine($"{player.character}의 공격! - 더블 스트라이크!!");
                Console.WriteLine();
                Console.Write($"{battle.battleMonsters[randomIndex].Name} 을(를) 맞췄습니다. [데미지 : {skillDamage}]");
                Console.WriteLine();
                Console.WriteLine($"LV.{battle.battleMonsters[randomIndex].Level} {battle.battleMonsters[randomIndex].Name}");
                Console.Write($"HP {battle.battleMonsters[randomIndex].Hp} -> ");

                battle.battleMonsters[randomIndex].Hp -= skillDamage;

                if (battle.battleMonsters[randomIndex].Hp <= 0)
                {
                    battle.battleMonsters[randomIndex].IsDead = true;
                }

                if (battle.battleMonsters[randomIndex].IsDead)
                {
                    ConsoleUtility.ColorWriteLine("Dead", ConsoleColor.DarkGray);
                }
                else
                {
                    ConsoleUtility.ColorWriteLine($"{battle.battleMonsters[randomIndex].Hp} ", ConsoleColor.DarkRed);
                }

                int deadCount = 0;

                for (int j = 0; j < battle.battleMonsters.Count; j++)
                {
                    if (battle.battleMonsters[j].IsDead == true)
                    {
                        deadCount++;
                    }
                }

                Thread.Sleep(2000);

                if (deadCount == battle.battleMonsters.Count)
                {
                    battle.isClear = 1;
                    break;
                }

                if (leaveCount == 1)
                {
                    break;
                }
            }


        }
    }
}
