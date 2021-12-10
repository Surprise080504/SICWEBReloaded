namespace SICWEB.Models
{
    public class SearchKey
    {
        public int family { get; set; }
        public int subFamily { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

    public class SearchProveedorKey
    {
        public string ruc { get; set; }
        public string social { get; set; }
    }

    public class SearchOrdenKey
    {
        public string ruc { get; set; }
        public int moneda { get; set; }
        public int estado { get; set; }
    }

    public class SearchEntradaKey
    {
        public string ruc { get; set; }
        public string razonsocial { get; set; }
        public string desde { get; set; }
        public string hasta { get; set; }
        public int estado { get; set; }
    }

    public class SearchAlmacenKey
    {
        public string descripcion { get; set; }
    }

    public class SearchMenuKey
    {
        public string menu { get; set; }
        public int parent_id { get; set; }
        public int state { get; set; }
    }
    public class SearchUserKey
    {
        public string user { get; set; }
        public string networkuser{ get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int state { get; set; }
    }

    public class SearchClientKey
    {
        public string company { get; set; }
        public string ruc { get; set; }
        public bool client { get; set; }
        public bool provider { get; set; }
    }

    public class SearchStyleKey
    {
        public string code { get; set; }
        public string name { get; set; }
        public string color { get; set; }
    }

    public class SearchPedidoKey
    {
        public int id { get; set; }
        public string client { get; set; }
        public int pedido { get; set; }
    }
}