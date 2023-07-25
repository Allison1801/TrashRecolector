namespace TrashRecolectorBackend.Entidades
{
    public class ResponsiveList<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
