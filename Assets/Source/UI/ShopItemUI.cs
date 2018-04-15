using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Source.App;

namespace Assets.Source.UI
{
    public class ShopItemUI : MonoBehaviour
    {

        public void PurchaseKickForceUpgrade()
        {
            GameObjectPool.playerProfile.KickForce += 200f;
        }

        public void PurchaseKickCountUpgrade()
        {
            int kickCount = GameObjectPool.playerProfile.KickCount;
            GameObjectPool.playerProfile.KickCount = kickCount + 2;
        }

        public void PurchaseStatsReset()
        {
            GameObjectPool.playerProfile.ResetStats();
        }
    }
}
