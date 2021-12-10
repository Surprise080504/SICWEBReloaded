using System.Collections.Generic;

namespace SICWEB.Models
{
    public class FamilySub 
    {
        public T_ITEM_FAMILIA Family { get; set; }
        public T_ITEM_SUB_FAMILIA SubFamily { get; set; }

        public FamilySub()
        { }
    }


    public class FamilySubList
    {
        public List<T_ITEM_FAMILIA> Family { get; set; }
        public List<T_ITEM_SUB_FAMILIA> SubFamily { get; set; }

        public FamilySubList()
        { }
    }
}