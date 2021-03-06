﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Gcm;
using Android.Gms.Iid;

namespace com.codears.test.app.Droid
{
    

   [Service(Exported = false)]
        class RegistrationIntentService : IntentService
        {
            static object locker = new object();

            public RegistrationIntentService() : base("RegistrationIntentService") { }

            protected override void OnHandleIntent(Intent intent)
            {
                try
                {
                    Log.Info("RegistrationIntentService", "Calling InstanceID.GetToken");
                    lock (locker)
                    {
                        var instanceID = InstanceID.GetInstance(this);
                        string token = instanceID.GetToken("174677789761", GoogleCloudMessaging.InstanceIdScope, null); //sender id

                        Log.Info("RegistrationIntentService", "GCM Registration Token: " + token);
                        SendRegistrationToAppServer(token);
                        Subscribe(token);
                    }
                }
                catch (Exception e)
                {
                    Log.Debug("RegistrationIntentService", "Failed to get a registration token");
                    return;
                }
            }

            void SendRegistrationToAppServer(string token)
            {
                TokenReceived(this, token);
            }

            public static EventHandler<string> TokenReceived;

            void Subscribe(string token)
            {
                var pubSub = GcmPubSub.GetInstance(this);
                pubSub.Subscribe(token, "/topics/global", null);
            }
        }
    }
    
