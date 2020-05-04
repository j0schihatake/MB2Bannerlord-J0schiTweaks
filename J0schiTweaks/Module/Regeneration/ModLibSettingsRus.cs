using ModLib;
using ModLib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace J0schiTweaks.Config
{
    public class ModLibSettingsRus : SettingsBase
    {
        public const string InstanceID = "J0schi Tweaks";
        public const string ModuleFolder = "J0schiTweaks";

        public override string ModName
        {
            get {
                return "J0schi Tweaks";
            }
        }

        public override string ModuleFolderName
        {
            get {
                return "J0schiTweaks";
            }
        }

        [XmlElement]
        public override string ID { get; set; } = "J0schi Tweaks";

        public static ModLibSettingsRus Instance
        {
            get {
                return (ModLibSettingsRus)SettingsDatabase.GetSettings("J0schi Tweaks");
            }
        }

//---------------------------------------------------------------Regeneration Begin-----------------------------------------------------------------------
        // Настройки параметров:
        [XmlElement]
        [SettingProperty("Пауза между прибавкой здоровья у персонажа(в сек).", 1, 10, 1, 30, "Пауза между прибавкой здоровья у персонажа(в сек).")]
        [SettingPropertyGroup("Регенерация здоровья.", false)]
        public float RegenerationDelay { get; set; } = 1;

        [XmlElement]
        [SettingProperty("Значение прибавки к здоровью персонажа.", 1, 30, 1, 100, "Значение на которое будет увеличено здоровье персонажа.")]
        [SettingPropertyGroup("Регенерация здоровья.", false)]
        public float RegenerationValue { get; set; } = 1;

        // Настройки регенерации игрока:
        [XmlElement]
        [SettingProperty("Категории постепеннной регенерации.", 0, 1, "Категории постепенной регенерации.")]
        [SettingPropertyGroup("Регенерация здоровья", true)]
        public bool CategoryRegeneration { get; set; } = true;

        [XmlElement]
        [SettingProperty("Здоровье всех персонажей.", 0, 1, "Постепенная регенерация здоровья всех живых персонажей.")]
        [SettingPropertyGroup("Регенерация здоровья", false)]
        public bool AllHealthRegeneration { get; set; } = true;

        [XmlElement]
        [SettingProperty("Здоровье игрока.", 0, 1, "Постепенная регенерация здоровья игрока.")]
        [SettingPropertyGroup("Регенерация здоровья", false)]
        public bool PlayerHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("Здоровье напарников игрока.", 0, 1, "Регенерация здоровья напарников игрока, включая союзных лордов.")]
        [SettingPropertyGroup("Регенерация здоровья", false)]
        public bool CompanionHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("Здоровье солдат партии игрока.", 0, 1, "Постепенная регенрация здоровья всех солдат партии игрока кроме компаньенов.")]
        [SettingPropertyGroup("Регенерация здоровья", false)]
        public bool PartyHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("Здоровье вражеских лидеров.", 0, 1, "Постепенная регенерация здоровья вражеских лидеров.")]
        [SettingPropertyGroup("Регенерация здоровья", false)]
        public bool EnemyLeaderHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("Здоровье солдат вражеской партии кроме лордов.", 0, 1, "Постепенная регенерация здоровья солдат вражеской партии кроме лордов.")]
        [SettingPropertyGroup("Регенерация здоровья", false)]
        public bool EnemyPartyHealthRegeneration { get; set; } = false;

//----------------------------------------------------------Regeneration End------------------------------------------------------------------

        [XmlElement]
        [SettingProperty("Включение/отключение отладочных сообщений.", 0, 1, "Включение/отключение отладочных сообщений.")]
        [SettingPropertyGroup("Режим разработки", false)]
        public bool EnableMessage { get; set; } = false;
    }
}
