namespace Stone.Sdk.Messaging
{
    public static class MessageContextExtensions
    {
        public static string GetMetadataStringValue(this IMessageContext context, string key)
        {
            return context.Metadata.ContainsKey(key)
                ? System.Text.Encoding.Default.GetString((byte[]) context.Metadata[key])
                : string.Empty;
        }
    }
}