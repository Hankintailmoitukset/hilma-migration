namespace Hilma.Domain.Extensions
{
    /*
        These are added to requests in APIM like this:

        <inbound>
            <set-header name="Subscription-Identifier" exists-action="override">
                <value>@(context.Subscription.Id)</value>
            </set-header>
            <set-header name="Subscription-Name" exists-action="override">
                <value>@(context.Subscription.Name)</value>
            </set-header>
            <base />
        </inbound>
    */

    public static class HilmaHeaders
    {
        public const string SubscriptionIdentifier = "Subscription-Identifier";
        public const string SubscriptionName = "Subscription-Name";
    }
}
