﻿using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PetStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Панкреазим",
                        Description = "Полиферментный препарат. Панкреатические ферменты (липаза, амилаза и протеаза), входящие в его состав, способствуют расщеплению жиров, белков и углеводов, что способствует их полному всасыванию в тонком кишечнике. При заболеваниях поджелудочной железы препарат компенсирует недостаточность ее внешнесекреторной функции и способствует улучшению процесса пищеварения. Таблетки покрыты защитной оболочкой, нерастворимой в кислой среде желудка, защищающей пищеварительные ферменты от разрушения в желудке.",
                        Category = "Ферменты",
                        Price = 100,
                        ImageId=""
                    },
                    new Product
                    {
                        Name = "Мезим",
                        Description = "Действующим веществом лекарственного средства Мезим форте является порошок из поджелудочных желез (панкреатин) млекопитающих, обычно свиней, который, кроме экскреторных панкреатических ферментов (липазы, альфа-амилазы, трипсина и химотрипсина), содержит и другие ферменты. Панкреатин также содержит другие сопутствующие вещества, не имеющие ферментативной активности.",
                        Category = "Ферменты",
                        Price = 45,
                        ImageId = ""
                    },
                    new Product
                    {
                        Name = "Но-шпа",
                        Description = "Дротаверин — производное изохинолина, который оказывает спазмолитическое действие на гладкие мышцы путем угнетения действия фермента ФДЭ ІV, вызывая увеличение концентрации цАМФ и благодаря инактивации легкой цепочки киназы миозина (MLCK) приводит к расслаблению гладких мышц.",
                        Category = "Лекарства от спазмов",
                        Price = 55,
                        ImageId = ""
                    },
                    new Product
                    {
                        Name = "Спазмалгон",
                        Description = "Спазмалгон — комбинированный препарат с анальгезирующим, спазмолитическим (папавериноподобным), холинолитическим (атропиноподобным) и некоторым противовоспалительным действием.",
                        Category = "Лекарства от спазмов",
                        Price = 25,
                        ImageId = ""
                    },
                    new Product
                    {
                        Name = "Валериана",
                        Description = "Препарат снижает возбудимость ЦНС. Действие обусловлено содержанием эфирного масла, большая часть которого — сложный эфир спирта борнеола и изовалериановой кислоты. Седативные свойства имеют также валепотриаты и алкалоиды — валерин и хотинин. Седативное действие проявляется медленно, но достаточно стабильно. Валериановая кислота и валепотриаты оказывают слабое спазмолитическое действие.",
                        Category = "Успокоительные",
                        Price = 72,
                        ImageId = ""
                    },
                    new Product
                    {
                        Name = "Анальгин",
                        Description = "Анальгин (метамизола натриевая соль) проявляет аналгетическое, жаропонижающее и противовоспалительное действие. Аналгетический эффект обусловлен ингибицией циклооксигеназы и блокированием синтеза простагландинов из арахидоновой кислоты, которые принимают участие в формировании реакции на болевые раздражители (брадикинины, простагландины); замедлением проведения экстра- и проприоцептивных болевых импульсов в центральной нервной системе, повышением порога возбудимости таламических центров болевой чувствительности и ослаблением реакции структур головного мозга, отвечающих за восприятие боли.",
                        Category = "Обезбаливающие",
                        Price = 72,
                        ImageId = ""
                    },
                    new Product
                    {
                        Name = "Нурофен",
                        Description = "Ибупрофен — это НПВП, производное пропионовой кислоты, который продемонстрировал свою эффективность путем угнетения синтеза простагландинов. У человека ибупрофен уменьшает выраженность боли при воспалении, отеки и лихорадку. Кроме того, ибупрофен обратимо угнетает агрегацию тромбоцитов.",
                        Category = "Обезбаливающие",
                        Price = 55,
                        ImageId = ""
                    },
                    new Product
                    {
                        Name = "Стрепсилс",
                        Description = "Препарат обладает антисептическими свойствами. Активен в отношении широкого спектра грамположительных и грамотрицательных микроорганизмов; оказывает противогрибковое действие. Эффективность препарата обусловлена наличием двух антибактериальных компонентов широкого спектра действия, которые облегчают боль в горле и уменьшают проявления воспаления. Амилметакрезол разрушает структуру белков бактерий, что обеспечивает бактерицидное действие. 2,4-дихлорбензиловый спирт проявляет бактериостатический эффект за счет обезвоживания бактериальной клетки.",
                        Category = "Обезбаливающие",
                        Price = 70,
                        ImageId = ""
                    },
                    new Product
                    {
                        Name = "Цитрамон",
                        Description = "Цитрамон — комбинированный препарат, который оказывает анальгезирующее, жаропонижающее и противовоспалительное действие. Компоненты, входящие в его состав, усиливают эффекты друг друга.",
                        Category = "Обезбаливающие",
                        Price = 45,
                        ImageId = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
