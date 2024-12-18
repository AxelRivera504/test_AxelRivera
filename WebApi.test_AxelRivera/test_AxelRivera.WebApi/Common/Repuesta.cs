namespace test_AxelRivera.WebApi.Common
{
    public sealed class Respuesta<TType>
    {
        public bool Ok { get; set; }

        public string Codigo { get; set; }

        public string Mensaje { get; set; }

        public TType Data { get; set; } = default(TType);

        // Métodos para respuestas exitosas
        public static Respuesta<TType> Success()
        {
            return Success("");
        }

        public static Respuesta<TType> Success(string mensaje)
        {
            return new Respuesta<TType>
            {
                Ok = true,
                Mensaje = mensaje
            };
        }

        public static Respuesta<TType> Success(TType data)
        {
            return Success(data, "");
        }

        public static Respuesta<TType> Success(TType data, string mensaje, string codigo = "")
        {
            return new Respuesta<TType>
            {
                Ok = true,
                Mensaje = mensaje,
                Data = data,
                Codigo = codigo
            };
        }

        public static Respuesta<TType> Fault(string mensaje = "", string codigoError = "", TType data = default(TType))
        {
            return new Respuesta<TType>
            {
                Ok = false,
                Mensaje = mensaje,
                Codigo = codigoError,
                Data = data
            };
        }
    }

}
