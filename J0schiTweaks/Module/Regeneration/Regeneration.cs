using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using System.IO;
using System.Reflection;
using ModLib;
using J0schiTweaks.Config;

namespace J0schiTweaks
{
    public class Regeneration
    {
        private Agent playerAgent;

        // Настроечные параметры:
        public float regenerationValue;
        public float regenerationDelay;
        public bool allHealthRegeneration;
        public bool playerHealthRegeneration;
        public bool companionHealthRegeneration;
        public bool partyHealthRegeneration;
        public bool enemyLeaderHealthRegeneration;
        public bool enemyPartyHealthRegeneration;

        private int allUnitCount = 0;

        private int startTime = 1;
        private List<Agent> targetAgentList;


        // Метод выполняет регенерацию здоровья:
        public void regeneration()
        {
            // Если миссия активна:
            if(Mission.Current != null)
            {
                // Начало миссии:
                if(getAllAgentList().Count > allUnitCount || targetAgentList == null || targetAgentList.Count == 0)
                {

                    J0schiTweaks.debug("getAllAgentList().Count = " + getAllAgentList().Count);
                    J0schiTweaks.debug("allUnitCount = " + allUnitCount);

                    playerAgent = null;

                    targetAgentList = new List<Agent>();

                    if(Mission.Current.MainAgent != null)
                    {
                        playerAgent = Mission.Current.MainAgent;
                    }

                    List<Agent> allAgent = getAllAgentList();

                    allUnitCount = allAgent.Count;

                    if(playerAgent != null)
                    {

                        J0schiTweaks.debug("Подготовка списка юнитов.");

                        if(playerHealthRegeneration)
                        {
                            targetAgentList.Add(playerAgent);
                            J0schiTweaks.debug("Игрок был добавлен в список.");
                        }

                        // Применяем фильтры:
                        foreach(Agent a in allAgent.ToArray())
                        {
                            if(a.IsHuman)
                            {

                                // Отбор всех только людей:
                                if(allHealthRegeneration)
                                {
                                    targetAgentList.Add(a);
                                    J0schiTweaks.debug("Добавлены все.");
                                    continue;
                                }

                                // Отбор союзных компаньенов и дружественных лордов:
                                if(companionHealthRegeneration)
                                {
                                    if(a.IsFriendOf(playerAgent))
                                    {
                                        if(a.IsHero)
                                        {
                                            targetAgentList.Add(a);
                                            J0schiTweaks.debug("Добавлен союзный герой: " + a.Name);
                                            continue;
                                        }
                                    }
                                }

                                // Отбор союзных солдат кроме компаньенов и лордов:
                                if(partyHealthRegeneration)
                                {
                                    if(a.IsFriendOf(playerAgent))
                                    {
                                        if(!a.IsHero)
                                        {
                                            targetAgentList.Add(a);
                                            J0schiTweaks.debug("Добавлен союзный пехотинец: " + a.Name);
                                            continue;
                                        }
                                    }
                                }

                                // Отбор только вражеских лордов:
                                if(enemyLeaderHealthRegeneration)
                                {
                                    if(!a.IsFriendOf(playerAgent))
                                    {
                                        if(a.IsHero)
                                        {
                                            targetAgentList.Add(a);
                                            J0schiTweaks.debug("Добавлен вражеский герой: " + a.Name);
                                            continue;
                                        }
                                    }
                                }

                                if(enemyPartyHealthRegeneration)
                                {
                                    if(!a.IsFriendOf(playerAgent))
                                    {
                                        if(!a.IsHero)
                                        {
                                            targetAgentList.Add(a);
                                            J0schiTweaks.debug("Добавлен вражеский пехотинец: " + a.Name);
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                startTime = startTime + 1;

                if(startTime >= regenerationDelay * 10)
                {
                    foreach(Agent a in targetAgentList.ToArray())
                    {

                        if(a.Health <= 0)
                            continue;

                        if((double)a.Health < a.HealthLimit)
                        {
                            J0schiTweaks.debug("next Agent in List: " + a.Name);
                            J0schiTweaks.debug(a.Name + " healt = " + a.Health);
                            J0schiTweaks.debug("regenerationValue = " + regenerationValue);
                            if((double)a.Health + regenerationValue <= a.HealthLimit)
                            {
                                a.Health = (a.Health + regenerationValue);
                            }
                            else
                            {
                                a.Health = a.HealthLimit;
                            }
                        }
                    }

                    startTime = 1;
                }
            }
            else
            {
                // Окончание миссии:
                if(targetAgentList != null)
                {
                    targetAgentList = null;
                }
                allUnitCount = 0;
            }
        }

        // Получение списка всех живых Agent-ов:
        public List<Agent> getAllAgentList()
        {
            List<Agent> resultAgents = Mission.Current.GetNearbyAgents(new Vec2(0.0f, 0.0f), 1E+07f).ToList<Agent>();
            return resultAgents.Count > 0 ? resultAgents : new List<Agent>();
        }

        // Получение списка только компаньенов и союзных лордов:
        public List<Agent> getPartyAgentList()
        {
            List<Agent> resultAgents = Mission.Current.GetNearbyAllyAgents(new Vec2(0.0f, 0.0f), 1E+07f, Mission.Current.MainAgent.Team).ToList<Agent>();
            return resultAgents.Count > 0 ? resultAgents : new List<Agent>();
        }

        // Получение списка противников:
        public List<Agent> getEnemyPartyAgentList()
        {
            List<Agent> resultAgents = Mission.Current.GetNearbyEnemyAgents(new Vec2(0.0f, 0.0f), 1E+07f, Mission.Current.MainAgent.Team).ToList<Agent>();
            return resultAgents.Count > 0 ? resultAgents : new List<Agent>();
        }

        public void regenerationLoadFromFile()
        {
            try
            {
                string[] strArray = File.ReadAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/RegenConfig.cfg");
                this.regenerationDelay = float.Parse(strArray[0].Split('#')[1].Trim());
                this.regenerationValue = float.Parse(strArray[1].Split('#')[1].Trim());
                this.allHealthRegeneration = (int.Parse(strArray[2].Split('#')[1].Trim())) == 1 ? true : false;
                this.playerHealthRegeneration = (int.Parse(strArray[3].Split('#')[1].Trim())) == 1 ? true : false;
                this.companionHealthRegeneration = (float.Parse(strArray[4].Split('#')[1].Trim())) == 1 ? true : false;
                this.partyHealthRegeneration = (float.Parse(strArray[5].Split('#')[1].Trim())) == 1 ? true : false;
                this.enemyLeaderHealthRegeneration = (float.Parse(strArray[6].Split('#')[1].Trim())) == 1 ? true : false;
                this.enemyPartyHealthRegeneration = (float.Parse(strArray[7].Split('#')[1].Trim())) == 1 ? true : false;
            }
            catch(FileNotFoundException ex)
            {
                InformationManager.DisplayMessage(new InformationMessage("Config not found, using default values"));
                this.regenerationDelay = 2;
                this.regenerationValue = 0.5f;
                this.allHealthRegeneration = false;
                this.playerHealthRegeneration = true;
                this.companionHealthRegeneration = true;
                this.partyHealthRegeneration = false;
                this.enemyLeaderHealthRegeneration = true;
                this.enemyPartyHealthRegeneration = false;
            }
        }

        public void loadRegenerationSettings()
        {
            if(J0schiTweaks.isRussian)
            {
                FileDatabase.Initialise("J0schiTweaks");
                SettingsDatabase.RegisterSettings((SettingsBase)(FileDatabase.Get<ModLibSettingsRus>("J0schi Tweaks") ?? new ModLibSettingsRus()));

                //-------------------------------Regeneration---------------------------------------------------------------
                this.regenerationDelay = ModLibSettingsRus.Instance.RegenerationDelay;
                this.regenerationValue = ModLibSettingsRus.Instance.RegenerationValue;
                this.allHealthRegeneration = ModLibSettingsRus.Instance.AllHealthRegeneration;
                this.playerHealthRegeneration = ModLibSettingsRus.Instance.PlayerHealthRegeneration;
                this.companionHealthRegeneration = ModLibSettingsRus.Instance.CompanionHealthRegeneration;
                this.partyHealthRegeneration = ModLibSettingsRus.Instance.PartyHealthRegeneration;
                this.enemyLeaderHealthRegeneration = ModLibSettingsRus.Instance.EnemyLeaderHealthRegeneration;
                this.enemyPartyHealthRegeneration = ModLibSettingsRus.Instance.EnemyPartyHealthRegeneration;
                J0schiTweaks.enableMessage = ModLibSettingsRus.Instance.EnableMessage;
            }
            else
            {
                FileDatabase.Initialise("J0schiTweaks");
                SettingsDatabase.RegisterSettings((SettingsBase)(FileDatabase.Get<ModLibSettingsEng>("J0schi Tweaks") ?? new ModLibSettingsEng()));

                //-------------------------------Regeneration---------------------------------------------------------------
                this.regenerationDelay = ModLibSettingsEng.Instance.RegenerationDelay;
                this.regenerationValue = ModLibSettingsEng.Instance.RegenerationValue;
                this.allHealthRegeneration = ModLibSettingsEng.Instance.AllHealthRegeneration;
                this.playerHealthRegeneration = ModLibSettingsEng.Instance.PlayerHealthRegeneration;
                this.companionHealthRegeneration = ModLibSettingsEng.Instance.CompanionHealthRegeneration;
                this.partyHealthRegeneration = ModLibSettingsEng.Instance.PartyHealthRegeneration;
                this.enemyLeaderHealthRegeneration = ModLibSettingsEng.Instance.EnemyLeaderHealthRegeneration;
                this.enemyPartyHealthRegeneration = ModLibSettingsEng.Instance.EnemyPartyHealthRegeneration;
                J0schiTweaks.enableMessage = ModLibSettingsEng.Instance.EnableMessage;
            }
        }
    }
}