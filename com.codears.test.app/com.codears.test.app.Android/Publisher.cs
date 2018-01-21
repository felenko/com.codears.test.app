using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BackendlessAPI;
using BackendlessAPI.Messaging;

namespace com.codears.test.app.Droid
{
    public class Publisher
    {
        public void Publish()
        {
            DeliveryOptions deliveryOptions = new DeliveryOptions();
            deliveryOptions.PushPolicy = PushPolicyEnum.ONLY;
            deliveryOptions.PushBroadcast = DeliveryOptions.ANDROID;

            PublishOptions publicOptions = new PublishOptions();
            publicOptions.Headers.Add("android-ticker-text", "Hi you got push notification!");
            publicOptions.Headers.Add("android-content-title", "Message title!");
            publicOptions.Headers.Add("android-content-text", "notification text");
            MessageStatus status = Backendless.Messaging.Publish("Hi Devices!", publicOptions, deliveryOptions);

        }
    }
}