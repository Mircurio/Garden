using System;
using System.Collections.Generic;
using System.IO;

namespace Program
{
    public class DifferentSeasonsException : Exception
    {
        public DifferentSeasonsException(string message) : base(message) { }
        public DifferentSeasonsException() : this("Не все растения могут расти в текущий сезон!") { }
    }
    public class CannotFindSoilException : Exception
    {
        public CannotFindSoilException() : base("Не удаётся найти грядку с таким номером!")
        {

        }
    }

    public class ThisSoilIsAlreadyExistsException : Exception
    {
        public ThisSoilIsAlreadyExistsException() : base("Грядка с таким номером уже существует!")
        {

        }
    }
    public enum Seasons
    {
        autumn,
        winter,
        spring,
        summer,
    }
    public class Soil
    {
        private int grFormula;
        private int compost;
        private int manure;
        private int number;

        public Soil(int number, int grFormula, int compost, int manure)
        {
            this.grFormula = grFormula;
            this.compost = compost;
            this.manure = manure;
            this.number = number;
        }

        public int getNumber()
        {
            return number;
        }

        public int getGrFormula()
        {
            return grFormula;
        }

        public int getCompost()
        {
            return compost;
        }

        public int getManure()
        {
            return manure;
        }

        public void setGrFormula(int grFormula)
        {
            this.grFormula = grFormula;
        }

        public void setCompost(int compost)
        {
            this.compost = compost;
        }

        public void setManure(int manure)
        {
            this.manure = manure;
        }
    }

    public static class Garden
    {
        private static List<Soil> soils;

        public static Soil findSoil(int number)
        {
            foreach (Soil soil in soils)
            {
                if (soil.getNumber() == number)
                    return soil;
            }

            throw new CannotFindSoilException();
        }

        static Garden()
        {
            soils = new List<Soil>();
        }
        public static void addSoil(int number, int grFormula = 0, int compost = 0, int manure = 0)
        {
            foreach (Soil soil in soils)
            {
                if (soil.getNumber() == number)
                {
                    throw new ThisSoilIsAlreadyExistsException();
                }
            }

            soils.Add(new Soil(number, grFormula, compost, manure));
        }

        public static void delSoil(int number)
        {
            bool deleted = false;

            for (int i = 0; i < soils.Count; i++)
            {
                if (soils[i].getNumber() == number)
                {
                    soils.RemoveAt(i);
                    deleted = true;
                }
            }

            if (!deleted)
            {
                throw new CannotFindSoilException();
            }
        }

        public static List<Soil> getSoils()
        {
            return soils;
        }

        public static void setSoils(List<Soil> soils)
        {
            Garden.soils.Clear();
            Garden.soils = new List<Soil>(soils);
        }
    }

    public class Plant
    {
        private string name;
        private List<Seasons> seasons;

        private int grFormula;
        private int compost;
        private int manure;

        public Plant(string name, List<Seasons> seasons, int grFormula, int compost, int manure)
        {
            this.name = name;
            this.seasons = seasons;
            this.grFormula = grFormula;
            this.compost = compost;
            this.manure = manure;
        }

        public string getName()
        {
            return name;
        }

        public List<Seasons> getSeasons()
        {
            return seasons;
        }

        public int getGrFormula()
        {
            return grFormula;
        }

        public int getCompost()
        {
            return compost;
        }

        public int getManure()
        {
            return manure;
        }
    }
    public static class Planner
    {
        private static List<Plant> plants;
        private static Seasons currentSeason;

        private const int maxFertilizers = 100;
        private const int minFertilizers = 0;
        private static void permissibleValue(ref int value)
        {
            if (value > maxFertilizers)
            {
                value = maxFertilizers;
            }
            else if (value < minFertilizers)
            {
                value = minFertilizers;
            }
        }

        private static bool canGrowInThisSeason(Plant plant)
        {
            if (plant.getSeasons() == null)
            {
                throw new ArgumentException("Сезоны для данного растения не заданы!");
            }

            if (plant.getSeasons().Contains(currentSeason))
            {
                return true;
            }

            return false;
        }

        // Возвращает список растений, которые не могут расти в заданный сезон.
        private static List<Plant> checkAllPlants()
        {
            List<Plant> result = new List<Plant>();

            foreach (Plant plant in plants)
            {
                if (!canGrowInThisSeason(plant) && !result.Contains(plant))
                {
                    result.Add(plant);
                }
            }

            return result;
        }
        static Planner()
        {
            plants = new List<Plant>();
        }

        public static void clear()
        {
            plants.Clear();
        }

        public static void addPlant(Plant plant)
        {
            plants.Add(plant);
        }

        public static void setCurrentSeason(Seasons season)
        {
            currentSeason = season;
        }
        public static void removePlant(Plant plant)
        {
            plants.Remove(plant);
        }
        public static void calculate(Soil soil)
        {
            List<Plant> plantsWithOtherSeasons = checkAllPlants();

            // Пока не сохраняем удобрения.
            int grFormula = 0;
            int compost = 0;
            int manure = 0;

            foreach (Plant plant in plants)
            {
                grFormula += plant.getGrFormula();
                compost += plant.getCompost();
                manure += plant.getManure();
            }

            // Умножаем на 4, т.к. 4 стадии роста.
            grFormula *= 4;
            compost *= 4;
            manure *= 4;

            grFormula += soil.getGrFormula();
            compost += soil.getCompost();
            manure += soil.getManure();

            Menu.displayResults(grFormula, compost, manure, plantsWithOtherSeasons);

            // Не может быть больше 100 и меньше 0.
            permissibleValue(ref grFormula);
            permissibleValue(ref compost);
            permissibleValue(ref manure);

            if (Menu.willSave())
            {
                soil.setGrFormula(grFormula);
                soil.setCompost(compost);
                soil.setManure(manure);
            }
        }

        public static int getCount()
        {
            return plants.Count();
        }
    }

    public static class Save
    {
        private const string path = "Garden.txt";
        public static void save()
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                List<Soil> soils = Garden.getSoils();

                foreach (Soil soil in soils)
                {
                    file.WriteLine(soil.getNumber());
                    file.WriteLine(soil.getGrFormula());
                    file.WriteLine(soil.getCompost());
                    file.WriteLine(soil.getManure());
                }

                Console.WriteLine("Данные успешно сохранены!");
                Thread.Sleep(2000);
            }
        }
    }

    public static class Load
    {
        private const string path = "Garden.txt";

        public static void load()
        {
            try
            {
                using (StreamReader file = new StreamReader(path))
                {
                    List<Soil> soils = new List<Soil>();

                    while (!file.EndOfStream)
                    {
                        int number = Convert.ToInt32(file.ReadLine());
                        int grFormula = Convert.ToInt32(file.ReadLine());
                        int compost = Convert.ToInt32(file.ReadLine());
                        int manure = Convert.ToInt32(file.ReadLine());

                        soils.Add(new Soil(number, grFormula, compost, manure));
                    }

                    Garden.setSoils(soils);
                }
            }
            catch (FileNotFoundException ex)
            {

            }
        }
    }
    public static class Menu
    {
        private static void errorDetected(string message, int sleepingTime = 2000, bool willBeep = true)
        {
            Console.WriteLine(message);
            Console.Beep();
            Thread.Sleep(sleepingTime);
        }

        // Вызывается только из displayResults.
        private static void printSoilInfo(int amountOfFertilizer, string typeOfFertilizer)
        {
            // Если < 0, то удобрение в почве не хватает.

            if (amountOfFertilizer > 100)
            {
                Console.WriteLine($"В почве избыток {typeOfFertilizer}: {amountOfFertilizer - 100}.");
            }
            else if (amountOfFertilizer < 0)
            {
                Console.WriteLine($"В почве недостаток {typeOfFertilizer}: {-amountOfFertilizer}.");
            }
            else if (amountOfFertilizer > 0)
            {
                Console.WriteLine($"В почве содержится {typeOfFertilizer}: {amountOfFertilizer}.");
            }

        }

        // Вызывается только из Planner. Нужен для отображения результатов.
        public static void displayResults(int grFormula, int compost, int manure, List<Plant> plantsWithOtherSeasons)
        {
            if (plantsWithOtherSeasons.Count != 0)
            {
                Console.WriteLine("Предупреждение! Следующие растения не могум расти в текущий сезон:");

                Console.ForegroundColor = ConsoleColor.Red;

                // Первое растение с большой буквы!
                char[] tmp = plantsWithOtherSeasons[0].getName().ToCharArray();
                tmp[0] = char.ToUpper(tmp[0]);
                string firstPlant = new string(tmp);

                Console.Write(firstPlant);
                if (plantsWithOtherSeasons.Count > 1)
                {
                    Console.Write(", ");
                }

                for (int i = 1; i < plantsWithOtherSeasons.Count - 1; i++)
                {
                    Console.Write(plantsWithOtherSeasons[i].getName() + ", ");
                }


                if (plantsWithOtherSeasons.Count > 1)
                {
                    Console.Write(plantsWithOtherSeasons[plantsWithOtherSeasons.Count - 1].getName());
                }

                Console.WriteLine();
                Console.ResetColor();
            }

            printSoilInfo(grFormula, "формулы роста");
            printSoilInfo(compost, "компостных веществ");
            printSoilInfo(manure, "навозных веществ");
        }

        // Будет ли сохранён результат планировки?
        public static bool willSave()
        {

            string choice;
            Console.Write("Записать данные на грядку? (да/Нет): ");
            choice = Console.ReadLine();

            if (choice == "Да" || choice == "да")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Данная функция запускается только после выбора грядки.
        // Далее идёт расчёт удобрений.
        private static void calculateMenu(Soil soil)
        {
            const string stop = ".";

            
               
                string input = "";

                Console.Clear();

                while (true)
                {
                    try
                    {
                        Console.Write("Введите текущий сезон (О/З/В/Л): ");
                        input = Console.ReadLine();

                        if (input != "О" && input != "З" && input != "В" && input != "Л")
                        {
                            throw new FormatException("Некорректный ввод!");
                        }

                        Seasons currentSeason = Seasons.autumn;
                        switch (input)
                        {
                            case "О":
                                currentSeason = Seasons.autumn;
                                break;
                            case "З":
                                currentSeason = Seasons.winter;
                                break;
                            case "В":
                                currentSeason = Seasons.spring;
                                break;
                            case "Л":
                                currentSeason = Seasons.summer;
                                break;
                        }

                        Planner.setCurrentSeason(currentSeason);
                        break; // Выход из бесконечного цикла.
                    }
                    catch (FormatException ex)
                    {
                        errorDetected(ex.Message + "\nПопробуйте ещё раз.");
                    }

                }
                input = "";

                Console.WriteLine($"Введите {stop} чтобы закончить ввод растений.");
                Console.WriteLine();

                for (int i = 0; i < Plants.plants.Count(); i++)
                {
                    Console.WriteLine($"{i + 1} - {Plants.plants[i].getName()}");
                }

                while (input != stop && Planner.getCount() < 9)
                {
                    Console.Write("Выберете растение: ");
                    input = Console.ReadLine();

                    try
                    {
                        if (input != stop)
                        {
                            if (Convert.ToInt32(input) >= 1 && Convert.ToInt32(input) <= Plants.plants.Count())
                            {
                                Planner.addPlant(Plants.plants[Convert.ToInt32(input) - 1]);
                            }
                            else
                            {
                                throw new FormatException("Некорректный ввод!");
                            }
                        }
                    }
                    catch (FormatException ex)
                    {
                        errorDetected("Некорректный ввод!\nПопробуйте ещё раз!");
                    }
                }

                Planner.calculate(soil);
                Planner.clear();
            
        }
        

        public static void menu()
        {
            bool finish = false;

            do
            {

                Console.WriteLine("1 - выбрать грядку");
                Console.WriteLine("2 - дабавить грядку");
                Console.WriteLine("3 - удалить грядку");
                Console.WriteLine("4 - вывести список растений");
                Console.WriteLine("5 - сохранить данные в файл.");
                Console.WriteLine("6 - выйти из программы");

                Console.Write("Выберете действие: ");

                int choice;

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice > 6 || choice < 0)
                    {
                        throw new FormatException();
                    }
                }
                catch(FormatException ex)
                {
                    errorDetected("Некорректный ввод!\nПопробуйте еще раз!\n");
                    choice = -1;
                }

                switch (choice)
                {

                    case 1:
                        {
                            bool success = false;

                            do
                            {
                                try
                                {
                                    int number; // Номер грядки. Вводится с клавиатуры.

                                    // Ввод с клавиатуры.
                                    Console.Write("Введите номер грядки: ");
                                    number = Convert.ToInt32(Console.ReadLine());

                                    calculateMenu(Garden.findSoil(number));
                                    success = true;
                                }
                                catch (CannotFindSoilException ex)
                                {
                                    errorDetected(ex.Message, 3000);
                                    Console.Clear();
                                    success = true; // Так как нам нужно выйти из цикла.
                                }
                                catch (FormatException ex)
                                {
                                    errorDetected("Некорректный ввод!\nПопробуйте ещё раз.");
                                }

                            } while (!success);

                            break;
                        }
                    case 2:
                        {
                            bool success = false;

                            do
                            {
                                try
                                {
                                    int number; // Номер грядки. Вводится с клавиатуры.

                                    Console.Write("Придумайте номер грядки: ");
                                    number = Convert.ToInt32(Console.ReadLine());

                                    string fertilizersСhoice;
                                    Console.Write("Ввести начальное количество удобрений?(да/Нет): ");
                                    fertilizersСhoice = Console.ReadLine();

                                    if (fertilizersСhoice == "Да" || fertilizersСhoice == "да")
                                    {
                                        int grFormula, compost, manure;
                                        // Вводятся с клавиатуры.

                                        Console.Write("Введите количество формулы роста: ");
                                        grFormula = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Введите количество компостных веществ: ");
                                        compost = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Введите количество навозных веществ: ");
                                        manure = Convert.ToInt32(Console.ReadLine());

                                        Garden.addSoil(number, grFormula, compost, manure);
                                        Console.WriteLine("Грядка успешно добавлена!");

                                        success = true;

                                        Thread.Sleep(2000);
                                        Console.Clear();

                                    }
                                    else if (fertilizersСhoice == null || fertilizersСhoice == "" || fertilizersСhoice == "Нет" || fertilizersСhoice == "нет")
                                    {

                                        Garden.addSoil(number);
                                        Console.WriteLine("Грядка успешно добавлена!");

                                        success = true;

                                        Thread.Sleep(2000);
                                        Console.Clear();
                                    }
                                }
                                catch (ThisSoilIsAlreadyExistsException ex)
                                {
                                    errorDetected(ex.Message + "\nПопробуйте ещё раз.", 5000);
                                    Console.Clear();
                                }

                            } while (!success);
                            break;
                        }

                    case 3:
                        {
                            bool success = false;

                            do
                            {
                                try
                                {
                                    int number;

                                    Console.Write("Введите номер грядки: ");
                                    number = Convert.ToInt32(Console.ReadLine());

                                    Garden.delSoil(number);

                                    Console.WriteLine("Грядка успешно удалена!");
                                    success = true;

                                    Thread.Sleep(2000);
                                    Console.Clear();
                                }
                                catch (CannotFindSoilException ex)
                                {
                                    errorDetected(ex.Message + "\nПопробуйте ещё раз.", 5000);
                                    Console.Clear();
                                }
                                catch (FormatException ex)
                                {
                                    errorDetected("Некорректный ввод!\nПопробуйте ещё раз.", 5000);
                                    Console.Clear();
                                }

                            } while (!success);
                            break;
                        }

                    case 4:
                        {
                            Plants.printAll();
                            break;
                        }

                    case 5:
                        {
                            Save.save();

                            break;
                        }
                    case 6:
                        {
                            finish = true;
                            break;
                        }
                }

            } while (!finish);
        }
    }
    public class main
    {
        const string version = "3.1";
        public static void Main()
        {
            Console.WriteLine($"Добро пожаловать в Огородик {version}!");
            Console.WriteLine();

            Plant plant = new Plant("carrot", new List<Seasons> { Seasons.autumn, Seasons.winter, Seasons.spring }, -4, 2, 2);

            Load.load();

            Menu.menu();
        }
    }
}