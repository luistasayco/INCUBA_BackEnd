namespace Net.Business.Entities
{
    public class BE_TxSIMLesiones: EntityBase
    {
        public int IdSIMLesiones { get; set; }
        public int IdSIM { get; set; }
        public int Edad { get; set; }
        public string LesionesDeudemo { get; set; }
        public string LesionesIntestinoMedio { get; set; }
        public string LesionesHigado { get; set; }
    }
}
