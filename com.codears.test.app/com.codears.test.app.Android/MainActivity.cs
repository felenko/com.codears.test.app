using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BackendlessAPI;
using BackendlessAPI.Async;
using BackendlessAPI.Exception;
using BackendlessAPI.Messaging;
using Java.Lang;
using Java.Util;
using Exception = System.Exception;
using String = System.String;

namespace com.codears.test.app.Droid
{
	[Activity (Label = "com.codears.test.app", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);
		    InitApi();
		    StartPushService();
			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new com.codears.test.app.App ());
		}

	    private void StartPushService()
	    {
           // if (IsPlayServicesAvailable())
	        {
	            RegistrationIntentService.TokenReceived += (sender, token) =>
	            {
	                RegisterDevice(token);
	            };
 
                var intent = new Intent(this, typeof(RegistrationIntentService));
	            StartService(intent);
	        }
        }

	    private void InitApi()
	    {
	        String appId = "A3D96FA2-7314-2543-FF66-0B60549D7300";
	        String secretKey = "97ABCC00-2E19-E4F9-FFCA-B3721842F100";
	        Backendless.URL = "http://api.backendless.com";
	        try
	        {
	            Backendless.InitApp(appId, secretKey);

                
	        }
	        catch (Exception e)
	        {

	        }

        }

	    private void errorHandler(BackendlessFault fault)
	    {
	        Toast.MakeText(this, "error registering", ToastLength.Short);
	    }

      
	    public void RegisterDevice(string token)
	    {
            // Server API Key AIzaSyCasyCtHfgsdccFss90nt06-LnJ6y_4D3g
	        String id = null;
	       
	            id = Build.Serial;
	            var OS_VERSION = (Build.VERSION.SdkInt).ToString();
	            var OS = "ANDROID";
	            try
	            {
	                id = UUID.RandomUUID().ToString();
	            }
	            catch (Exception e)
	            {
	                //StringBuilder builder = new StringBuilder();
	                //builder.append(System.getProperty("os.name"));
	                //builder.append(System.getProperty("os.arch"));
	                //builder.append(System.getProperty("os.version"));
	                //builder.append(System.getProperty("user.name"));
	                //builder.append(System.getProperty("java.home"));
	                //id = UUID.nameUUIDFromBytes(builder.toString().getBytes()).toString();
	            }

	        var DEVICE_ID = id;
	        //"174677789761"
            var deviceReg = new DeviceRegistration();
	        deviceReg.Os = OS;
	        deviceReg.OsVersion = OS_VERSION;
	        deviceReg.Expiration = DateTime.Now.AddDays(10);
	        deviceReg.DeviceId = DEVICE_ID;
	        deviceReg.DeviceToken = token;
            Backendless.Messaging.DeviceRegistration = deviceReg;
            
            Backendless.Messaging.RegisterDevice(token, "default", new AsyncCallback<string>(responseHanlder, errorHandler));
	    }

	    private void responseHanlder(string response)
	    {
            //Toast.MakeText(this, "Registered", ToastLength.Short);
            //Toast.MakeText(this, Backendless.Messaging.DeviceRegistration.DeviceId, ToastLength.Short).Show();
	        string id = Backendless.Messaging.DeviceRegistration.DeviceId;
	    }
    }
}

