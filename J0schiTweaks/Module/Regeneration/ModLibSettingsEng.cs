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
    public class ModLibSettingsEng : SettingsBase
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
        [SettingProperty("Pause between the character’s health gain (in sec).", 1, 100, 1, 30, "Pause between the character’s health gain (in sec).")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public int RegenerationDelay { get; set; } = 1;

        [XmlElement]
        [SettingProperty("The value of the character’s health gain.", 1, 30, 1, 100, "The value by which the character's health will be increased.")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public int RegenerationValue { get; set; } = 1;

        // Настройки регенерации игрока:
        [XmlElement]
        [SettingProperty("Categories of gradual regeneration.", 0, 1, "Categories of gradual regeneration.")]
        [SettingPropertyGroup("Health Regeneration.", true)]
        public bool CategoryRegeneration { get; set; } = true;

        [XmlElement]
        [SettingProperty("The health of all characters.", 0, 1, "Gradual health regeneration of all living characters.")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public bool AllHealthRegeneration { get; set; } = true;

        [XmlElement]
        [SettingProperty("Player Health", 0, 1, "Gradual regeneration of player’s health.")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public bool PlayerHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("Player mates health.", 0, 1, "Regeneration of the player’s teammates health, including Allied Lords.")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public bool CompanionHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("Health soldier party player.", 0, 1, "Gradual health regeneration of all soldiers of the player party except companions.")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public bool PartyHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("The health of enemy leaders.", 0, 1, "Gradual health regeneration of enemy leaders.")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public bool EnemyLeaderHealthRegeneration { get; set; } = false;

        [XmlElement]
        [SettingProperty("Health of soldiers of an enemy party other than Lords.", 0, 1, "Gradual regeneration of the health of soldiers of an enemy party except for Lords.")]
        [SettingPropertyGroup("Health Regeneration.", false)]
        public bool EnemyPartyHealthRegeneration { get; set; } = false;

//----------------------------------------------------------Regeneration End------------------------------------------------------------------

        [XmlElement]
        [SettingProperty("Enable / disable debugging messages.", 0, 1, "Enable / disable debugging messages.")]
        [SettingPropertyGroup("Development mode", false)]
        public bool EnableMessage { get; set; } = false;
    }
}
