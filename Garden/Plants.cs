using System;
using System.Collections.Generic;

namespace Program
{
    public class Program2
    {
        public static readonly Program.Plant[] plants = new Program.Plant[]
        {
           new Program.Plant ("морковь", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.winter, Program.Seasons.spring}, -4, 2, 2 ) ,
           new Program.Plant ("кукуруза", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.spring, Program.Seasons.autumn}, 2, -4, 2 ) ,
           new Program.Plant ("картофель", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.winter, Program.Seasons.spring}, 2, 2, -4 ) ,
           new Program.Plant ("томат", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.spring, Program.Seasons.summer}, -2, -2, 4 ) ,
           new Program.Plant ("спаржа", new List<Program.Seasons> { Program.Seasons.winter, Program.Seasons.spring}, 2, -4, 2 ) ,
           new Program.Plant ("баклажан", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.spring}, 2, 2, -4 ) ,
           new Program.Plant ("тыква", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.winter}, -4, 2, 2 ) ,
           new Program.Plant ("арбуз", new List<Program.Seasons> { Program.Seasons.spring, Program.Seasons.autumn}, 4, -2, -2 ) ,
           new Program.Plant ("питайа", new List<Program.Seasons> { Program.Seasons.spring, Program.Seasons.autumn}, 4, 4, -8 ) ,
           new Program.Plant ("дуриан", new List<Program.Seasons> { Program.Seasons.spring}, 4, -8, 4 ) ,
           new Program.Plant ("чеснок", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.winter, Program.Seasons.spring, Program.Seasons.summer}, 4, -8, 4 ) ,
           new Program.Plant ("лук", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.spring, Program.Seasons.summer}, -8, 4, 4 ) ,
           new Program.Plant ("перец", new List<Program.Seasons> { Program.Seasons.autumn, Program.Seasons.summer}, 4, 4, -8 ) ,
           new Program.Plant ("гранат", new List<Program.Seasons> { Program.Seasons.spring, Program.Seasons.summer}, -8, 4, 4 ) ,
        };

        public static void printAll()
        {
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Растение\t|Формула роста\t|Компост\t|Навоз\t\t|Любимые сезоны\t|");
            Console.WriteLine("---------------------------------------------------------------------------------");
            
            foreach (Program.Plant plant in plants)
            {
                if (plant.getName().Length >= 8)
                {
                    Console.Write($"{plant.getName()}\t|\t{plant.getGrFormula()}\t|\t{plant.getCompost()}\t|\t{plant.getManure()}\t|");

                    foreach(Program.Seasons season in plant.getSeasons())
                    {
                        switch(season)
                        {
                            case Program.Seasons.autumn:
                                Console.Write(" О ");
                                break;
                            case Program.Seasons.winter:
                                Console.Write(" З ");
                                break;
                            case Program.Seasons.spring:
                                Console.Write(" В ");
                                break;
                            case Program.Seasons.summer:
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

                    foreach (Program.Seasons season in plant.getSeasons())
                    {
                        switch (season)
                        {
                            case Program.Seasons.autumn:
                                Console.Write(" О ");
                                break;
                            case Program.Seasons.winter:
                                Console.Write(" З ");
                                break;
                            case Program.Seasons.spring:
                                Console.Write(" В ");
                                break;
                            case Program.Seasons.summer:
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