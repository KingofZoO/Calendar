﻿using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using AndroidApp = Android.App.Application;
using Xamarin.Forms;
using Calendar.Interfaces;

[assembly: Dependency(typeof(Calendar.Droid.AndroidNotificationManager))]
namespace Calendar.Droid {
    public class AndroidNotificationManager : INotificationManager {
        public const string ChannelId = "default";
        private const string channelName = "Default";
        private const string channelDescription = "The default channel for notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        private bool channelInitialized = false;
        private NotificationManager manager;

        public void ScheduleNotification(string title, string message, DateTime scheduleDate) {
            if (!channelInitialized) {
                CreateNotificationChannel();
            }

            var alarmIntent = new Intent(AndroidApp.Context, typeof(NotificationAlarmHandler));
            alarmIntent.PutExtra(TitleKey, title);
            alarmIntent.PutExtra(MessageKey, message);

            var pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            var utcTime = TimeZoneInfo.ConvertTimeToUtc(scheduleDate);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            var notifyTimeInInMilliseconds = utcTime.AddSeconds(-epochDif).Ticks / 10000;

            var alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager?.Set(AlarmType.RtcWakeup, notifyTimeInInMilliseconds, pendingIntent);
        }

        private void CreateNotificationChannel() {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O) {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(ChannelId, channelNameJava, NotificationImportance.Default) {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
    }

    [BroadcastReceiver]
    internal class NotificationAlarmHandler : BroadcastReceiver {
        private const int pendingIntentId = 0;

        public override void OnReceive(Context context, Intent intent) {
            var message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
            var title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);

            PendingIntent pendingIntent = PendingIntent.GetActivity(context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, AndroidNotificationManager.ChannelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.xamagonBlue))
                .SetSmallIcon(Resource.Drawable.xamagonBlue)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(0, builder.Build());
        }
    }
}