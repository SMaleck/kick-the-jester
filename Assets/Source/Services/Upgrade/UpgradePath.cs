using System;
using System.Collections.Generic;
using Assets.Source.App.Upgrade;

namespace Assets.Source.Services.Upgrade
{
    public class UpgradePath<T>
    {
        private readonly List<UpgradeStep<T>> path;
        
        public UpgradePath(List<UpgradeStep<T>> path)
        {
            this.path = path;
        }

        public int MaxLevel
        {
            get { return path.Count - 1; }
        }

        public bool IsMaxLevel(int level)
        { 
            return level >= MaxLevel;
        }

        /// <summary>
        /// Returns the next level available, or the maximum level if already at it.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int NextLevel(int level)
        {
            if (IsMaxLevel(level)) return level;
            return level + 1;
        }

        /// <summary>
        /// Gets the value of a specific level.
        /// </summary>
        /// <param name="level"></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in case the level exceeds the maximum</exception>
        /// <returns></returns>
        public T ValueAt(int level)
        {
            if (level > MaxLevel)
            {
                throw new ArgumentOutOfRangeException("level");   
            }

            return path[level].Value;
        }

        /// <summary>
        /// <para>Gets the cost of upgrading to the next level</para>
        /// Returns 0 if it is already at the maximum level
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <returns></returns>
        public int UpgradeCost(int currentLevel)
        {
            if (IsMaxLevel(currentLevel)) return 0;

            return path[NextLevel(currentLevel)].Cost;
        }
    }
}
