using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using EnjinAutoDonator.Services;
using System;

namespace EnjinAutoDonator
{
    public class EnjinAutoDonatorPlugin : RocketPlugin<EnjinAutoDonatorConfiguration>
    {
        public static EnjinAutoDonatorPlugin Instance { get; private set; }
        
        public UnityEngine.Color MessageColor { get; private set; }

        public PurchasesService PurchasesService { get; private set; }

        protected override void Load()
        {
            Instance = this;
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, UnityEngine.Color.green);

            PurchasesService = gameObject.AddComponent<PurchasesService>();

            Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Destroy(PurchasesService);
            Logger.Log($"{Name} has been unloaded!", ConsoleColor.Yellow);
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "PurchaseBroadcast", "{0} purchased and received {1}!" },
            { "DiscordNotification", "[{0}](https://steamcommunity.com/profiles/{1}) purchased and received {2}!" }
        };
    }
}
