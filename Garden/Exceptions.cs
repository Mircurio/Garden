using System;
using System.Collections.Generic;

namespace Program
{
    public class DifferentSeasonsException : Exception
    {
        public DifferentSeasonsException() : base("Не все растения могут расти в текущий сезон!") { }
        public DifferentSeasonsException(string message) : base(message) { }
        
    }
    public class CannotFindSoilException : Exception
    {
        public CannotFindSoilException() : base("Не удаётся найти грядку с таким номером!") { }
        public CannotFindSoilException(string message) : base(message) { }
    }

    public class ThisSoilIsAlreadyExistsException : Exception
    {
        public ThisSoilIsAlreadyExistsException() : base("Грядка с таким номером уже существует!") { }
        public ThisSoilIsAlreadyExistsException(string message) : base(message) { }
    }

    public class CannotFindThisFertilizerException : Exception
    {
        public CannotFindThisFertilizerException() : base("Не удаётся найти удобрение с таким именем!") { }
        public CannotFindThisFertilizerException(string message) : base(message) { }
    }
}
