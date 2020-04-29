using J0schiTweaks.Config;
using ModLib;
using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace J0schiTweaks
{
    public class J0schiTweaks : MBSubModuleBase
    {
        public const string InstanceID = "J0schiTweaks";
        public const string ModuleFolder = "J0schiTweaks";
        public const string Version = "0.0.3";

        public static bool enableMessage = true;

        public static Regeneration regeneration = null;

        private static bool noGUI = false;
        private static bool isRussian = true;

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            try
            {
                // Подключаю модули:
                regeneration = new Regeneration();
                if(J0schiTweaks.noGUI)
                {
                    regeneration.regenerationLoadFromFile();
                    InformationManager.DisplayMessage(new InformationMessage("J0schiTweaks loaded."));
                }
                if(!J0schiTweaks.noGUI)
                {
                    loadSettings();
                    InformationManager.DisplayMessage(new InformationMessage("J0schiTweaks loaded."));
                }
            }
            catch(Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage("J0schiTweaks not loaded." + ex));
            }
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

        }

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);

            if(regeneration != null) {
               regeneration.regeneration();
            }
        }

        public static void debug(String message)
        {
            if(enableMessage)
            {
                InformationManager.DisplayMessage(new InformationMessage(message));
            }
        }

        public void loadSettings() {

            if(isRussian) {
                FileDatabase.Initialise("J0schiTweaks");
                SettingsDatabase.RegisterSettings((SettingsBase)(FileDatabase.Get<ModLibSettingsRus>("J0schi Tweaks") ?? new ModLibSettingsRus()));

                //-------------------------------Regeneration---------------------------------------------------------------
                regeneration.regenerationDelay = ModLibSettingsRus.Instance.RegenerationDelay;
                regeneration.regenerationValue = ModLibSettingsRus.Instance.RegenerationValue;
                regeneration.allHealthRegeneration = ModLibSettingsRus.Instance.AllHealthRegeneration;
                regeneration.playerHealthRegeneration = ModLibSettingsRus.Instance.PlayerHealthRegeneration;
                regeneration.companionHealthRegeneration = ModLibSettingsRus.Instance.CompanionHealthRegeneration;
                regeneration.partyHealthRegeneration = ModLibSettingsRus.Instance.PartyHealthRegeneration;
                regeneration.enemyLeaderHealthRegeneration = ModLibSettingsRus.Instance.EnemyLeaderHealthRegeneration;
                regeneration.enemyPartyHealthRegeneration = ModLibSettingsRus.Instance.EnemyPartyHealthRegeneration;
                enableMessage = ModLibSettingsRus.Instance.EnableMessage;
            }
            else {
                FileDatabase.Initialise("J0schiTweaks");
                SettingsDatabase.RegisterSettings((SettingsBase)(FileDatabase.Get<ModLibSettingsEng>("J0schi Tweaks") ?? new ModLibSettingsEng()));

                //-------------------------------Regeneration---------------------------------------------------------------
                regeneration.regenerationDelay = ModLibSettingsEng.Instance.RegenerationDelay;
                regeneration.regenerationValue = ModLibSettingsEng.Instance.RegenerationValue;
                regeneration.allHealthRegeneration = ModLibSettingsEng.Instance.AllHealthRegeneration;
                regeneration.playerHealthRegeneration = ModLibSettingsEng.Instance.PlayerHealthRegeneration;
                regeneration.companionHealthRegeneration = ModLibSettingsEng.Instance.CompanionHealthRegeneration;
                regeneration.partyHealthRegeneration = ModLibSettingsEng.Instance.PartyHealthRegeneration;
                regeneration.enemyLeaderHealthRegeneration = ModLibSettingsEng.Instance.EnemyLeaderHealthRegeneration;
                regeneration.enemyPartyHealthRegeneration = ModLibSettingsEng.Instance.EnemyPartyHealthRegeneration;
                enableMessage = ModLibSettingsEng.Instance.EnableMessage;
            }
        }
    }
}


///singleplayer _MODULES_*Native*UIfix*SandBoxCore*CustomBattle*Sandbox*StoryMode*RussianByCommando.com.ua*J0schiTweaks*_MODULES_