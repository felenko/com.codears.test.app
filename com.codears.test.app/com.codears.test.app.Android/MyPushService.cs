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

namespace com.codears.test.app.Droid
{

        public class MyPushService//:BackendlessPushService
        {

            public void OnRegistered(Context context, String registrationId)
        {
            Toast.MakeText(context, "device registered" + registrationId, ToastLength.Short).Show();
            Toast.MakeText(context, Backendless.Messaging.DeviceRegistration.DeviceId + registrationId, ToastLength.Short).Show();
        }

        
        public void onUnregistered(Context context, Boolean unregistered)
        {
            Toast.MakeText(context, "device unregistered", ToastLength.Short).Show();
        }

    
        public bool onMessage(Context context, Intent intent)
        {
            String message = intent.GetStringExtra("message");
            Toast.MakeText(context, "Push message received. Message: " + message, ToastLength.Short).Show();
            // When returning 'true', default Backendless onMessage implementation will be executed.
            // The default implementation displays the notification in the Android Notification Center.
            // Returning false, cancels the execution of the default implementation.
            return false;
        }

       
        public void onError(Context context, String message)
        {
            Toast.MakeText(context, message, ToastLength.Short).Show();
        }
    }
}
