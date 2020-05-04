using System;
using System.IO;
using System.Reflection;
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

        public static bool noGUI = false;
        public static bool isRussian = true;

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            try
            {
                loadDefaultSettings();

                // Подключаю модули:
                regeneration = new Regeneration();
                if(!J0schiTweaks.noGUI)
                {
                    regeneration.loadRegenerationSettings();
                    InformationManager.DisplayMessage(new InformationMessage("J0schiTweaks loaded."));
                }
                else {
                    regeneration.regenerationLoadFromFile();
                    InformationManager.DisplayMessage(new InformationMessage("J0schiTweaks loaded from file."));
                }
            }
            catch(Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage("J0schiTweaks not loaded." + ex));
            }
        }

        /*
        public override void OnMissionBehaviourInitialize(Mission mission) {
        }

        public override void BeginGameStart(Game game) { 
            
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }
        */

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

        public void loadDefaultSettings() {
            string[] strArray = File.ReadAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DefaultConfig.cfg");
            int noGUIint = int.Parse(strArray[0].Split('#')[1].Trim());
            int isRussianInt = int.Parse(strArray[1].Split('#')[1].Trim());
            noGUI = noGUIint == 1 ? true : false;
            isRussian = isRussianInt == 1 ? true : false;
        }
    }
}


///singleplayer _MODULES_*Native*UIfix*SandBoxCore*CustomBattle*Sandbox*StoryMode*RussianByCommando.com.ua*J0schiTweaks*_MODULES_