using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace J0schiTweaks
{
    public class J0schiTweaks : MBSubModuleBase
    {
        public static bool enableMessage = false;

        Regeneration regeneration = null;

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            // Подключаю модули:
            Regeneration regeneration = new Regeneration();
            regeneration.loadFromFile();

            InformationManager.DisplayMessage(new InformationMessage("J0schiTweaks loaded."));
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
    }
}

//J0schiTweaks
///singleplayer _MODULES_*Native*UIfix*SandBoxCore*CustomBattle*Sandbox*StoryMode*RussianByCommando.com.ua*test*_MODULES_