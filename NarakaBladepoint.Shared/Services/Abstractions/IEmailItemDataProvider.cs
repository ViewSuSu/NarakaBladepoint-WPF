namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IEmailItemDataProvider
    {
        Task<List<EmailItemData>> GetEmailItemDatasAsync();
    }
}
