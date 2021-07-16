﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Recipes.Domain.Models;

namespace Recipes.Infrastructure
{
    public class DbInitializer
    {
        public static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<RecipesContext>();
                context.Database.Migrate();
                FillWithStartData(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<DbInitializer>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
        
        private static void FillWithStartData(RecipesContext context)
        {
            if (context.Recipes.Any())
            {
                return;   // DB has been seeded
            }

            var recipes = new Recipe[]
            {
                new()
                {
                    Name = "Клубичная Панна-Котта", 
                    Description = "Десерт, который невероятно легко и быстро готовится. " +
                                  "Советую подавать его порционно в красивых бокалах, " +
                                  "украсив взбитыми сливками, свежими ягодами и мятой.",
                    Portions = 5,
                    ImagePath = "r1.png",
                    CookingTimeMin = 35,
                    Steps = new [] 
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
                    IngredientBlocks = new List<RecipeIngredientBlock>
                    {
                        new()
                        {
                            Header = "Для панна котты", 
                            Text = "Сливки-20-30% - 500мл.\n" +
                                   "Молоко - 100мл.\n" +
                                   "Желатин - 2ч.л.\n" +
                                   "Сахар - 3ст.л.\n" +
                                   "Ванильный сахар - 2 ч.л.",
                        },
                        new()
                        {
                            Header = "Для клубничного желе", 
                            Text = "Сливки-20-30% - 500мл.\n" +
                                   "Молоко - 100мл.\n" +
                                   "Желатин - 2ч.л.\n" +
                                   "Сахар - 3ст.л.\n" +
                                   "Ванильный сахар - 2 ч.л.",
                        }
                    }
                },
                new()
                {
                    Name = "Мясные фрикадельки", 
                    Description = "Мясные фрикадельки в томатном соусе - несложное и вкусное блюдо, которым можно порадовать своих близких.",
                    Portions = 4,
                    ImagePath = "r2.png",
                    CookingTimeMin = 90,
                    Steps = new [] 
                    {
                        "МЯСНЫЕ ФРИКАДЕЛЬКИ. Приготовим панна котту: Зальем желатин молоком и поставим на 30 минут для набухания. " +
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
                    }
                },
                new()
                {
                    Name = "Панкейки", 
                    Description = "Панкейки: меньше, чем блины, но больше, чем оладьи. Основное отличие — в тесте, " +
                                  "оно должно быть воздушным, чтобы панкейки не растекались по сковородке...",
                    Portions = 3,
                    ImagePath = "r3.png",
                    CookingTimeMin = 40,
                    Steps = new [] 
                    {
                        "ПАНКЕЙКИ!! Приготовим панна котту: Зальем желатин молоком и поставим на 30 минут для набухания. " +
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
                    }
                },
                new()
                {
                    Name = "Полезное мороженое без сахара", 
                    Description = "Йогуртовое мороженое сочетает в себе нежный вкус и низкую калорийность, " +
                                  "что будет особенно актуально для сладкоежек, соблюдающих диету.",
                    Portions = 2,
                    ImagePath = "r3.png",
                    CookingTimeMin = 35,
                    Steps = new [] 
                    {
                        "МОРОЖЕНОЕ. Приготовим панна котту: Зальем желатин молоком и поставим на 30 минут для набухания. " +
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
                    }
                },
            };
            
            foreach (var recipe in recipes)
                context.Recipes.Add(recipe);
            
            context.SaveChanges();
        }
    }
}