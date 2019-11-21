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

            if (!context.Products.Any() && !context.StockItems.Any())
            {
                #region Pharmacy

                //Product p1 = new Product
                //{
                //    Name = "Панкреазим",
                //    Description = "Полиферментный препарат. Панкреатические ферменты (липаза, амилаза и протеаза), входящие в его состав, способствуют расщеплению жиров, белков и углеводов, что способствует их полному всасыванию в тонком кишечнике. При заболеваниях поджелудочной железы препарат компенсирует недостаточность ее внешнесекреторной функции и способствует улучшению процесса пищеварения. Таблетки покрыты защитной оболочкой, нерастворимой в кислой среде желудка, защищающей пищеварительные ферменты от разрушения в желудке.",
                //    Category = "Ферменты",
                //    Price = 100,
                //    ImageId = ""
                //};

                //Product p2 = new Product
                //{
                //    Name = "Но-шпа",
                //    Description = "Дротаверин — производное изохинолина, который оказывает спазмолитическое действие на гладкие мышцы путем угнетения действия фермента ФДЭ ІV, вызывая увеличение концентрации цАМФ и благодаря инактивации легкой цепочки киназы миозина (MLCK) приводит к расслаблению гладких мышц.",
                //    Category = "Лекарства от спазмов",
                //    Price = 55,
                //    ImageId = ""
                //};

                //Product p3 = new Product
                //{
                //    Name = "Мезим",
                //    Description = "Действующим веществом лекарственного средства Мезим форте является порошок из поджелудочных желез (панкреатин) млекопитающих, обычно свиней, который, кроме экскреторных панкреатических ферментов (липазы, альфа-амилазы, трипсина и химотрипсина), содержит и другие ферменты. Панкреатин также содержит другие сопутствующие вещества, не имеющие ферментативной активности.",
                //    Category = "Ферменты",
                //    Price = 45,
                //    ImageId = ""
                //};

                //Product p4 = new Product
                //{
                //    Name = "Спазмалгон",
                //    Description = "Спазмалгон — комбинированный препарат с анальгезирующим, спазмолитическим (папавериноподобным), холинолитическим (атропиноподобным) и некоторым противовоспалительным действием.",
                //    Category = "Лекарства от спазмов",
                //    Price = 25,
                //    ImageId = ""
                //};

                //Product p5 = new Product
                //{
                //    Name = "Валериана",
                //    Description = "Препарат снижает возбудимость ЦНС. Действие обусловлено содержанием эфирного масла, большая часть которого — сложный эфир спирта борнеола и изовалериановой кислоты. Седативные свойства имеют также валепотриаты и алкалоиды — валерин и хотинин. Седативное действие проявляется медленно, но достаточно стабильно. Валериановая кислота и валепотриаты оказывают слабое спазмолитическое действие.",
                //    Category = "Успокоительные",
                //    Price = 72,
                //    ImageId = ""
                //};

                //Product p6 = new Product
                //{
                //    Name = "Анальгин",
                //    Description = "Анальгин (метамизола натриевая соль) проявляет аналгетическое, жаропонижающее и противовоспалительное действие. Аналгетический эффект обусловлен ингибицией циклооксигеназы и блокированием синтеза простагландинов из арахидоновой кислоты, которые принимают участие в формировании реакции на болевые раздражители (брадикинины, простагландины); замедлением проведения экстра- и проприоцептивных болевых импульсов в центральной нервной системе, повышением порога возбудимости таламических центров болевой чувствительности и ослаблением реакции структур головного мозга, отвечающих за восприятие боли.",
                //    Category = "Обезбаливающие",
                //    Price = 72,
                //    ImageId = ""
                //};

                //Product p7 = new Product
                //{
                //    Name = "Нурофен",
                //    Description = "Ибупрофен — это НПВП, производное пропионовой кислоты, который продемонстрировал свою эффективность путем угнетения синтеза простагландинов. У человека ибупрофен уменьшает выраженность боли при воспалении, отеки и лихорадку. Кроме того, ибупрофен обратимо угнетает агрегацию тромбоцитов.",
                //    Category = "Обезбаливающие",
                //    Price = 55,
                //    ImageId = ""
                //};

                //Product p8 = new Product
                //{
                //    Name = "Стрепсилс",
                //    Description = "Препарат обладает антисептическими свойствами. Активен в отношении широкого спектра грамположительных и грамотрицательных микроорганизмов; оказывает противогрибковое действие. Эффективность препарата обусловлена наличием двух антибактериальных компонентов широкого спектра действия, которые облегчают боль в горле и уменьшают проявления воспаления. Амилметакрезол разрушает структуру белков бактерий, что обеспечивает бактерицидное действие. 2,4-дихлорбензиловый спирт проявляет бактериостатический эффект за счет обезвоживания бактериальной клетки.",
                //    Category = "Обезбаливающие",
                //    Price = 70,
                //    ImageId = ""
                //};

                //Product p9 = new Product
                //{
                //    Name = "Цитрамон",
                //    Description = "Цитрамон — комбинированный препарат, который оказывает анальгезирующее, жаропонижающее и противовоспалительное действие. Компоненты, входящие в его состав, усиливают эффекты друг друга.",
                //    Category = "Обезбаливающие",
                //    Price = 45,
                //    ImageId = ""
                //};

                #endregion

                #region PetStore

                Product p1 = new Product
                {
                    Name = "Ferplast CAGE REKORD 4 WHITE Клетка для канареек и маленьких экзотических птиц",
                    Description = "Среди многочисленных клеток для птиц, выпускаемых компанией Ferplast, клетка Record 4 является удобной клеткой, выдержанной в традиционном стиле. Record 4 - прямоугольная клетка средних размеров, идеально подходит для канареек и экзотических маленьких птиц. Имеет очень прочный пластиковый поддон с выдвижным подносом для сбора мусора, облегчающим процесс ежедневной уборки. Record 4 идет в комплекте с аксессуарами, среди которых: пластиковые жердочки с резиновым покрытием, поворотные кормушки Brava 2, прищепки для подкормки, игрушка-зеркало с маленьким колокольчиком.",
                    Category = "Клетки для птиц",
                    Price = 1970,
                    ImageId = ""
                };

                Product p2 = new Product
                {
                    Name = "Karlie-Flamingo Jambi КАРЛИ-ФЛАМИНГО ДЖАМБО клетка для птиц, канарейки, металл, белый",
                    Description = "Оптимальная клетка для канареек, волнистых попугаев Karlie-Flamingo (КАРЛИ-ФЛАМИНГО Джамбо) JAMBI с выдвижным пластиковым поддоном. Красивая и просторная клетка отличный выбор для канареек, волнистых попугаев и других. Имеет металлические прутья с белым покрытием, пластиковый выдвижной поддон для удобства очистки клетки, жердочки, кормушки. Клетка проста в применении, легко моется, не боится химических веществ.",
                    Category = "Клетки для птиц",
                    Price = 469,
                    ImageId = ""
                };

                Product p3 = new Product
                {
                    Name = "Подвесное зеркало для попугаев в пластиковой рамке с колокольчиком Karlie-Flamingo (КАРЛИ-ФЛАМИНГО) LANTERN WITH BELL разнообразит досуг Вашего любимца, поможет сохранить активность и общительность.",
                    Description = "Подвесное зеркало для попугаев в пластиковой рамке с колокольчиком Karlie-Flamingo (КАРЛИ-ФЛАМИНГО) LANTERN WITH BELL разнообразит досуг Вашего любимца, поможет сохранить активность и общительность.",
                    Category = "Игрушки для птиц",
                    Price = 78,
                    ImageId = ""
                };

                Product p4 = new Product
                {
                    Name = "Pet Pro СВЕТОФОР с колокольчиком игрушка для попугаев, акрил",
                    Description = "Светофор с колокольчиком, акриловая игрушка для попугаев. Очень яркий и красочный аксессуар птичьей клетки. Все материалы очень прочные и абсолютно безопасные для здоровья пернатых любимцев. Игрушка выполнена в виде светофора и ее детали не имеют острых углов. Металлический колокольчик будет сопровождать мелодичным звуком все контакты птицы с игрушкой, птица останется довольна. Игрушка подойдет для любой птичьей клетки, ее размер 23 см в длину.",
                    Category = "Игрушки для птиц",
                    Price = 83,
                    ImageId = ""
                };

                Product p5 = new Product
                {
                    Name = "Tetra Аквариум Silhouette LED 12L",
                    Description = "Элегантный, дизайнерский аквариум Tetra Silhouette LED станет прекрасным подарком близким или же украшением Вашей квартиры или кабинета. Выполнен аквариум из высококачественного стекла, корое характеризуется прочностью и надежностью. Аквариум Tetra Silhouette – личается оригинальным дизайном и станет изюминкой в Вашем интерьере. Светодиодное освещение создает зрелищные эффекты подводного мира, а встроенная система фильтрации обеспечивает кристально чистую воду, безопасную для рыб.",
                    Category = "Аквариумы",
                    Price = 3260,
                    ImageId = ""
                };

                Product p6 = new Product
                {
                    Name = "Edel Cat heart k Влажный корм для кошек с рыбой в деликатном томатном желе",
                    Description = "Edel Cat heart k с рыбой в деликатном томатном желе – влажный корм для кошек все пород. Полнорационный корм разработан с учётом всех особенностей организма взрослых кошек, поэтому продукт рекомендован всем питомцам, достигшим возраста 8 месяцев. Содержит комплекс витаминов и минералов для укрепления иммунитета, для красоты шерсти.",
                    Category = "Корма для кошек",
                    Price = 31,
                    ImageId = ""
                };

                Product p7 = new Product
                {
                    Name = "1st Choice (Фест Чойс) КОТЕНОК сухой супер премиум корм для котят , 10 кг.",
                    Description = "1st Choice «Здоровый старт» - идеальная формула для начального прикорма котенка с 2 месяцев, когда он нуждается в новых источниках питания вместо материнского молока. При этом нет необходимости разделять котенка с мамой. 1st Choice для котят содержит все необходимое не только для растущего организма, но и для беременных и кормящих кошек. Оптимальное питание для старта в здоровую жизнь!",
                    Category = "Корма для кошек",
                    Price = 2184,
                    ImageId = ""
                };

                Product p8 = new Product
                {
                    Name = "Arden Grange Adult Cat Fresh Salmon & Potato Корм беззерновой для взрослых кошек",
                    Description = "Arden Grange беззерновой для взрослых кошек, со свежим лососем и картофелем - это полноценное питание супер премиум класса для всех пород взрослых кошки с нормальным уровнем активности. Корм на основе ингридиентов наивысшего качества идеально подойдет для тех кошек, которым требуется дополнительный уход за кожей и шерстью. Корм содержит большое количество свежего лосося, который придает является источником легко усваивающихся протеинов и незаменимых натуральных жирных кислот.",
                    Category = "Корма для кошек",
                    Price = 1749,
                    ImageId = ""
                };

                Product p9 = new Product
                {
                    Name = "Edel Cat k 100g паштет кролик",
                    Description = "Полнорационный корм разработан с учётом всех особенностей организма взрослых кошек. 100% натуральный корм приготовлен по новейшей высокотехнологичной рецептуре. Не содержит консервантов, антиоксидантов, красителей, ароматизаторов, усилителей вкуса, сои.",
                    Category = "Корма для кошек",
                    Price = 20,
                    ImageId = ""
                };

                #endregion

                context.Products.AddRange(p1, p2, p3, p4, p5, p6, p7, p8, p9);
                context.SaveChanges();
                context.StockItems.AddRange(
                    new Stock { Product = p1, Quantity = 10 },
                    new Stock { Product = p2, Quantity = 5 },
                    new Stock { Product = p3, Quantity = 12 },
                    new Stock { Product = p4, Quantity = 3 },
                    new Stock { Product = p5, Quantity = 6 },
                    new Stock { Product = p6, Quantity = 7 },
                    new Stock { Product = p7, Quantity = 4 },
                    new Stock { Product = p8, Quantity = 2 },
                    new Stock { Product = p9, Quantity = 19 }
                    );
                context.SaveChanges();
            }
        }
    }
}
