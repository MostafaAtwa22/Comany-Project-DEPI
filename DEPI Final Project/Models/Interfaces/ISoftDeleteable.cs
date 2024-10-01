namespace DEPI_Final_Project.Models.Interfaces
{
    public interface ISoftDeleteable
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateOFDelete { get; set; }
        public void Delete()
        {
            IsDeleted = true;
            DateOFDelete = DateTime.Now;
        }
        public void UnDoDelete()
        {
            IsDeleted = false;
            DateOFDelete = null;
        }
    }
}
