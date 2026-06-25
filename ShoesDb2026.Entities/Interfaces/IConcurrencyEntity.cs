namespace ShoesDb2026.Entities.Interfaces
{
    public interface IConcurrencyEntity
    {
        public byte[] RowVersion { get; set; }
    }
}
