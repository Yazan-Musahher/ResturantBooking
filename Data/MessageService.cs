using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_Booking.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioTest.Data;

// Twilio documentation
// https://www.twilio.com/docs/sms/quickstart/csharp-dotnet-framework
public class MessageService
{
    public MessageService()
    {
        
    }

    public void SendMessage(Restaurant restaurant, Reservation reservation)
    {
        // Initalize Twilio client
        TwilioClient.Init(accountSid, authToken);

        // Message which gets sent to the customer
        var message = MessageResource.Create(
            body: $"You have booked a table at {restaurant.Name}. " +
                  $"Looking forward to see you at {reservation.Time}.",
            from: new Twilio.Types.PhoneNumber(phoneNumberFrom),
            to: new Twilio.Types.PhoneNumber(phoneNumberTo)
        );
        
        Console.WriteLine(message.Sid);
    }
    
    private string accountSid = "Insert SSID";
    private string authToken = "Insert AuthToken";
    private string phoneNumberFrom = "+12674632427";
    private string phoneNumberTo = "+4747752193";
}