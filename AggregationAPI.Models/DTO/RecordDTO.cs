using System;

namespace AggregationAPI.Models.DTO
{
    public class RecordDTO
    {
        public RecordDTO(string tINKLAS, string oBT_PAVADINIMAS, string oBJ_GV_TIPAS, int oBJ_NUMERIS, double? p_PLUS, DateTime pL_T, double? p_MINUS)
        {
            TINKLAS = tINKLAS;
            OBT_PAVADINIMAS = oBT_PAVADINIMAS;
            OBJ_GV_TIPAS = oBJ_GV_TIPAS;
            OBJ_NUMERIS = oBJ_NUMERIS;
            P_PLUS = p_PLUS;
            PL_T = pL_T;
            P_MINUS = p_MINUS;
        }

        public string TINKLAS { get; set; }
        public string OBT_PAVADINIMAS { get; set; }
        public string OBJ_GV_TIPAS { get; set; }
        public int OBJ_NUMERIS { get; set; }
        public double? P_PLUS { get; set; }
        public DateTime PL_T { get; set; }
        public double? P_MINUS { get; set; }


        public override bool Equals(object obj)
        {
            var comparer = obj as RecordDTO;
            if (TINKLAS != comparer.TINKLAS || OBT_PAVADINIMAS != comparer.OBT_PAVADINIMAS
                || OBJ_GV_TIPAS != comparer.OBJ_GV_TIPAS || OBJ_NUMERIS != comparer.OBJ_NUMERIS
                || P_PLUS != comparer.P_PLUS || PL_T != comparer.PL_T || P_MINUS != comparer.P_MINUS)
            {
                return false;
            }
            return true;

        }
    }
}
