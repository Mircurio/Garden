using System;
using System.Collections.Generic;

namespace Program
{
    public class Fertilizer
    {
        private string name;
        private int grFormula;
        private int compost;
        private int manure;

        public Fertilizer(string name, int grFormula, int compost, int manure)
        {
            this.name = name;
            this.grFormula = grFormula;
            this.compost = compost;
            this.manure = manure;
        }

        public string getName()
        {
            return name;
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
    public static class Fertilizers
    {
        private static readonly Fertilizer[] fertilizers = new Fertilizer[]
        {
            new Fertilizer("навоз",                 0, 0, 8),
            new Fertilizer("гниль",                 0, 8, 0),
            new Fertilizer("тухлое яйцо",           0, 16, 0),
            new Fertilizer("гуано",                 0, 0, 16),
            new Fertilizer("слизь гломмера",        8, 8, 8),
            new Fertilizer("ведро с удобрениями",   0, 0, 16),
            new Fertilizer("компостная обёртка",    24, 32, 24),
            new Fertilizer("компост",               0, 24, 0),
            new Fertilizer("формула роста",         32, 0, 0),
            new Fertilizer("сгнившая рыбина",       16, 0, 0),
            new Fertilizer("сгнивший кусочек рыбы", 8, 0, 0),
            new Fertilizer("древесный джем",        8, 32, 8),
        };

        public static Fertilizer GetFertilizer(string name)
        {
            for (int i = 0; i < fertilizers.Length; i++)
            {
                if (fertilizers[i].getName() == name)
                {
                    return fertilizers[i];
                }
            }

            throw new CannotFindThisFertilizerException(); 
        }
    }
}
