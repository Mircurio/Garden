using System;
using System.Collections.Generic;

namespace Program
{
    public class Plants
    {
        public static readonly Plant[] plants = new Plant[]
        {
           new Plant ("морковь", new List<Seasons> { Seasons.autumn, Seasons.winter, Seasons.spring }, -4, 2, 2 ) ,
           new Plant ("кукуруза", new List<Seasons> { Seasons.autumn, Seasons.spring, Seasons.autumn }, 2, -4, 2 ) ,
           new Plant ("картофель", new List<Seasons> { Seasons.autumn, Seasons.winter, Seasons.spring }, 2, 2, -4 ) ,
           new Plant ("томат", new List<Seasons> { Seasons.autumn, Seasons.spring, Seasons.summer }, -2, -2, 4 ) ,
           new Plant ("спаржа", new List<Seasons> { Seasons.winter, Seasons.spring }, 2, -4, 2 ) ,
           new Plant ("баклажан", new List<Seasons> { Seasons.autumn, Seasons.spring }, 2, 2, -4 ) ,
           new Plant ("тыква", new List<Seasons> { Seasons.autumn, Seasons.winter }, -4, 2, 2 ) ,
           new Plant ("арбуз", new List<Seasons> { Seasons.spring, Seasons.summer }, 4, -2, -2 ) ,
           new Plant ("питайа", new List<Seasons> { Seasons.spring, Seasons.summer }, 4, 4, -8 ) ,
           new Plant ("дуриан", new List<Seasons> { Seasons.spring}, 4, -8, 4 ) ,
           new Plant ("чеснок", new List<Seasons> { Seasons.autumn, Seasons.winter, Seasons.spring, Seasons.summer }, 4, -8, 4 ) ,
           new Plant ("лук", new List<Seasons> { Seasons.autumn, Seasons.spring, Seasons.summer }, -8, 4, 4 ) ,
           new Plant ("перец", new List<Seasons> { Seasons.autumn, Seasons.summer }, 4, 4, -8 ) ,
           new Plant ("гранат", new List<Seasons> { Seasons.spring, Seasons.summer }, -8, 4, 4 ) ,
        };

        public static void printAll()
        {
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Растение\t|Формула роста\t|Компост\t|Навоз\t\t|Любимые сезоны\t|");
            Console.WriteLine("---------------------------------------------------------------------------------");

            foreach (Plant plant in plants)
            {
                if (plant.getName().Length >= 8)
                {
                    Console.Write($"{plant.getName()}\t|\t{plant.getGrFormula()}\t|\t{plant.getCompost()}\t|\t{plant.getManure()}\t|");

                    foreach (Seasons season in plant.getSeasons())
                    {
                        switch (season)
                        {
                            case Seasons.autumn:
                                Console.Write(" О ");
                                break;
                            case Seasons.winter:
                                Console.Write(" З ");
                                break;
                            case Seasons.spring:
                                Console.Write(" В ");
                                break;
                            case Seasons.summer:
                                Console.Write(" Л ");
                                break;
                        }
                    }

                    if (plant.getSeasons().Count <= 2)
                    {
                        Console.Write("\t");
                    }

                    Console.WriteLine("\t|");
                }
                else
                {
                    Console.Write($"{plant.getName()}\t\t|\t{plant.getGrFormula()}\t|\t{plant.getCompost()}\t|\t{plant.getManure()}\t|");

                    foreach (Seasons season in plant.getSeasons())
                    {
                        switch (season)
                        {
                            case Seasons.autumn:
                                Console.Write(" О ");
                                break;
                            case Seasons.winter:
                                Console.Write(" З ");
                                break;
                            case Seasons.spring:
                                Console.Write(" В ");
                                break;
                            case Seasons.summer:
                                Console.Write(" Л ");
                                break;
                        }
                    }

                    if (plant.getSeasons().Count <= 2)
                    {
                        Console.Write("\t");
                    }

                    Console.WriteLine("\t|");
                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------");
        }
    }

}