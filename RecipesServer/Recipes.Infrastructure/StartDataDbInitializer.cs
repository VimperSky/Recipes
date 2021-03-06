using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure
{
    public class StartDataDbInitializer
    {
        private const string BaseTagIconsPath = "default_images/tags/";
        private const string BaseImagesPath = "default_images/";

        private static readonly Tag[] Tags =
        {
            Tag.Create("клубника"),
            Tag.Create("десерты"),
            Tag.Create("сливки"),
            Tag.Create("вторые блюда"),
            Tag.Create("соевый соус"),
            Tag.Create("завтрак"),
            Tag.Create("блины"),
            Tag.Create("вишня"),
            Tag.Create("мороженое"),
            Tag.Create("сладости"),
            Tag.Create("бисквит"),
            Tag.Create("мясо", TagLevel.Suggested),
            Tag.Create("деликатесы", TagLevel.Suggested),
            Tag.Create("пироги", TagLevel.Suggested),
            Tag.Create("рыба", TagLevel.Suggested),
            Tag.Create("пост", TagLevel.Suggested),
            Tag.Create("пасха2021", TagLevel.Suggested),
            Tag.Create("Простые блюда", TagLevel.Featured, BaseTagIconsPath + "simple.svg",
                "Время приготвления таких блюд не более 1 часа"),

            Tag.Create("Детское", TagLevel.Featured, BaseTagIconsPath + "child.svg",
                "Самые полезные блюда которые можно детям любого возраста"),

            Tag.Create("От шеф-поваров", TagLevel.Featured, BaseTagIconsPath + "chef.svg",
                "Требуют умения, времени и терпения, зато как в ресторане"),

            Tag.Create("На праздник", TagLevel.Featured, BaseTagIconsPath + "holiday.svg",
                "Чем удивить гостей, чтобы все были сыты за праздничным столом")
        };

        private static readonly Dictionary<string, Tag> TagsDict = Tags.ToDictionary(x => x.Value, x => x);

        public static readonly Recipe[] Recipes =
        {
            new()
            {
                Name = "Клубичная Панна-Котта",
                Description = "Десерт, который невероятно легко и быстро готовится. " +
                              "Советую подавать его порционно в красивых бокалах, " +
                              "украсив взбитыми сливками, свежими ягодами и мятой.",
                Portions = 5,
                ImagePath = $"{BaseImagesPath}/r1.png",
                CookingTimeMin = 35,
                Steps = new[]
                {
                    "Приготовим панна котту: Зальем желатин молоком и поставим на 30 минут для набухания. " +
                    "В сливки добавим сахар и ванильный сахар. Доводим до кипения (не кипятим!).",

                    "Добавим в сливки набухший в молоке желатин. Перемешаем до полного растворения. " +
                    "Огонь отключаем. Охладим до комнатной температуры.",

                    "Разольем охлажденные сливки по креманкам и поставим в холодильник до полного застывания." +
                    " Это около 3-5 часов.",

                    "Приготовим клубничное желе: Клубнику помоем, очистим от плодоножек. Добавим сахар." +
                    " Взбиваем клубнику с помощью блендера в пюре.",

                    "Добавим в миску с желатином 2ст.ложки холодной воды и сок лимона. Перемешаем и поставим на 30 минут для набухания. " +
                    "Доведем клубничное пюре до кипения. Добавим набухший желатин, перемешаем до полного растворения. " +
                    "Огонь отключаем. Охладим до комнатной температуры.",

                    "Сверху на застывшие сливки добавим охлажденное клубничное желе. Поставим в холодильник " +
                    "до полного застывания клубничного желе. Готовую панна коту подаем с фруктами."
                },
                Tags = new[] { GetTag("клубника"), GetTag("десерты"), GetTag("сливки") }.ToList(),
                Ingredients = new List<RecipeIngredientsBlock>
                {
                    new()
                    {
                        Header = "Для панна котты",
                        Value = "Сливки-20-30% - 500мл.\n" +
                                "Молоко - 100мл.\n" +
                                "Желатин - 2ч.л.\n" +
                                "Сахар - 3ст.л.\n" +
                                "Ванильный сахар - 2 ч.л."
                    },
                    new()
                    {
                        Header = "Для клубничного желе",
                        Value = "Сливки-20-30% - 500мл.\n" +
                                "Молоко - 100мл.\n" +
                                "Желатин - 2ч.л.\n" +
                                "Сахар - 3ст.л.\n" +
                                "Ванильный сахар - 2 ч.л."
                    }
                }
            },
            new()
            {
                Name = "Мясные фрикадельки",
                Description =
                    "Мясные фрикадельки в томатном соусе - несложное и вкусное блюдо, которым можно порадовать своих близких.",
                Portions = 4,
                ImagePath = $"{BaseImagesPath}/r2.png",
                CookingTimeMin = 90,
                Steps = new[]
                {
                    "Мясо (без пленок и жира) пропустить через мясорубку, прибавить хлеб (без корочки), " +
                    "размоченный в воде и затем отжатый, и пропустить вторично через мясорубку.",

                    "Затем прибавить взбитый белок, соль, чайную ложку холодной воды, 1/2 чайной ложки масла и" +
                    " вымесить фарш, чтобы масса была нежной, ровной.",

                    "Приготовленную массу выложить на доску, смоченную водой, разделить на фрикадельки. " +
                    "Фрикадельки скатать шариками (руки надо смазать яичным белком) и опустить в соленый кипяток; " +
                    "через 5 минут фрикадельки вынуть на решето, дать им обсохнуть, потом опустить в суп," +
                    " прокипятить и подавать к столу."
                },
                Tags = new[] { GetTag("вторые блюда"), GetTag("мясо"), GetTag("соевый соус") }.ToList(),
                Ingredients = new List<RecipeIngredientsBlock>
                {
                    new()
                    {
                        Header = "",
                        Value = "Мясо 50г.\n" +
                                "Белый хлеб 15г.\n" +
                                "Яичный белок 1 шт..\n" +
                                "Соль по вкусу.\n"
                    }
                }
            },
            new()
            {
                Name = "Панкейки",
                Description = "Панкейки: меньше, чем блины, но больше, чем оладьи. Основное отличие — в тесте, " +
                              "оно должно быть воздушным, чтобы панкейки не растекались по сковородке...",
                Portions = 3,
                ImagePath = $"{BaseImagesPath}/r3.png",
                CookingTimeMin = 40,
                Steps = new[]
                {
                    "В ёмкость, в которой будем делать тесто для панкейков, выбиваем яйцо, добавляем сахар " +
                    "(30 г или 2 ст. ложки) и соль (1/2 ч. ложки). Если хотите добавить ванильный сахар, добавьте его сейчас.",

                    "Перемешиваем миксером или просто венчиком до однородного состояния и полного растворения сахара и соли.",

                    "Добавляем молоко (210 г), перемешиваем.",

                    "Наливаем растительное масло (25 г (~2 ст. ложки) и снова хорошенько перемешиваем."
                },
                Tags = new[] { GetTag("завтрак"), GetTag("десерты"), GetTag("блины") }.ToList(),
                Ingredients = new List<RecipeIngredientsBlock>
                {
                    new()
                    {
                        Header = "",
                        Value = "молоко 210 г (мл)\nяйцо 1 шт.\nмука 200 г\nразрыхлитель 5 г (1 ч. ложка)\n" +
                                "масло растительное 25 г (2 ст. ложки)\nсахар 30 г (2 ст. ложки)\nсоль 1/2 ч. ложки"
                    }
                }
            },
            new()
            {
                Name = "Полезное мороженое без сахара",
                Description = "Йогуртовое мороженое сочетает в себе нежный вкус и низкую калорийность, " +
                              "что будет особенно актуально для сладкоежек, соблюдающих диету.",
                Portions = 2,
                ImagePath = $"{BaseImagesPath}/r4.png",
                CookingTimeMin = 35,
                Steps = new[]
                {
                    "Очищаем 2 банана, режем на кружочки или кубики, выкладываем на любую плоскую посудину," +
                    " накрываем и отправляем в морозилку на пару часов, или пока не решите делать мороженое. ",

                    "Достаем из морозилки замороженные бананы, скидываем в блендер, добавляем какао 1 ст л. (с горкой), " +
                    "если хотите более шоколадный вкус. ",

                    "Блендерим до однородности, но не долго. Чтобы смесь не нагрелась.",

                    "Готовое мороженое выкладываем в чашку, миску..."
                },
                Tags = new[] { GetTag("десерты"), GetTag("вишня"), GetTag("мороженое") }.ToList(),
                Ingredients = new List<RecipeIngredientsBlock>
                {
                    new()
                    {
                        Header = "",
                        Value = "2 банана\n1 ст. л. какао\nХолод"
                    }
                }
            },
            new()
            {
                Name = "Бисквит классический",
                Description = "Приготовить нежный и воздушный бисквит совсем несложно! Бисквит по этому рецепту" +
                              " я готовлю уже достаточно давно и он меня не подводил",
                Portions = 6,
                ImagePath = $"{BaseImagesPath}/r5.jpg",
                CookingTimeMin = 400,
                Steps = new[]
                {
                    "Итак, для приготовления классического бисквита возьмем яйца (крупного размера), сахар, муку и ванильный сахар.",

                    "Очень аккуратно, стараясь, чтобы в белки не попал желток разделяем яйца. " +
                    "Белки отставляем в сторону, а к желткам добавляем половину всего сахара " +
                    "(около 75 г, если добавите немного больше или меньше, то ничего страшного) и весь ванильный сахар.",

                    "Миксером на максимальной скорости взбиваем желтки в течение 5-7 минут до получения очень плотной и светлой массы.",

                    "Венчики миксера тщательно моем и насухо вытираем и начинаем взбивать белки. " +
                    "Сперва взбиваем их самостоятельно до мягких пиков. Взбивать белки начинаем на " +
                    "минимальной скорости миксера и по мере того, как белки начнут увеличиваться в объеме - увеличиваем скорость.",


                    "Не прекращая взбивать всыпаем в белки небольшими порциями оставшийся сахарный песок.",

                    "И взбиваем их до твёрдых пиков. Готовность белков проверяем очень просто - " +
                    "наклоняем миску с белками в сторону и, если белковая масса не стремится убежать, значит всё готово.."
                },
                Tags = new[] { GetTag("бисквит"), GetTag("сладости") }.ToList(),
                Ingredients = new List<RecipeIngredientsBlock>
                {
                    new()
                    {
                        Header = "",
                        Value = "Куриные яйца 4 шт. \nМука 100 гр.\nСахар 150гр.\nВанильный сахар 1 чайн. л."
                    }
                }
            }
        };

        public static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<RecipesDbContext>();
                FillDbWithData(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<StartDataDbInitializer>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }

        private static Tag GetTag(string value)
        {
            return TagsDict[value];
        }

        public static void FillDbWithData(RecipesDbContext dbContext)
        {
            if (dbContext.Recipes.Any()) return;

            // Транзакция и SaveChanges() на каждой итерации нужны, чтобы все рецепты были добавлены в строгом порядке
            using var transaction = dbContext.Database.BeginTransaction();
            dbContext.Tags.AddRange(Tags);
            dbContext.SaveChanges();

            foreach (var recipe in Recipes)
            {
                dbContext.Recipes.Add(recipe);
                dbContext.SaveChanges();
            }

            transaction.Commit();
        }
    }
}