namespace NarakaBladepoint.Shared.Services.Implementation
{
    [Component(ComponentLifetime.Singleton)]
    internal class EmailItemDataProvider : IEmailItemDataProvider
    {
        public async Task<List<EmailItemData>> GetEmailItemDatasAsync()
        {
            return ConfigurationDataReader.GetList<EmailItemData>();
        }
    }
}
