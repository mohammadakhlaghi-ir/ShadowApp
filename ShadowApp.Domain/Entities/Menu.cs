namespace ShadowApp.Domain.Entities
{
    public class Menu
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = "";
        public Guid LayoutItemID { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Crc { get; set; } = "";

        #region navigations
        public LayoutItem LayoutItem { get; set; } = null!;
        #endregion
    }
}
